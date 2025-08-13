using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

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

		/// <summary>
		/// Updates the current statistics for the user.
		/// </summary>
		/// <param name="evaluate"></param>
		/// <param name="letter"></param>
		/// <param name="time"></param>
		/// <param name="correct"></param>
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
		/// <summary>
		/// Calls for the next iteration of the game.
		/// </summary>
		/// <param name="letter"></param>
		void submissionPhase();
		/// <summary>
		/// Loads a submission into the game class and ends the submission phase if all responses are present.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="input"></param>
		void loadResponse(string userID, double[] input);
		/// <summary>
		/// Evaluates responses and sends statistics to clients. Calls the next submission phase if available.
		/// </summary>
		/// <param name="letter"></param>
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
		protected string type;
		private bool started;

		protected List<string> userIDs;
		protected List<friendData> userDatas;
		protected string gameID;
		protected int maxPlayers;

		protected List<char> letters;
		protected Random rnd;
		protected int count = 0;

		protected DateTime startTime;
		protected Dictionary<string, stats> stats;
		protected List<string> continueRequests = [];
		protected Dictionary<string, (double[] submission, DateTime time)> currentResponses;

		/// <summary>
		/// Base initialisation for the game classes.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="maxPlayers"></param>
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

		/// <summary>
		/// Queues a user into the current game.
		/// </summary>
		/// <param name="userID"></param>
		/// <exception cref="DisconnectException"></exception>
		public void queueUser(string userID)
		{
			if (database.loadFriendData(userID, out friendData data))
			{
				userIDs.Add(userID);
				userDatas.Add(data);
			}
		}
		
		/// <summary>
		/// Dequeues a user from a game regardless of current state.
		/// </summary>
		/// <param name="userID"></param>
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
		
		/// <summary>
		/// Updates the clients with a list of users.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="DisconnectException"></exception>
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

		/// <summary>
		/// Starts the current game and initialises values for statistics for each user.
		/// </summary>
		/// <exception cref="DisconnectException"></exception>
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

		/// <summary>
		/// Generates a fixed number of random characters from A-Z.
		/// </summary>
		/// <param name="count"></param>
		/// <returns>A list of random characters.</returns>
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
		public abstract void submissionPhase();

		/// <summary>
		/// Configures a countdown on the clients before the next round.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="DisconnectException"></exception>
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

		/// <summary>
		/// Sends a character to the given users.
		/// </summary>
		/// <param name="userIDs"></param>
		/// <param name="letter"></param>
		/// <returns></returns>
		/// <exception cref="DisconnectException"></exception>
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

		/// <summary>
		/// Evaluates a user's submission and updates their current statistics.
		/// </summary>
		/// <param name="evaluates"></param>
		/// <param name="i"></param>
		/// <param name="userIDs"></param>
		/// <param name="character"></param>
		/// <returns>A boolean value representing if the submission was correct.</returns>
		protected bool evaluateSubmission(ref evaluate evaluate, string userID, int character)
		{
			// evaluate the submission
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

		/// <summary>
		/// Sends a user their result for the current character.
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="stats"></param>
		/// <returns></returns>
		/// <exception cref="DisconnectException"></exception>
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

		/// <summary>
		/// Ends the current game and updates statistics.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="DisconnectException"></exception>
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

		/// <summary>
		/// Gets the type of game.
		/// </summary>
		/// <returns></returns>
		public string getType() => type;

		/// <summary>
		/// Gets the ID of the game.
		/// </summary>
		/// <returns></returns>
		public string getGameID() => gameID;

		/// <summary>
		/// Returns whether the game has started.
		/// </summary>
		/// <returns><see langword="true"/> if the game has started; otherwise, <see langword="false"/>.</returns>
		public bool hasStarted() => started;

		/// <summary>
		/// Gets the maximum number of players that can join the game.
		/// </summary>
		public int getMaxPlayers() => maxPlayers;

		/// <summary>
		/// Gets the number of players currently in the game
		/// </summary>
		public int getPlayerCount() => userIDs.Count;
	}
}
