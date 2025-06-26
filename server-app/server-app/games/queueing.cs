using System.Runtime.CompilerServices;

namespace server_app.games
{
    // contains all instances of running games
    public static class @queueing
    {
        public readonly struct currentGames
        {
            public static readonly List<accuracy> accuracy = [];
            public static readonly List<_1v1> _1v1 = [];
            public static readonly List<knockout> knockout = [];

            public static readonly List<abstractGame> test = []; // maybe get this working somehow
        }
        private static bool queueGame(abstractGame game, string userID)
        {
            if (game.getPlayerCount() < game.getMaxPlayers())
            {
                game.queueUser(userID);
                if (game.getPlayerCount() == game.getMaxPlayers())
                {
                    game.runGame();
                    return true;
                }
            }
            return false;
        }
        public static void queue_accuracy(string userID)
        {
            foreach (var game in currentGames.accuracy)
            {
                if (queueGame(game, userID))
                {
                    // user has been successfully queued into a game
                    break;
                }
            }
            // no game found
            currentGames.accuracy.Add(new accuracy(userID));
        }
        public static void queue_1v1(string userID)
        {
            foreach (var game in currentGames._1v1)
            {
                if (queueGame(game, userID))
                {
                    // user has been successfully queued into a game
                    break;
                }
            }
            // no game found
            currentGames._1v1.Add(new _1v1(userID));
        }
        public static void queue_knockout(string userID)
        {
            foreach (var game in currentGames.knockout)
            {
                if (queueGame(game, userID))
                {
                    // user has been successfully queued into a game
                    break;
                }
            }
            // no game found
            currentGames.knockout.Add(new knockout(userID));
        }


        public static void loadSubmission(string gameID, string userID, double[] input)
        {
            // there's probably a neat way of doing this however i am stupid
            foreach (var game in currentGames.accuracy)
            {
                if (game.gameID == gameID)
                {
                    game.loadResponse(userID, input);
                    return;
                }
            }
            foreach (var game in currentGames._1v1)
            {
                if (game.gameID == gameID)
                {
                    game.loadResponse(userID, input);
                    return;
                }
            }
            foreach (var game in currentGames.knockout)
            {
                if (game.gameID == gameID)
                {
                    game.loadResponse(userID, input);
                    return;
                }
            }
        }
    }
}
