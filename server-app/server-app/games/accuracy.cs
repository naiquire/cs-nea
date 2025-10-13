using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Drawing;

namespace server_app.games
{
	public class @accuracy(string userID, IHubContext<connection> context) : abstractGame(context, "accuracy", userID, 1), IPlayable
	{
		private const int rounds = 10;
		public override async Task startGame()
		{
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
			evaluate[] evaluates = new evaluate[getPlayerCount()];
			for (int i = 0; i < userIDs.Count; i++)
			{
				evaluateSubmission(ref evaluates[i], userIDs[i], letter);
				await sendResult(userIDs[i], stats[userIDs[i]]);
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
	}
}
