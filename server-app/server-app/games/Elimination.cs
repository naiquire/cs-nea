using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Runtime.CompilerServices;

namespace server_app.games
{
	public class Elimination(string userID, IHubContext<Connection> context) : Game(context, Games.Knockout, userID, 3), IPlayable
	{
		private List<string> _aliveUsers = [];
		public override async Task StartGame()
		{
			_aliveUsers = [.. userIDs];

			await base.StartGame();
			await SubmissionPhase();
		}
		public override void DequeueUser(string userID)
		{
			_aliveUsers.Remove(userID);
			base.DequeueUser(userID);
		}
		protected override async Task AwaitRound()
		{
			continueRequests.Clear();
			foreach (string userID in _aliveUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
				}
			}
		}
		public async Task SubmissionPhase()
		{
			if (_aliveUsers.Count > 1)
			{
				continueRequests.Clear();

				char letter = (char)(rnd.Next(0, 26) + 65);
				letters.Add(letter);

				await AwaitRound();
				await Task.Delay(3000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await SendLetter(_aliveUsers, letter);
			}
			else
			{
				EndGame();
			}
		}
		public override void LoadResponse(string userID, byte[] input)
		{
			base.LoadResponse(userID, input);
			if (currentResponses.Count == _aliveUsers.Count)
			{
				EvaluationPhase(letters[^1]);
			}
		}
		public async void EvaluationPhase(char letter)
		{
			List<string> eliminatedUsers = [];

			List<string> incorrectUsers = [];
			Network[] evaluates = new Network[getPlayerCount()];
			for (int i = 0; i < _aliveUsers.Count; i++)
			{
				if (!EvaluateSubmission(ref evaluates[i], _aliveUsers[i], letter))
				{
					incorrectUsers.Add(_aliveUsers[i]);
				}
				await SendResult(_aliveUsers[i], gameStats[_aliveUsers[i]]);
			}

			if (incorrectUsers.Count == 0)
			{
				// eliminate user with longest time
				(string user, TimeSpan time) highest = (string.Empty, TimeSpan.MinValue);
				foreach (string userID in _aliveUsers)
				{
					var time = gameStats[userID].time[roundCount];
					if (time > highest.time)
					{
						highest = (userID, time);
					}
				}

				eliminatedUsers.Add(highest.user);

				_aliveUsers.Remove(highest.user);
			}
			else if (incorrectUsers.Count < _aliveUsers.Count)
			{
				// eliminate incorrect users
				eliminatedUsers.AddRange(incorrectUsers);
				foreach (string user in incorrectUsers)
				{
					_aliveUsers.Remove(user);
				}
			}

			await SendKnockoutResults(eliminatedUsers);
            roundCount++;
        }
		public async Task ContinueRequest(string userID)
		{
			if (!_aliveUsers.Contains(userID))
			{
				// if user is eliminated, then endGame for client
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
				return;
			}

			if (!continueRequests.Contains(userID) && _aliveUsers.Contains(userID))
			{
				continueRequests.Add(userID);
			}

			if (continueRequests.Count == _aliveUsers.Count)
			{
				await SubmissionPhase();
			}
		}
		private async Task SendKnockoutResults(List<string> eliminatedUsers)
		{
			foreach (string userID in _aliveUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", _aliveUsers);
				}
			}
			foreach (string userID in eliminatedUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", _aliveUsers);
				}
			}
		}
	}
}
