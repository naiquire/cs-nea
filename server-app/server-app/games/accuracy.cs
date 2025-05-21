using server_app.connections;
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
            if (getPlayerCount() == getMaxPlayers())
            {
                startGame();
            }
        }
        public override async void startGame()
        {
            base.startGame();
            MethodInfo? methodInfo = typeof(connection).GetMethod("sendStartRequest") ?? throw new Exception("Method not found");
            methodInfo.Invoke(methodInfo, [userIDs]);

            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < 10; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }

            for (int i = 0; i < 10; i++)
            {
                methodInfo = typeof(connection).GetMethod("sendLetter") ?? throw new Exception("Method not found");
                methodInfo.Invoke(methodInfo, [userIDs, letters[i]]);

            }

        }
    }
}
