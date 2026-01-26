using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;

namespace server_app.games
{
	public class Versus(string userID, IHubContext<Connection> context) : Game(context, Games.Versus, userID, 2), IPlayable
	{
		private const int rounds = 10;
		private readonly List<friendData> userCache = [];
		private readonly Dictionary<string, double> scores = [];

		public override async Task StartGame()
		{
			foreach (string userID in userIDs)
			{
				scores[userID] = 0;
			}

			// cache user data to allow for rank updates after dequeue during game
			userCache.Add(userDatas[0]);
			userCache.Add(userDatas[1]);

			await base.StartGame();

			letters = GenerateLetters(rounds);
			await SubmissionPhase();
		}

		public async Task SubmissionPhase()
		{
			if (roundCount < rounds)
			{
				await AwaitRound();
				await Task.Delay(3000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await SendLetter(userIDs, letters[roundCount]);
			}
			else
			{
				EndGame();
			}
		}
		public override void LoadResponse(string userID, byte[] input)
		{
			base.LoadResponse(userID, input);
			if (currentResponses.Count == GetPlayerCount())
			{
				EvaluationPhase(letters[roundCount]);
			}
		}

		public async void EvaluationPhase(char letter)
		{
			List<string> correctUsers = [];

			for (int i = 0; i < userIDs.Count; i++)
			{
				if (EvaluateSubmission(userIDs[i], letter))
				{
					correctUsers.Add(userIDs[i]);
				}
				await SendResult(userIDs[i], gameStats[userIDs[i]]);
			}

			if (correctUsers.Count == 0)
			{
				// if none correct then a winner is not determined
				foreach (string userID in userIDs)
				{
					scores[userID] += 0.5;
				}

				await SendVersusResults(userIDs, null);
			}
			else
			{
				// otherwise the user with the lowest time who is also correct is the winner
				(string user, TimeSpan time) lowest = (string.Empty, TimeSpan.MaxValue);
				foreach (string userID in correctUsers)
				{
					var time = gameStats[userID].time[roundCount];
					if (time < lowest.time)
					{
						lowest = (userID, time);
					}
				}

				scores[lowest.user] += 1;
				await SendVersusResults(userIDs, lowest.user);
			}
			roundCount++;
		}
		public async Task ContinueRequest(string userID)
		{
			if (!continueRequests.Contains(userID))
			{
				continueRequests.Add(userID);
			}

			if (continueRequests.Count == GetPlayerCount())
			{
				await SubmissionPhase();
			}
		}

		public override async void EndGame()
		{
			for (int i = 0; i < userCache.Count; i++)
			{
				double expectedScore = 1.0 / (1 + Math.Pow(10, (i == 0 ? userCache[1].rank - userCache[0].rank : userCache[0].rank - userCache[1].rank) / 400.0));
				expectedScore *= rounds;

				int rank = CalculateRank(userCache[i], expectedScore);
				if (Database.UpdateRank(userCache[i].userID, rank))
				{
					if (Connection.map.TryGetValue(userCache[i].userID, out string? connectionID))
					{
						await hubContext.Clients.Client(connectionID).SendAsync("updateRank", rank);
					}
				}
				else
				{
					Database.outputException($"Failed to update user rank : <{userCache[i].userID}>");
				}
			}

			base.EndGame();
		}
		private int CalculateRank(friendData user, double expectedScore)
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
		private async Task SendVersusResults(List<string> userIDs, string? winner)
		{
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveVersusResult", winner);
				}
			}
		}
	}
}
