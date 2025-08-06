using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace server_app.games
{
	public class @accuracy(string userID, IHubContext<connection> context) : abstractGame(context, "accuracy", userID, 1), IPlayable
	{
		private const int rounds = 10;
		public override void startGame()
		{
			base.startGame();
			 
			letters = generateLetters(rounds);
			submissionPhase();
		}
		public async override void submissionPhase()
		{
			if (count < rounds)
			{
				await awaitRound();
				Thread.Sleep(5000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await sendLetter(userIDs, letters[count]);
				count++;
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
				evaluationPhase(letters[count]);
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
		}
		public void continueRequest(string userID)
		{
			continueRequests.Add(userID);
			if (continueRequests.Count == userIDs.Count)
			{
				submissionPhase();
			}
		}
		public async override void endGame()
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("endAccuracy");
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}

			base.endGame();
		}
	}
}
