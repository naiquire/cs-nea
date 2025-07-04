using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class _1v1(string userID) : abstractGame(userID, 2), IPlayable
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
            await new connection().sendLetter(userIDs, letters[count]);
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
                await new connection().sendResult(userIDs[i], stats[userIDs[i]]);
            }


            /// <summary>
            /// currently this is NEARLY generalized for any amount of players so could be used for knockout. only problem is send1v1Result which needs to be reworked somehow
            /// could also pass the game type as a parameter if this was to be abstracted 
            /// </summary>

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
                await new connection().send1v1Results(userIDs, null);
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

                await new connection().send1v1Results(userIDs, lowest.user);
            }

            // call next submission phase
            if (count++ < 10)
            {
                submissionPhase();
            }
        }
    }
}


