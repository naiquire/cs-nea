using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class @knockout(string userID) : abstractGame(userID, 12)
    {
        private List<string> aliveUsers = [];
        public async override void startGame(string gameID)
        {
            base.startGame(gameID);
            aliveUsers = [.. userIDs];

            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < 10; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }
            while (aliveUsers.Count > 1)
            {
                // send letter to all
                letters.Add((char)(rnd.Next(0, 26) + 65));
                char letter = letters[^1];

                startTime = DateTime.UtcNow;
                await new connection().sendLetter(aliveUsers, letter);

                TaskCompletionSource<bool> receivedAll = new();
                await awaitResponses(receivedAll);

                bool allCorrect = true;
                evaluate[] evaluates = new evaluate[getPlayerCount()];
                for (int i = 0; i < aliveUsers.Count; i++)
                {
                    evaluates[i] = new evaluate(currentResponses[aliveUsers[i]].submission);
                    bool correct = evaluates[i].result == letter - 65;

                    if (stats.TryGetValue(aliveUsers[i], out stats currentStats))
                    {
                        currentStats.accuracy[i] = evaluates[i].activatedValues[evaluate.layerCount - 1][letter - 65];
                        currentStats.epochs++;
                        currentStats.correct += correct ? 1 : 0;
                        currentStats.time[i] = currentResponses[aliveUsers[i]].time - startTime;
                    }
                    stats[userIDs[i]] = currentStats;

                    await new connection().sendResults(aliveUsers[i], stats[aliveUsers[i]], correct);

                    if (!correct)
                    {
                        aliveUsers.RemoveAt(i);
                        allCorrect = false;
                    }
                }

                if (allCorrect)
                {
                    // remove slowest user
                }

                    //await new connection(). did user make it through current round
            }
            
        }
    }
}
