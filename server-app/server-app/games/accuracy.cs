using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Reflection;

namespace server_app.games
{
    // 1 player game
    // measures time and accuracy only, basically training???
    public class @accuracy(string userID) : abstractGame(userID, 1)
    {
        public const bool online = false;

        public override async void runGame()
        {
            base.runGame();

            var letters = generateLetters(10);

            // for each letter send to client
            foreach (var letter in letters)
            {
                startTime = DateTime.UtcNow;
                await new connection().sendLetter(userIDs, letter);

                TaskCompletionSource<bool> receivedAll = new();
                await awaitResponses(receivedAll);

                evaluate[] evaluates = new evaluate[getPlayerCount()];
                for (int i = 0; i < userIDs.Count; i++)
                {
                    bool correct = evaluateSubmission(ref evaluates, i, userIDs, letter);
                    await new connection().sendResults(userIDs[i], stats[userIDs[i]], correct);
                }
            }            
        }
    }
}
