using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
	public class @versus(string userID, IHubContext<connection> context) : abstractGame(context, "versus", userID, 2), IPlayable
	{
		private const int rounds = 5;
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
		public async override void submissionPhase()
		{
			
			if (count < rounds)
			{
				continueRequests.Clear();
				await awaitRound();
				Thread.Sleep(5000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await sendLetter(userIDs, letters[count]);
			}
			else
			{
				endGame();
			}
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
			List<string> correctUsers = [];

			evaluate[] evaluates = new evaluate[getPlayerCount()];
			for (int i = 0; i < userIDs.Count; i++)
			{
				if (evaluateSubmission(ref evaluates[i], userIDs[i], letter))
				{
					correctUsers.Add(userIDs[i]);
				}
				await sendResult(userIDs[i], stats[userIDs[i]]);
			}

			if (correctUsers.Count == 0)
			{
				// if none correct then a winner is not determined
				
				foreach (string userID in userIDs)
				{
					scores[userID] += 0.5;
				}

				await sendVersusResults(userIDs, null);
			}
			else
			{
				// otherwise the user with the lowest time who is also correct is the winner
				(string user, TimeSpan time) lowest = ("", TimeSpan.MaxValue);
				foreach (string userID in correctUsers)
				{
					var time = stats[userID].time[count];
					if (time < lowest.time)
					{
						lowest = (userID, time);
					}
				}

				scores[lowest.user] += 1;
				await sendVersusResults(userIDs, lowest.user);
			}
			count++;
		}
		public void continueRequest(string userID)
		{
			continueRequests.Add(userID);
			if (continueRequests.Count == userIDs.Count)
			{
				submissionPhase();
			}
		}
		public async override void endGame()
		{
			for (int i = 0; i < userIDs.Count; i++)
			{
				double expScore;
				if (i == 0)
				{
					expScore = 1.0 / (1 + Math.Pow(10, (userDatas[0].rank - userDatas[1].rank) / 400));
				}
				else if (i == 1)
				{
					expScore = 1.0 / (1 + Math.Pow(10, (userDatas[1].rank - userDatas[0].rank) / 400));
				}
				else
				{
					expScore = 0;
				}

				expScore *= rounds;

				int rank = calculateRank(userDatas[i], expScore);

				if (database.updateRank(userIDs[i], rank))
				{
					if (connection.map.TryGetValue(userIDs[i], out string? connectionID))
					{
						await hubContext.Clients.Client(connectionID).SendAsync("updateRank", rank);
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

			int calculateRank(friendData user, double expScore)
			{
				int k;
				if (user.rank < 2100)
				{
					k = 32;
				}
				else if (user.rank > 2400)
				{
					k = 16;
				}
				else
				{
					k = 24;
				}

				return (int)(user.rank + k * (scores[user.userID] - expScore));
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
		public async Task sendVersusResults(List<string> userIDs, string? winner)
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveVersusResult", winner);
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}
		}
	}
}


