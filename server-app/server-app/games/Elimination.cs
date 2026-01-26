using Microsoft.AspNetCore.SignalR;
using server_app.connections;

namespace server_app.games
{
	public class Elimination(string userID, IHubContext<Connection> context) : Game(context, Games.Elimination, userID, 12), IPlayable
	{
		private List<string> aliveUsers = [];
		public override bool DequeueUser(string userID)
		{
			aliveUsers.Remove(userID);
			return base.DequeueUser(userID);
		}
		public override async Task StartGame()
		{
			aliveUsers = [.. userIDs];

			await base.StartGame();
			await SubmissionPhase();
		}

		public async Task SubmissionPhase()
		{
			if (aliveUsers.Count > 1)
			{
				char letter = (char)(rnd.Next(0, 26) + 65);
				letters.Add(letter);

				await AwaitRound();
				await Task.Delay(3000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await SendLetter(aliveUsers, letter);
			}
			else
			{
				EndGame();
			}
		}
		protected override async Task AwaitRound()
		{
			continueRequests.Clear();
			foreach (string userID in aliveUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
				}
			}
		}
		public override void LoadResponse(string userID, byte[] input)
		{
			base.LoadResponse(userID, input);
			if (currentResponses.Count == aliveUsers.Count)
			{
				EvaluationPhase(letters[^1]);
			}
		}

		public async void EvaluationPhase(char letter)
		{
			List<string> eliminatedUsers = [];
			List<string> incorrectUsers = [];

			for (int i = 0; i < aliveUsers.Count; i++)
			{
				if (!EvaluateSubmission(aliveUsers[i], letter))
				{
					incorrectUsers.Add(aliveUsers[i]);
				}
				await SendResult(aliveUsers[i], gameStats[aliveUsers[i]]);
			}

			if (incorrectUsers.Count == 0)
			{
				// eliminate user with longest time
				(string user, TimeSpan time) highest = (string.Empty, TimeSpan.MinValue);
				foreach (string userID in aliveUsers)
				{
					var time = gameStats[userID].time[roundCount];
					if (time > highest.time)
					{
						highest = (userID, time);
					}
				}

				eliminatedUsers.Add(highest.user);

				aliveUsers.Remove(highest.user);
			}
			else if (incorrectUsers.Count < aliveUsers.Count)
			{
				// eliminate incorrect users
				eliminatedUsers.AddRange(incorrectUsers);
				foreach (string user in incorrectUsers)
				{
					aliveUsers.Remove(user);
				}
			}

			await SendEliminationResults(eliminatedUsers);
			roundCount++;
		}
		public async Task ContinueRequest(string userID)
		{
			if (!aliveUsers.Contains(userID))
			{
				// if user is eliminated, then endGame for client
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
				return;
			}

			if (!continueRequests.Contains(userID) && aliveUsers.Contains(userID))
			{
				continueRequests.Add(userID);
			}

			if (continueRequests.Count == aliveUsers.Count)
			{
				await SubmissionPhase();
			}
		}
		private async Task SendEliminationResults(List<string> eliminatedUsers)
		{
			foreach (string userID in aliveUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers);
				}
			}
			foreach (string userID in eliminatedUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers);
				}
			}
		}
	}
}
