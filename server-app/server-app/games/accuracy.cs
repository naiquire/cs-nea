using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class Accuracy(string userID, IHubContext<Connection> context) : Game(context, "accuracy", userID, 1), IPlayable
	{
		private const int rounds = 10;
		public override async Task StartGame()
		{
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
			Network[] evaluates = new Network[getPlayerCount()];
			for (int i = 0; i < userIDs.Count; i++)
			{
				EvaluateSubmission(ref evaluates[i], userIDs[i], letter);
				await SendResult(userIDs[i], stats[userIDs[i]]);
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
	}
}
