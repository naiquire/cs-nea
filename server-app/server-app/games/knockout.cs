using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class @knockout(string userID, IHubContext<connection> context) : abstractGame(context, userID, 12), IPlayable
    {
        public const bool online = true;
        private List<string> aliveUsers = [];
        public override void startGame()
        {
            aliveUsers = [.. userIDs];

            base.startGame();
            submissionPhase();          
        }
        public async void submissionPhase()
        {
            char letter = (char)(rnd.Next(0, 26) + 65);
            letters.Add(letter);

            startTime = DateTime.UtcNow;
            currentResponses.Clear();
            await sendLetter(aliveUsers, letter);
        }
        public void loadResponse(string userID, double[] input)
        {
            currentResponses.Add(userID, (input, DateTime.UtcNow));
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
                bool correct = evaluateSubmission(ref evaluates, i, userIDs, letter);
                if (!correct)
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
                    var time = stats[userID].time[letter];
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

            if (aliveUsers.Count > 1)
            {
                submissionPhase();
            }
        }

        /// <summary>
        /// Sends the unique results to users for the knockout game type.
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="aliveUsers"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task sendKnockoutResults(List<string> userIDs, List<string> aliveUsers)
        {
            foreach (string userID in userIDs)
            {
                if (connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("receiveKnockoutResult", aliveUsers.Contains(userID));
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }
    }
}
