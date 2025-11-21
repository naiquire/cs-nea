using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Drawing;

namespace server_app.games
{
	public struct gameStats
	{
		public List<bool> correct;
		public List<double> accuracy;
		public List<TimeSpan> time;
		public gameStats()
		{
			correct = [];
			accuracy = [];
			time = [];
		}

		public readonly void Update(double accuracy, TimeSpan time, bool correct)
		{
			this.accuracy.Add(accuracy);
			this.correct.Add(correct);
			this.time.Add(time);
		}
	}
	public enum Games
	{
		Accuracy,
		Versus,
		Knockout,
	}
	public interface IPlayable
	{
		bool QueueUser(string userID);
		void DequeueUser(string userID);
		Task UpdateUsers();
		Task StartGame();
		Task SubmissionPhase();
		void LoadResponse(string userID, byte[] input);
		void EvaluationPhase(char letter);
		Task ContinueRequest(string userID);
		void EndGame();
		Games getType();
		string getGameID();
		bool hasStarted();
		int getPlayerCount();
		int getMaxPlayers();
	}
	public abstract class Game
	{
		protected IHubContext<Connection> hubContext;

		protected string gameID;
		protected int maxPlayers;
		protected Games type;
		private bool _started;

		protected List<string> userIDs;
		protected List<friendData> userDatas;
		protected Dictionary<string, gameStats> gameStats;

		protected Random rnd;
		protected List<char> letters;
		protected int roundCount;

		protected DateTime startTime;
		protected Dictionary<string, (double[] submission, DateTime time)> currentResponses;
		protected List<string> continueRequests;

		public Games getType() => type;
		public string getGameID() => gameID;
		public bool hasStarted() => _started;
		public int getMaxPlayers() => maxPlayers;
		public int getPlayerCount() => userIDs.Count;

		public Game(IHubContext<Connection> context, Games type, string userID, int maxPlayers)
		{
			hubContext = context;

			gameID = userID + DateTime.UtcNow.ToString();
			this.maxPlayers = maxPlayers;
			this.type = type;
			_started = false;

			userIDs = [];
			userDatas = [];
			gameStats = [];

			rnd = new();
			letters = [];
			roundCount = 0;

			currentResponses = [];
			continueRequests = [];
		}

		public bool QueueUser(string userID)
		{
			if (!database.loadFriendData(userID, out friendData data))
			{
				return false;
			}

			userIDs.Add(userID);
			userDatas.Add(data);
			return true;
		}
		public async virtual void DequeueUser(string userID)
		{
			userIDs.Remove(userID);

			int index = 0;
			for (int i = 0; i < getPlayerCount(); i++)
			{
				if (userDatas[i].userID == userID)
				{
					index = i;
					break;
				}
			}
			userDatas.RemoveAt(index);

			await UpdateUsers();
		}
		public async Task UpdateUsers()
		{
			foreach (var user in userIDs)
			{
				if (Connection.map.TryGetValue(user, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("updateUsers", userDatas);
				}
			}
		}

		public virtual async Task StartGame()
		{
			_started = true;

			foreach (string user in userIDs)
			{
				gameStats.Add(user, new gameStats());
			}

			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitStart");
				}
			}

			await Task.Delay(5000);

			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("startGame");
				}
			}
		}
		protected List<char> GenerateLetters(int count)
		{
			List<char> letters = [];

			Random rnd = new();
			for (int i = 0; i < count; i++)
			{
				letters.Add((char)(rnd.Next(0, 26) + 65));
			}
			return letters;
		}

		protected virtual async Task AwaitRound()
		{
			continueRequests.Clear();
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
				}
			}
		}
		protected async Task SendLetter(List<string> userIDs, char letter)
		{
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveLetter", letter);
				}
			}
		}
		public virtual void LoadResponse(string userID, byte[] input)
		{
            DateTime endTime = DateTime.UtcNow;
			double[] array;

			using (var ms = new MemoryStream(input))
			{
				var bmp = new Bitmap(ms);
				array = data.preprocessImage(bmp);
			}

			currentResponses.Add(userID, (array, endTime));
        }

		protected bool EvaluateSubmission(ref Network evaluate, string userID, char character)
		{
			int letter = character - 65;

			evaluate = new Network(currentResponses[userID].submission);
			bool correct = evaluate.result == letter;

			if (gameStats.TryGetValue(userID, out gameStats currentStats))
			{
				DateTime endTime = currentResponses[userID].time;
				double accuracy = evaluate.activatedValues[Network.layerCount - 1][letter];

				currentStats.Update(accuracy, endTime - startTime, correct);
			}
			gameStats[userID] = currentStats;
			return correct;
		}
		protected async Task SendResult(string userID, gameStats stats)
		{
			bool correct = stats.correct[roundCount];
			double accuracy = stats.accuracy[roundCount];
			TimeSpan time = stats.time[roundCount];

			if (Connection.map.TryGetValue(userID, out string? connectionID))
			{
				await hubContext.Clients.Client(connectionID).SendAsync("receiveResults", correct, accuracy, time);
			}
		}

		public virtual async void EndGame()
		{
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
			}

			foreach (string userID in userIDs)
			{
				for (int i = 0; i < gameStats[userID].accuracy.Count; i++)
				{
					// iterate through each round the current user completed - in Elimination each user may complete a different number of rounds
					if (database.loadUserData(userID, out userData userData))
					{
						// reload userData after each update for duplicate letters
						await UpdateStatistics(userData, letters[i], i);
					}
					else
					{
						database.outputException("Failed to retrieve statistics");
					}
				}
			}
		}
		private async Task UpdateStatistics(userData userData, char letter, int round)
		{
			double accuracy = userData.statistics[letter].accuracy;
			TimeSpan time = userData.statistics[letter].time;
			int total = userData.statistics[letter].total;

			double updatedAccuracy = (accuracy * total + gameStats[userData.userID].accuracy[round]) / (total + 1);
			TimeSpan updatedTime = (time * total + gameStats[userData.userID].time[round]) / (total + 1);

			if (!database.updateStatistics(userData.userID, letter, updatedAccuracy, updatedTime, total + 1))
			{
				database.outputException("Failed to update statistics");
				return;
			}

			if (Connection.map.TryGetValue(userData.userID, out string? connectionID))
			{
				// update statistics client-side for the indexed round
				statistics updated = new(updatedAccuracy, updatedTime, total + 1);
				await hubContext.Clients.Client(connectionID).SendAsync("updateStatistics", letter, updated);
			}
		}
	}
}
