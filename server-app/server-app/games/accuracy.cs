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
        public override async void startGame()
        {
            base.startGame();

            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < 10; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }

            await new connection().accuracyGame(userIDs);

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
                }
                evaluate[] evaluates = new evaluate[getPlayerCount()];
                for (int i = 0; i < currentResponses.Count; i++)
                {
                    evaluates[i] = new evaluate(currentResponses[userIDs[i]]);
                    // figure out time later
                    // send back accuracy
                    // store in stats
                }
            }
                // get response from all clients
            
        }
    }
}
