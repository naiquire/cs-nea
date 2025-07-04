using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class @knockout(string userID) : abstractGame(userID, 12), IPlayable
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
            await new connection().sendLetter(aliveUsers, letter);
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
            evaluate[] evaluates = new evaluate[getPlayerCount()];
            for (int i = 0; i < aliveUsers.Count; i++)
            {
                evaluateSubmission(ref evaluates, i, userIDs, letter);
                await new connection().sendResult(userIDs[i], stats[userIDs[i]]);
            }

            List<string> incorrectUsers = [];
            foreach (string user in stats.Keys)
            {
                if (!stats[user].correct[^1])
                {
                    incorrectUsers.Add(user);
                }
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

            await new connection().sendKnockoutResults(userIDs, aliveUsers);

            if (aliveUsers.Count > 1)
            {
                submissionPhase();
            }
        }
    }
}
