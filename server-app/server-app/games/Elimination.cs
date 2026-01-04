using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class Elimination(string userID, IHubContext<Connection> context) : Game(context, Games.Elimination, userID, 2), IPlayable
	{
		private List<string> _aliveUsers = [];
		public override bool DequeueUser(string userID)
		{
			_aliveUsers.Remove(userID);
			return base.DequeueUser(userID);
		}
		public override async Task StartGame()
		{
			_aliveUsers = [.. _userIDs];

			await base.StartGame();
			await SubmissionPhase();
		}		

		public async Task SubmissionPhase()
		{
			if (_aliveUsers.Count > 1)
			{
				char letter = (char)(_rnd.Next(0, 26) + 65);
				_letters.Add(letter);

				await AwaitRound();
				await Task.Delay(3000);

				_startTime = DateTime.UtcNow;
				_currentResponses.Clear();
				await SendLetter(_aliveUsers, letter);
			}
			else
			{
				EndGame();
			}
		}
		protected override async Task AwaitRound()
		{
			_continueRequests.Clear();
			foreach (string userID in _aliveUsers)
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
			if (_currentResponses.Count == _aliveUsers.Count)
			{
				EvaluationPhase(_letters[^1]);
			}
		}

		public async void EvaluationPhase(char letter)
		{
			List<string> eliminatedUsers = [];

			List<string> incorrectUsers = [];
			Network[] evaluates = new Network[_aliveUsers.Count];
			for (int i = 0; i < _aliveUsers.Count; i++)
			{
				if (!EvaluateSubmission(ref evaluates[i], _aliveUsers[i], letter))
				{
					incorrectUsers.Add(_aliveUsers[i]);
				}
				await SendResult(_aliveUsers[i], _gameStats[_aliveUsers[i]]);
			}

			if (incorrectUsers.Count == 0)
			{
				// eliminate user with longest time
				(string user, TimeSpan time) highest = (string.Empty, TimeSpan.MinValue);
				foreach (string userID in _aliveUsers)
				{
					var time = _gameStats[userID].time[_roundCount];
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

			await SendEliminationResults(eliminatedUsers);
			_roundCount++;
		}
		public async Task ContinueRequest(string userID)
		{
			if (!_aliveUsers.Contains(userID))
			{
				// if user is eliminated, then endGame for client
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("endGame");
				}
				return;
			}

			if (!_continueRequests.Contains(userID) && _aliveUsers.Contains(userID))
			{
				_continueRequests.Add(userID);
			}

			if (_continueRequests.Count == _aliveUsers.Count)
			{
				await SubmissionPhase();
			}
		}
		private async Task SendEliminationResults(List<string> eliminatedUsers)
		{
			foreach (string userID in _aliveUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", _aliveUsers);
				}
			}
			foreach (string userID in eliminatedUsers)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await _hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", _aliveUsers);
				}
			}
		}
	}
}
