using server_app.connections;
using server_app.neuralNetwork;

namespace server_app.games
{
    public class _1v1(string userID) : abstractGame(userID, 2)
    {
        public const bool online = true;

        public override async void runGame()
        {
            base.runGame();

            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < 10; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }

            // for each letter send to client
            foreach (var letter in letters)
            {
                startTime = DateTime.UtcNow;
                await new connection().sendLetter(userIDs, letters[letter]);

                TaskCompletionSource<bool> receivedAll = new();
                await awaitResponses(receivedAll);

                evaluate[] evaluates = new evaluate[getPlayerCount()];
                for (int i = 0; i < userIDs.Count; i++)
                {
                    bool correct = evaluateSubmission(ref evaluates, i, userIDs, letter);
                    await new connection().sendResults(userIDs[i], stats[userIDs[i]], correct);
                }

                // kill whoever got it wrong as well TODO                                                 ---------------------------------------------------------
                // who was first
                (string user, TimeSpan time) lowest = ("", TimeSpan.MaxValue);
                foreach (string userID in userIDs)
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
