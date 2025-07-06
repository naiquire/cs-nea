using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class _1v1(string userID, IHubContext<connection> context) : abstractGame(context, userID, 2), IPlayable
    {
        public const bool online = true;
        private int count = 0;
        public override void startGame()
        {
            base.startGame();
            submissionPhase();
        }
        public async void submissionPhase()
        {
            char letter = (char)(rnd.Next(0, 26) + 65);
            letters.Add(letter);

            startTime = DateTime.UtcNow; // might be better to handle timing on users end to reduce the effect of latency but that feels vulnerable to cheats
            await sendLetter(userIDs, letters[count]);
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
                evaluateSubmission(ref evaluates, i, userIDs, letter);
                await sendResult(userIDs[i], stats[userIDs[i]]);
            }

            List<string> correctUsers = [];
            foreach (string user in stats.Keys)
            {
                if (stats[user].correct[^1])
                {
                    correctUsers.Add(user);
                }
            }

            if (correctUsers.Count == 0)
            {
                // if none correct then a winner is not determined
                await send1v1Results(userIDs, null);
            }
            else
            {
                // otherwise the user with the lowest time who is also correct is the winner
                (string user, TimeSpan time) lowest = ("", TimeSpan.MaxValue);
                foreach (string userID in correctUsers)
                {
                    var time = stats[userID].time[letter];
                    if (time < lowest.time)
                    {
                        lowest = (userID, time);
                    }
                }

                await send1v1Results(userIDs, lowest.user);
            }

            // call next submission phase
            if (count++ < 10)
            {
                submissionPhase();
            }
        }

        /// <summary>
        /// Sends the unique results to users for the 1v1 game type.
        /// </summary>
        /// <param name="userIDs"></param>
        /// <param name="winner"></param>
        /// <returns></returns>
        /// <exception cref="DisconnectException"></exception>
        public async Task send1v1Results(List<string> userIDs, string? winner)
        {
            foreach (string userID in userIDs)
            {
                if (connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("receive1v1Result", winner);
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }
    }
}


