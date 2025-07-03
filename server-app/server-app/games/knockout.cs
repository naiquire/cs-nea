using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class @knockout(string userID) : abstractGame(userID, 12)
    {
        public const bool online = true;
        private List<string> aliveUsers = [];
        /// <summary>
        /// Sets up and starts the current game.
        /// </summary>
        public async override void runGame()
        {
            base.runGame();
            aliveUsers = [.. userIDs];

            List<char> letters = [];

            Random rnd = new();

            while (aliveUsers.Count > 1)
            {
                // send letter to all
                char letter = (char)(rnd.Next(0, 26) + 65);
                letters.Add(letter);

                startTime = DateTime.UtcNow;
                await new connection().sendLetter(aliveUsers, letter);

                TaskCompletionSource<bool> receivedAll = new();
                await awaitResponses(receivedAll);

                bool allCorrect = true;
                evaluate[] evaluates = new evaluate[getPlayerCount()];
                for (int i = 0; i < aliveUsers.Count; i++)
                {
                    bool correct = evaluateSubmission(ref evaluates, i, userIDs, letter);

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
