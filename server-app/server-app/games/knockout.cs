using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace server_app.games
{
	public class @knockout(string userID, IHubContext<connection> context) : abstractGame(context, "knockout", userID, 2), IPlayable
	{
		private List<string> aliveUsers = [];
		public override async Task startGame()
		{
			aliveUsers = [.. userIDs];

			await base.startGame();
			await submissionPhase();
		}
		public override void dequeueUser(string userID)
		{
			aliveUsers.Remove(userID);
			base.dequeueUser(userID);
		}
		public override async Task submissionPhase()
		{
			if (aliveUsers.Count > 1)
			{
				continueRequests.Clear();

				char letter = (char)(rnd.Next(0, 26) + 65);
				letters.Add(letter);

				await awaitRound();
				await Task.Delay(3000);

				startTime = DateTime.UtcNow;
				currentResponses.Clear();
				await sendLetter(aliveUsers, letter);
			}
			else
			{
				endGame();
			}
		}
		public void loadResponse(string userID, byte[] input)
		{
			DateTime end = DateTime.UtcNow;
			var ms = new MemoryStream(input);
			var bmp = new Bitmap(ms);
			double[] array = data.preprocessImage(bmp);
			currentResponses.Add(userID, (array, end));
			if (currentResponses.Count == aliveUsers.Count)
			{
				evaluationPhase(letters[^1]);
			}
		}
		public async void evaluationPhase(char letter)
		{
			List<string> incorrectUsers = [];
			evaluate[] evaluates = new evaluate[getPlayerCount()];
			for (int i = 0; i < aliveUsers.Count; i++)
			{
				if (!evaluateSubmission(ref evaluates[i], userIDs[i], letter))
				{
					incorrectUsers.Add(userIDs[i]);
				}
				await sendResult(userIDs[i], stats[userIDs[i]]);
			}

			if (incorrectUsers.Count == 0)
			{
				// eliminate user with longest time
				(string user, TimeSpan time) highest = ("", TimeSpan.MinValue);
				foreach (string userID in aliveUsers)
				{
					var time = stats[userID].time[roundCount];
					if (time > highest.time)
					{
						highest = (userID, time);
					}
				}

				aliveUsers.Remove(highest.user);
			}
			else if (incorrectUsers.Count < aliveUsers.Count)
			{
				// eliminate incorrect users
				foreach (string user in incorrectUsers)
				{
					aliveUsers.Remove(user);
				}
			}

			await sendKnockoutResults(userIDs, aliveUsers);
            roundCount++;
        }
		public override async Task continueRequest(string userID)
		{
			continueRequests.Add(userID);
			if (continueRequests.Count == aliveUsers.Count)
			{
				await submissionPhase();
			}
		}

		public async Task sendKnockoutResults(List<string> userIDs, List<string> aliveUsers)
		{
			foreach (string userID in userIDs)
			{
				if (connection.map.TryGetValue(userID, out string? connectionID))
				{
					await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers);
				}
				else
				{
					throw new DisconnectException(userID);
				}
			}
		}
	}
}
