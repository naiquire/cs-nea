using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class Versus(string userID, IHubContext<Connection> context) : Game(context, "versus", userID, 2), IPlayable
	{
		private const int rounds = 1;
		private readonly List<friendData> _userCache = [];
		private readonly Dictionary<string, double> _scores = [];

		public override async Task StartGame()
		{
			foreach (string userID in userIDs)
			{
				_scores[userID] = 0;
			}

			// cache user data to allow for rank updates after dequeue during game
			_userCache.Add(userDatas[0]);
			_userCache.Add(userDatas[1]);

			await base.StartGame();

			letters = GenerateLetters(rounds);
			await SubmissionPhase();
		}
		public async Task SubmissionPhase()
		{
			if (roundCount < rounds)
			{
				continueRequests.Clear();
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
			if (currentResponses.Count == getPlayerCount())
			{
				EvaluationPhase(letters[roundCount]);
			}
		}
		public async void EvaluationPhase(char letter)
		{
			List<string> correctUsers = [];

			Network[] evaluates = new Network[getPlayerCount()];
			for (int i = 0; i < userIDs.Count; i++)
			{
				if (EvaluateSubmission(ref evaluates[i], userIDs[i], letter))
				{
					correctUsers.Add(userIDs[i]);
				}
				await SendResult(userIDs[i], stats[userIDs[i]]);
			}

			if (correctUsers.Count == 0)
			{
				// if none correct then a winner is not determined
				foreach (string userID in userIDs)
				{
					_scores[userID] += 0.5;
				}

				await SendVersusResults(userIDs, null);
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

				_scores[lowest.user] += 1;
				await SendVersusResults(userIDs, lowest.user);
			}
			roundCount++;
		}
		public override async Task ContinueRequest(string userID)
		{
			if (!continueRequests.Contains(userID)) continueRequests.Add(userID);
			if (continueRequests.Count == userIDs.Count)
			{
				await SubmissionPhase();
			}
		}
		public async override void EndGame()
		{
			for (int i = 0; i < _userCache.Count; i++)
			{
				double expectedScore = 1.0 / (1 + Math.Pow(10, (i == 0 ? _userCache[1].rank - _userCache[0].rank : _userCache[0].rank - _userCache[1].rank) / 400.0));
				expectedScore *= rounds;

				int rank = CalculateRank(_userCache[i], expectedScore);

				if (database.updateRank(_userCache[i].userID, rank))
				{
					if (Connection.map.TryGetValue(_userCache[i].userID, out string? connectionID))
					{
						await hubContext.Clients.Client(connectionID).SendAsync("updateRank", rank);
					}
				}
				else
				{
					database.outputException($"Failed to update user rank : <{_userCache[i].userID}>");
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

			return (int)(user.rank + k * (_scores[user.userID] - expectedScore));
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


