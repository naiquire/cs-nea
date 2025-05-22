using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Reflection;

namespace server_app.games
{
    // 1 player game
    // measures time and accuracy only, basically training???
    public class @accuracy : abstractGame
    {
        public const bool online = false;
        
        public accuracy(string userID) : base(userID, 1)
        {
            // balls
        }
        public override async void startGame(string gameID)
        {
            base.startGame(gameID);

            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < 10; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }

            // for each letter send to client
            foreach (var letter in letters)
            {
                await new connection().sendLetter(userIDs, letter);

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
                for (int i = 0; i < userIDs.Count; i++)
                {
                    evaluates[i] = new evaluate(currentResponses[userIDs[i]]);

                    if (stats.TryGetValue(userIDs[i], out stats currentStats))
                    {
                        currentStats.accuracy[i] = evaluates[i].activatedValues[evaluate.layerCount - 1][letter - 65];
                        currentStats.epochs++;
                        currentStats.correct += evaluates[i].result == letter + 65 ? 1 : 0;
                        currentStats.time[i] = TimeSpan.Zero; // temp
                    }
                    stats[userIDs[i]] = currentStats;


                  
                    await new connection().sendResults(userIDs[i], stats[userIDs[i]], evaluates[i].result == letter + 65);

                    // figure out time later
                    // send back accuracy
                    // store in stats
                }
            }
                // get response from all clients
            
        }
    }
}
