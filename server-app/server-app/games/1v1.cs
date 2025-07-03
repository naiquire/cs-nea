using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class _1v1(string userID) : abstractGame(userID, 2)
    {
        public const bool online = true;
        /// <summary>
        /// Sets up and starts the current game.
        /// </summary>
        public override async void runGame()
        {
            base.runGame();

            var letters = generateLetters(10);

            // for each letter send to client
            foreach (var letter in letters)
            {
                startTime = DateTime.UtcNow; // might be better to handle timing on users end to reduce the effect of latency but that feels vulnerable to cheats
                await new connection().sendLetter(userIDs, letters[letter]);

                // wait for everything to come in somehow

                evaluate[] evaluates = new evaluate[getPlayerCount()];
                for (int i = 0; i < userIDs.Count; i++)
                {
                    bool correct = evaluateSubmission(ref evaluates, i, userIDs, letter);
                    await new connection().sendResults(userIDs[i], stats[userIDs[i]]);
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
                    await new connection().send1v1Result(userIDs, null);
                }
                else
                {
                    (string user, TimeSpan time) lowest = ("", TimeSpan.MaxValue);
                    foreach (string userID in correctUsers)
                    {
                        var time = stats[userID].time[letter];
                        if (time < lowest.time)
                        {
                            lowest = (userID, time);
                        }
                    }

                    await new connection().send1v1Result(userIDs, lowest.user);
                }
            }
        }
    }
}


