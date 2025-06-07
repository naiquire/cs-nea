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
            foreach (var letter in letters)
            {
                // send letter to all

                startTime = DateTime.UtcNow;
                await new connection().sendLetter("knockout", aliveUsers, letter);

                bool receivedAll = false;
                while (!receivedAll)
                {
                    if (currentResponses.Count == getPlayerCount())
                    {
                        receivedAll = true;
                    }
                    Thread.Sleep(500); // this is probably a bad way of doing it but oh well
                }
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
                    }
                }
            }
            
        }
    }
}
