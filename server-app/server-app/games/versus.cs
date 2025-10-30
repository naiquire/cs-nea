using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;
using System.Drawing;

namespace server_app.games
{
	public class @versus(string userID, IHubContext<connection> context) : abstractGame(context, "versus", userID, 2), IPlayable
	{
		private const int rounds = 10;
		private List<friendData> userCache = [];
		private readonly Dictionary<string, double> scores = [];

		public override async Task startGame()
		{
			foreach (string userID in userIDs)
			{
				scores[userID] = 0;
			}

			// cache user data to allow for rank updates after dequeue
			userCache.Add(userDatas[0]);
			userCache.Add(userDatas[1]);

			await base.startGame();

			letters = generateLetters(rounds);
			await submissionPhase();
		}
		public override async Task submissionPhase()
		{
			if (roundCount < rounds)
			{
				continueRequests.Clear();
				await awaitRound();
				await Task.Delay(3000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await sendLetter(userIDs, letters[roundCount]);
			}
			else
			{
				endGame();
			}
		}
		public override void loadResponse(string userID, byte[] input)
		{
			base.loadResponse(userID, input);
			if (currentResponses.Count == getPlayerCount())
			{
				evaluationPhase(letters[roundCount]);
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
				(string user, TimeSpan time) lowest = (string.Empty, TimeSpan.MaxValue);
				foreach (string userID in correctUsers)
				{
					var time = stats[userID].time[roundCount];
					if (time < lowest.time)
					{
						lowest = (userID, time);
					}
				}

				scores[lowest.user] += 1;
				await sendVersusResults(userIDs, lowest.user);
			}
			roundCount++;
		}
		public override async Task continueRequest(string userID)
		{
			if (!continueRequests.Contains(userID)) continueRequests.Add(userID);
			if (continueRequests.Count == userIDs.Count)
			{
				await submissionPhase();
			}
		}
		public async override void endGame()
		{
			for (int i = 0; i < userIDs.Count; i++)
			{
				double expectedScore = 1.0 / 1 + Math.Pow(10, (i == 0 ? 1 : -1) * userCache[0].rank - userCache[1].rank) / 400;
				expectedScore *= rounds;

				int rank = calculateRank(userCache[i], expectedScore);

				if (database.updateRank(userIDs[i], rank))
				{
					if (connection.map.TryGetValue(userIDs[i], out string? connectionID))
					{
						await hubContext.Clients.Client(connectionID).SendAsync("updateRank", rank);
					}
				}
				else
				{
					database.outputException($"Failed to update user rank : <{userIDs[i]}>");
				}
			}

			base.endGame();
		}

		private int calculateRank(friendData user, double expectedScore)
		{
			double k;
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

			k /= rounds;

			return (int)(user.rank + k * (scores[user.userID] - expectedScore));
		}

		private async Task sendVersusResults(List<string> userIDs, string? winner)
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveVersusResult", winner);
				}
			}
		}
	}
}


