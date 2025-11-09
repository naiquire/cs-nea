using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class Elimination(string userID, IHubContext<Connection> context) : Game(context, "knockout", userID, 2), IPlayable
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
			List<string> incorrectUsers = [];
			Network[] evaluates = new Network[getPlayerCount()];
			for (int i = 0; i < _aliveUsers.Count; i++)
			{
				if (!EvaluateSubmission(ref evaluates[i], userIDs[i], letter))
				{
					incorrectUsers.Add(userIDs[i]);
				}
				await SendResult(userIDs[i], stats[userIDs[i]]);
			}

			if (incorrectUsers.Count == 0)
			{
				// eliminate user with longest time
				(string user, TimeSpan time) highest = (string.Empty, TimeSpan.MinValue);
				foreach (string userID in _aliveUsers)
				{
					var time = stats[userID].time[roundCount];
					if (time > highest.time)
					{
						highest = (userID, time);
					}
				}

				_aliveUsers.Remove(highest.user);
			}
			else if (incorrectUsers.Count < _aliveUsers.Count)
			{
				// eliminate incorrect users
				foreach (string user in incorrectUsers)
				{
					_aliveUsers.Remove(user);
				}
			}

			await SendKnockoutResults(userIDs, _aliveUsers);
            roundCount++;
        }
		public override async Task ContinueRequest(string userID)
		{
			if (!continueRequests.Contains(userID)) continueRequests.Add(userID);
			if (continueRequests.Count == _aliveUsers.Count)
			{
				await SubmissionPhase();
			}
		}
		private async Task SendKnockoutResults(List<string> userIDs, List<string> aliveUsers)
		{
			foreach (string userID in userIDs)
			{
				if (Connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers);
				}
			}
		}
	}
}
