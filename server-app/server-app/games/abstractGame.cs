using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;

namespace server_app.games
{
	public struct @stats
	{
		public @stats()
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
		void queueUser(string userID);
		void dequeueUser(string userID);
		Task updateUsers();
		void startGame();
		void submissionPhase();
		void loadResponse(string userID, double[] input);
		void evaluationPhase(char letter);
		void continueRequest(string userID);
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
		protected Dictionary<string, stats> stats;

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

		public void queueUser(string userID)
		{
			if (database.loadFriendData(userID, out friendData data))
			{
				userIDs.Add(userID);
				userDatas.Add(data);
			}
			else
			{
				// failed to queue user, send error to client
			}
		}
		public async virtual void dequeueUser(string userID)
		{
			userIDs.Remove(userID);

			// remove userData
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
				else
				{
					throw new DisconnectException(user);
				}
			}
		}

		public virtual async void startGame()
		{
			started = true;

			// define new stats object for each user
			foreach (string user in userIDs)
			{
				stats.Add(user, new stats());
			}

			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitStart");
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}

			Thread.Sleep(5000); // 5 sec countdown

			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("startGame");
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}
		}
		protected List<char> generateRandomLetters(int count)
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
				else
				{
					throw new DisconnectException(userID);
				}
			}
		}
		public abstract void submissionPhase();
		protected async Task sendLetter(List<string> userIDs, char letter)
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveLetter", letter);
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}
		}

		protected bool evaluateSubmission(ref evaluate evaluate, string userID, int character)
		{
			int letter = character - 65;

			evaluate = new evaluate(currentResponses[userID].submission);
			bool correct = evaluate.result == letter;

			// update the statistics for the current game
			if (stats.TryGetValue(userID, out stats currentStats))
			{
				DateTime endTime = currentResponses[userID].time;
				double accuracy = evaluate.activatedValues[evaluate.layerCount - 1][letter];

				currentStats.update(accuracy, endTime - startTime, correct);
			}
			stats[userID] = currentStats;
			return correct;
		}
		protected async Task sendResult(string userID, stats stats)
		{
			bool correct = stats.correct[^1];
			double accuracy = stats.accuracy[^1];
			TimeSpan time = stats.time[^1];

			if (connection.map.TryGetValue(userID, out string? connectionID))
			{
				await hubContext.Clients.Client(connectionID).SendAsync("receiveResults", correct, accuracy, time);
			}
			else
			{
				throw new DisconnectException(userID);
			}
		}
		public abstract void continueRequest(string userID);

		public virtual async void endGame() // possibly a faster way to implement this
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
				else
				{
					throw new DisconnectException(userID);
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
					else
					{
						throw new DisconnectException(userData.userID);
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
