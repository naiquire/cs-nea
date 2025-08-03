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
			startTime = DateTime.UtcNow; // might be better to handle timing on users end to reduce the effect of latency but that feels vulnerable to cheats
			currentResponses.Clear();
			await sendLetter(userIDs, letters[count]);
		}
		public override void loadResponse(string userID, double[] input)
		{
			currentResponses.Add(userID, (input, DateTime.UtcNow));
			if (currentResponses.Count == getPlayerCount())
			{
				evaluationPhase(letters[count]);
			}
		}
		public async override void evaluationPhase(char letter)
		{
			evaluate[] evaluates = new evaluate[getPlayerCount()];
			for (int i = 0; i < userIDs.Count; i++)
			{
				evaluateSubmission(ref evaluates[i], userIDs[i], letter);
				await sendResult(userIDs[i], stats[userIDs[i]]);
			}

			count++;
			if (count < rounds)
			{
				submissionPhase();
			}
			else
			{
				endGame();
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
