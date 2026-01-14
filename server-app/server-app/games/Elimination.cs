using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class Elimination(string userID, IHubContext<Connection> context) : Game(context, Games.Elimination, userID, 3), IPlayable
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
				char letter = (char)(_rnd.Next(0, 26) + 65);
				_letters.Add(letter);

				await AwaitRound();
				await Task.Delay(3000);

				_startTime = DateTime.UtcNow;
				_currentResponses.Clear();
				await SendLetter(aliveUsers, letter);
			}
			else
			{
				EndGame();
			}
		}
		protected override async Task AwaitRound()
		{
			_continueRequests.Clear();
			foreach (string userID in aliveUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
				}
			}
		}
		public override void LoadResponse(string userID, byte[] input)
		{
			base.LoadResponse(userID, input);
			if (_currentResponses.Count == aliveUsers.Count)
			{
				EvaluationPhase(_letters[^1]);
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
				await SendResult(aliveUsers[i], _gameStats[aliveUsers[i]]);
			}

			if (incorrectUsers.Count == 0)
			{
				// eliminate user with longest time
				(string user, TimeSpan time) highest = (string.Empty, TimeSpan.MinValue);
				foreach (string userID in aliveUsers)
				{
					var time = _gameStats[userID].time[_roundCount];
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
			_roundCount++;
		}
		public async Task ContinueRequest(string userID)
		{
			if (!aliveUsers.Contains(userID))
			{
				// if user is eliminated, then endGame for client
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
				return;
			}

			if (!_continueRequests.Contains(userID) && aliveUsers.Contains(userID))
			{
				_continueRequests.Add(userID);
			}

			if (_continueRequests.Count == aliveUsers.Count)
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
					await _hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers);
				}
			}
			foreach (string userID in eliminatedUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers);
				}
			}
		}
	}
}
