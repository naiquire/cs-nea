using System.Reflection;

namespace server_app.games
{
    // contains all instances of running MULTIPLAYER games
    public static class @queueing
    {
        public struct currentGames
        {
            public static List<accuracy> accuracy = [];
            public static List<_1v1> _1v1 = [];
            public static List<knockout> knockout = [];
        }

        public static void queue_accuracy(string userID)
        {
            foreach (var game in currentGames.accuracy)
            {
                if (game.getPlayerCount() < game.getMaxPlayers())
                {
                    game.queueUser(userID);
                    if (game.getPlayerCount() == game.getMaxPlayers())
                    {
                        game.startGame("accuracy");
                    }
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
                if (game.getPlayerCount() < game.getMaxPlayers())
                {
                    game.queueUser(userID);
                    if (game.getPlayerCount() == game.getMaxPlayers())
                    {
                        game.startGame("1v1");
                    }
                    break;
                }
            }
        }
        public static void queue_knockout(string userID)
        {
            foreach (var game in currentGames.knockout)
            {
                if (game.getPlayerCount() < game.getMaxPlayers())
                {
                    game.queueUser(userID);
                    if (game.getPlayerCount() == game.getMaxPlayers())
                    {
                        game.startGame("knockout");
                    }
                    break;
                }
            }
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
