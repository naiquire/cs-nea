using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
	public class @accuracy(string userID, IHubContext<connection> context) : abstractGame(context, "accuracy", userID, 1), IPlayable
	{
		private const int rounds = 10;
		public override void startGame()
		{
			base.startGame();

			letters = generateRandomLetters(rounds);
			submissionPhase();
		}
		public async override void submissionPhase()
		{
			if (roundCount < rounds)
			{
				continueRequests.Clear();
				await awaitRound();
				Thread.Sleep(3000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await sendLetter(userIDs, letters[roundCount]);
			}
			else
			{
				endGame();
			}
		}
		public void loadResponse(string userID, double[] input)
		{
			currentResponses.Add(userID, (input, DateTime.UtcNow));
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
		public override void continueRequest(string userID)
		{
			continueRequests.Add(userID);
			if (continueRequests.Count == userIDs.Count)
			{
				submissionPhase();
			}
		}
	}
}
