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

		public readonly void update(double accuracy, TimeSpan time, bool correct)
		{
			this.accuracy.Add(accuracy);
			this.correct.Add(correct);
			this.time.Add(time);
		}
	}
	public interface IPlayable
	{
		bool queueUser(string userID);
		void DequeueUser(string userID);
		Task updateUsers();
		Task StartGame();
		Task SubmissionPhase();
		void LoadResponse(string userID, byte[] input);
		void EvaluationPhase(char letter);
		Task ContinueRequest(string userID);
		void EndGame();
		string getType();
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
		protected string type;

		private bool _started = false;

		protected List<string> userIDs;
		protected List<friendData> userDatas;
		protected Dictionary<string, gameStats> stats;

		protected Random rnd;
		protected List<char> letters;
		protected int roundCount = 0;

		protected DateTime startTime;
		protected Dictionary<string, (double[] submission, DateTime time)> currentResponses;
		protected List<string> continueRequests = [];

		public string getType() => type;
		public string getGameID() => gameID;
		public bool hasStarted() => _started;
		public int getMaxPlayers() => maxPlayers;
		public int getPlayerCount() => userIDs.Count;

		public Game(IHubContext<Connection> context, string type, string userID, int maxPlayers)
		{
			hubContext = context;

			gameID = userID + DateTime.UtcNow.ToString();
			this.maxPlayers = maxPlayers;
			this.type = type;

			userIDs = [];
			userDatas = [];
			stats = [];

			rnd = new();
			letters = [];

			currentResponses = [];
		}

		public bool queueUser(string userID)
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
			for (int i = 0; i < userDatas.Count; i++)
			{
				if (userDatas[i].userID == userID)
				{
					index = i;
					break;
				}
			}
			userDatas.RemoveAt(index);

			await updateUsers();
		}
		public async Task updateUsers()
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
				stats.Add(user, new gameStats());
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

		protected async Task AwaitRound()
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

			if (stats.TryGetValue(userID, out gameStats currentStats))
			{
				DateTime endTime = currentResponses[userID].time;
				double accuracy = evaluate.activatedValues[Network.layerCount - 1][letter];

				currentStats.update(accuracy, endTime - startTime, correct);
			}
			stats[userID] = currentStats;
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
		public abstract Task ContinueRequest(string userID);

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
				for (int i = 0; i < letters.Count; i++)
				{
					if (database.loadUserData(userID, out userData userData))
					{
						await updateStatistics(userData, letters[i], i);
					}
					else
					{
						database.outputException("Failed to retrieve statistics");
					}
				}
			}
		}
		private async Task updateStatistics(userData userData, char letter, int index)  // breaks on versus ----------------------------------------------------------------------
		{
			double accuracy = userData.statistics[letter].accuracy;
			TimeSpan time = userData.statistics[letter].time;
			int total = userData.statistics[letter].total;

			double updatedAccuracy = (accuracy * total + stats[userData.userID].accuracy[index]) / (total + 1);
			TimeSpan updatedTime = (time * total + stats[userData.userID].time[index]) / (total + 1);

			if (!database.updateStatistics(userData.userID, letter, updatedAccuracy, updatedTime, total + 1))
			{
				database.outputException("Failed to update statistics");
				return;
			}

			if (Connection.map.TryGetValue(userData.userID, out string? connectionID))
			{
				statistics updated = new(updatedAccuracy, updatedTime, total + 1);
				await hubContext.Clients.Client(connectionID).SendAsync("updateStatistics", letter, updated);
			}
		}
	}
}
