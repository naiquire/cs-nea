using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
	public class _1v1(string userID, IHubContext<connection> context) : abstractGame(context, userID, 2), IPlayable
	{
		public const bool online = true;
		private const int rounds = 10;
		private int count = 0;
		private Dictionary<string, double> scores = [];
		public override void startGame()
		{
			base.startGame();

			foreach (string userID in userIDs)
			{
				scores[userID] = 0;
			}

			letters = generateLetters(rounds);
			submissionPhase();
		}
		public async void submissionPhase()
		{
			startTime = DateTime.UtcNow; // might be better to handle timing on users end to reduce the effect of latency but that feels vulnerable to cheats
			currentResponses.Clear();
			await sendLetter(userIDs, letters[count]);
		}
		public void loadResponse(string userID, double[] input)
		{
			currentResponses.Add(userID, (input, DateTime.UtcNow));
			if (currentResponses.Count == getPlayerCount())
			{
				evaluationPhase(letters[count]);
			}
		}
		public async void evaluationPhase(char letter)
		{
			evaluate[] evaluates = new evaluate[getPlayerCount()];
			for (int i = 0; i < userIDs.Count; i++)
			{
				evaluateSubmission(ref evaluates, i, userIDs, letter);
				await sendResult(userIDs[i], stats[userIDs[i]]);
			}

			List<string> correctUsers = [];
			foreach (string user in stats.Keys)
			{
				if (stats[user].correct[^1])
				{
					correctUsers.Add(user);
				}
			}

			if (correctUsers.Count == 0)
			{
				// if none correct then a winner is not determined
				
				foreach (string userID in userIDs)
				{
					scores[userID] += 0.5;
				}

				await send1v1Results(userIDs, null);
			}
			else
			{
				// otherwise the user with the lowest time who is also correct is the winner
				(string user, TimeSpan time) lowest = ("", TimeSpan.MaxValue);
				foreach (string userID in correctUsers)
				{
					var time = stats[userID].time[letter];
					if (time < lowest.time)
					{
						lowest = (userID, time);
					}
				}

				scores[lowest.user] += 1;
				await send1v1Results(userIDs, lowest.user);
			}

			// call next submission phase
			if (count++ < rounds)
			{
				submissionPhase();
			}
		}
		public async override void endGame()
		{
			userData[] userData = new userData[userIDs.Count];
			for (int i = 0; i < userIDs.Count; i++)
			{
				if (!database.loadUserData(userIDs[i], out userData[i]))
				{
					database.outputException($"Failed to get userData for rank update: <{userIDs[i]}>");
				}
			}

			for (int i = 0; i < userIDs.Count; i++)
			{
				double expScore = 1.0 / (1 + Math.Pow(10, (userData[i].rank - i == 0 ? userData[1].rank : userData[0].rank) / 400));

				int k;
				if (userData[i].rank < 2100)
				{
					k = 32;
				}
				else if (userData[i].rank > 2400)
				{
					k = 16;
				}
				else
				{
					k = 24;
				}

				int rank = (int)(userData[i].rank + k * (scores[userIDs[i]] - expScore));

				if (database.updateRank(userIDs[i], rank))
				{
					if (connection.map.TryGetValue(userIDs[i], out string? connectionID))
					{
						await hubContext.Clients.Client(connectionID).SendAsync("end1v1", rank);
					}
					else
					{
						throw new DisconnectException(userIDs[i]);
					}
				}
				else
				{
					database.outputException($"Failed to update user rank : <{userIDs[i]}>");
				}
			}

			base.endGame();
		}

		/// <summary>
		/// Sends the unique results to users for the 1v1 game type.
		/// </summary>
		/// <param name="userIDs"></param>
		/// <param name="winner"></param>
		/// <returns></returns>
		/// <exception cref="DisconnectException"></exception>
		public async Task send1v1Results(List<string> userIDs, string? winner)
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receive1v1Result", winner);
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}
		}
	}
}


