using server_app.connections;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class @knockout : abstractGame
    {
        private List<string> aliveUsers;
        public knockout(string userID) : base(userID, 12)
        {
            // tungsten cube
        }
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
                await new connection().sendLetter(aliveUsers, letter);

                bool receivedAll = false;
                while (!receivedAll)
                {
                    if (currentResponses.Count == getPlayerCount())
                    {
                        receivedAll = true;
                    }
                    Thread.Sleep(500); // this is probably a bad way of doing it but oh well
                }
            }
            
        }
    }
}
