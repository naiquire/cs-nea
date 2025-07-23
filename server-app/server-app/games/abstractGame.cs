using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;

namespace server_app.games
{
	public struct @stats
	{
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
		public void update(evaluate evaluate, int letter, TimeSpan time, bool correct)
		{
			this.accuracy.Add(evaluate.activatedValues[evaluate.layerCount - 1][letter]);
			this.correct.Add(correct);
			this.time.Add(time);
		}
	}
	public interface IPlayable
	{
		void queueUser(string userID);
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
		void endGame();
		string getType();
		string getGameID();
		int getPlayerCount();
		int getMaxPlayers();
	}
	public abstract class abstractGame : IPlayable
	{
		protected IHubContext<connection> hubContext;
		public string type;

		protected List<string> userIDs;
		protected List<friendData> userDatas;
		public string gameID;
		protected int maxPlayers;

		protected List<char> letters;
		protected Random rnd;
		protected int count = 0;

		protected DateTime startTime;
		protected Dictionary<string, stats> stats;
		protected Dictionary<string, (double[] submission, DateTime time)> currentResponses;

		/// <summary>
		/// Base initialisation for the game classes. Automatically queues the user into the respective game.
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

			gameID = userID + DateTime.UtcNow.ToString();
			queueUser(userID);
		}

		/// <summary>
		/// Queues a user into the current game and sends a confirmation to the user.
		/// </summary>
		/// <param name="userID"></param>
		/// <exception cref="DisconnectException"></exception>
		public async void queueUser(string userID)
		{
			if (database.loadFriendData(userID, out friendData data))
			{
				userDatas.Add(data);
			}

			if (connection.map.TryGetValue(userID, out string? connectionID))
			{
				await hubContext.Clients.Client(connectionID).SendAsync("receiveJoinConfirm", gameID, getType(), userDatas);
			}
			else
			{
				throw new DisconnectException(userID);
			}

		}

		/// <summary>
		/// Starts the current game and initialises values for statistics for each user.
		/// </summary>
		/// <exception cref="DisconnectException"></exception>
		public virtual async void startGame()
		{
			foreach (string user in userIDs)
			{
				stats.Add(user, new stats());
			}

			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("startGame", userIDs);
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
		public abstract void loadResponse(string userID, double[] input);
		public abstract void evaluationPhase(char letter);

		/// <summary>
		/// Evaluates a user's submission and updates their current statistics.
		/// </summary>
		/// <param name="evaluates"></param>
		/// <param name="i"></param>
		/// <param name="userIDs"></param>
		/// <param name="character"></param>
		/// <returns>A boolean value representing if the submission was correct.</returns>
		protected bool evaluateSubmission(ref evaluate[] evaluates, int i, List<string> userIDs, int character)
		{
			// evaluate the submission
			int letter = character - 65;
			evaluates[i] = new evaluate(currentResponses[userIDs[i]].submission);
			bool correct = evaluates[i].result == letter;

			// update the statistics for the current game
			if (stats.TryGetValue(userIDs[i], out stats currentStats))
			{
				DateTime endTime = currentResponses[userIDs[i]].time;
				currentStats.update(evaluates[i], letter, endTime - startTime, correct);
			}
			stats[userIDs[i]] = currentStats;
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
			if (connection.map.TryGetValue(userID, out string? connectionID))
			{
				await hubContext.Clients.Client(connectionID).SendAsync("receiveResults", stats);
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
		public virtual async void endGame()
		{
			// child classes handle different ways of displaying results

			async Task update(string userID)
			{
				for (int i = 0; i < letters.Count; i++)
				{
					char letter = letters[i];

					if (database.loadUserData(userID, out userData userData))
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
								await hubContext.Clients.Client(connectionID).SendAsync("updateStatistics", letter, updatedAccuracy, updatedTime, total + 1);
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
					else
					{
						database.outputException("Failed to retrieve statistics");
					}
				}
			}

			foreach (string userID in userIDs)
			{
				await update(userID);

				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveCurrentStatistics", stats);
				}
				else
				{
					throw new DisconnectException(userID);
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
		/// Gets the maximum number of players that can join the game.
		/// </summary>
		public int getMaxPlayers() => maxPlayers;

		/// <summary>
		/// Gets the number of players currently in the game
		/// </summary>
		public int getPlayerCount() => userIDs.Count;
	}

}
