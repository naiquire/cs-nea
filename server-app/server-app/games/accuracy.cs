using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class Accuracy(string userID, IHubContext<Connection> context) : Game(context, Games.Accuracy, userID, 1), IPlayable
	{
		private const int rounds = 10;
		public override async Task StartGame()
		{
			await base.StartGame();

			_letters = GenerateLetters(rounds);
			await SubmissionPhase();
		}

		public async Task SubmissionPhase()
		{
			if (_roundCount < rounds)
			{
				await AwaitRound();
				await Task.Delay(3000);

				_startTime = DateTime.UtcNow;
				_currentResponses.Clear();
				await SendLetter(userIDs, _letters[_roundCount]);
			}
			else
			{
				EndGame();
			}
		}
		public override void LoadResponse(string userID, byte[] input)
		{
			base.LoadResponse(userID, input);
			if (_currentResponses.Count == GetPlayerCount())
			{
				EvaluationPhase(_letters[_roundCount]);
			}
		}

		public async void EvaluationPhase(char letter)
		{
			for (int i = 0; i < GetPlayerCount(); i++)
			{
				EvaluateSubmission(userIDs[i], letter);
				await SendResult(userIDs[i], _gameStats[userIDs[i]]);
			}
			_roundCount++;
		}
		public async Task ContinueRequest(string userID)
		{
			if (!_continueRequests.Contains(userID))
			{
				_continueRequests.Add(userID);
			}

			if (_continueRequests.Count == GetPlayerCount())
			{
				await SubmissionPhase();
			}
		}
	}
}
