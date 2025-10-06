using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Drawing;

namespace server_app.games
{
	public struct gameStats
	{
		public gameStats()
		{
			correct = [];
			accuracy = [];
			time = [];
		}

		public List<bool> correct;
		public List<double> accuracy;
		public List<TimeSpan> time;

		public void update(double accuracy, TimeSpan time, bool correct)
		{
			this.accuracy.Add(accuracy);
			this.correct.Add(correct);
			this.time.Add(time);
		}
	}
	public interface IPlayable
	{
		bool queueUser(string userID);
		void dequeueUser(string userID);
		Task updateUsers();
		Task startGame();
		Task submissionPhase();
		void loadResponse(string userID, byte[] input);
		void evaluationPhase(char letter);
		Task continueRequest(string userID);
		void endGame();
		string getType();
		string getGameID();
		bool hasStarted();
		int getPlayerCount();
		int getMaxPlayers();
	}
	public abstract class abstractGame
	{
		protected IHubContext<connection> hubContext;

		protected string gameID;
		protected int maxPlayers;
		protected string type;

		private bool started;

		protected List<string> userIDs;
		protected List<friendData> userDatas;
		protected Dictionary<string, gameStats> stats;

		protected Random rnd;
		protected List<char> letters;
		protected int roundCount = 0;

		protected DateTime startTime;
		protected Dictionary<string, (double[] submission, DateTime time)> currentResponses;
		protected HashSet<string> continueRequests = [];

		public abstractGame(IHubContext<connection> context, string type, string userID, int maxPlayers)
		{
			userIDs = [];
			this.maxPlayers = maxPlayers;
			stats = [];
			letters = [];
			rnd = new();
			hubContext = context;
			currentResponses = [];
			userDatas = [];
			this.type = type;
			started = false;

			gameID = userID + DateTime.UtcNow.ToString();
		}

		public bool queueUser(string userID)
		{
			if (database.loadFriendData(userID, out friendData data))
			{
				userIDs.Add(userID);
				userDatas.Add(data);
				return true;
			}
			return false;
		}
		public async virtual void dequeueUser(string userID)
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
				if (connection.map.TryGetValue(user, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("updateUsers", userDatas);
				}
			}
		}

		public virtual async Task startGame()
		{
			started = true;

			foreach (string user in userIDs)
			{
				stats.Add(user, new gameStats());
			}

			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitStart");
				}
			}

			await Task.Delay(5000); // 5 second countdown

			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("startGame");
				}
			}
		}
		protected List<char> generateLetters(int count)
		{
			List<char> letters = [];

			Random rnd = new();
			for (int i = 0; i < count; i++)
			{
				letters.Add((char)(rnd.Next(0, 26) + 65));
			}
			return letters;
		}


		protected async Task awaitRound()
		{
			continueRequests.Clear();
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
				}
			}
		}
		public abstract Task submissionPhase();
		protected async Task sendLetter(List<string> userIDs, char letter)
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveLetter", letter);
				}
			}
		}

		public virtual void loadResponse(string userID, byte[] input)
		{
            DateTime end = DateTime.UtcNow;
            var ms = new MemoryStream(input);
            var bmp = new Bitmap(ms);
            double[] array = data.preprocessImage(bmp);
            currentResponses.Add(userID, (array, end));
        }
		protected bool evaluateSubmission(ref evaluate evaluate, string userID, char character)
		{
			int letter = character - 65;

			evaluate = new evaluate(currentResponses[userID].submission);
			bool correct = evaluate.result == letter;

			if (stats.TryGetValue(userID, out gameStats currentStats))
			{
				DateTime endTime = currentResponses[userID].time;
				double accuracy = evaluate.activatedValues[evaluate.layerCount - 1][letter];

				currentStats.update(accuracy, endTime - startTime, correct);
			}
			stats[userID] = currentStats;
			return correct;
		}
		protected async Task sendResult(string userID, gameStats stats)
		{
			bool correct = stats.correct[^1];
			double accuracy = stats.accuracy[^1];
			TimeSpan time = stats.time[^1];

			if (connection.map.TryGetValue(userID, out string? connectionID))
			{
				await hubContext.Clients.Client(connectionID).SendAsync("receiveResults", correct, accuracy, time);
			}
		}

		public abstract Task continueRequest(string userID);

		public virtual async void endGame()
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
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

			async Task updateStatistics(userData userData, char letter, int i)
			{
				double accuracy = userData.statistics[letter].accuracy;
				TimeSpan time = userData.statistics[letter].time;
				int total = userData.statistics[letter].total;

				double updatedAccuracy = (accuracy * total + stats[userData.userID].accuracy[i]) / (total + 1);
				TimeSpan updatedTime = (time * total + stats[userData.userID].time[i]) / (total + 1);

				if (database.updateStatistics(userData.userID, letter, updatedAccuracy, updatedTime, total + 1))
				{
					if (connection.map.TryGetValue(userData.userID, out string? connectionID))
					{
						await hubContext.Clients.Client(connectionID).SendAsync("updateStatistics", letter, new statistics(updatedAccuracy, updatedTime, total + 1));
					}
				}
				else
				{
					database.outputException("Failed to update statistics");
				}
			}
		}

		public string getType() => type;
		public string getGameID() => gameID;
		public bool hasStarted() => started;
		public int getMaxPlayers() => maxPlayers;
		public int getPlayerCount() => userIDs.Count;
	}
}
