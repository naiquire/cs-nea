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
        /// <summary>
        /// Attempts to queue a user into the current game, and starts the game if the lobby is full.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="userID"></param>
        /// <returns>A boolean value representing if the user was queued into the game</returns>
        private static bool tryQueueGame(abstractGame game, string userID)
        {
            if (game.getPlayerCount() < game.getMaxPlayers())
            {
                game.queueUser(userID);
                if (game.getPlayerCount() == game.getMaxPlayers())
                {
                    // async method
                    game.startGame();
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Queues a user for the Accuracy game type.
        /// </summary>
        /// <param name="userID"></param>
        public static void queue_accuracy(string userID)
        {
            foreach (var game in currentGames.accuracy)
            {
                if (tryQueueGame(game, userID))
                {
                    // user has been successfully queued into a game
                    break;
                }
            }
            // no game found
            currentGames.accuracy.Add(new accuracy(userID));
        }
        /// <summary>
        /// Queues a user for the 1v1 game type.
        /// </summary>
        /// <param name="userID"></param>
        public static void queue_1v1(string userID)
        {
            foreach (var game in currentGames._1v1)
            {
                if (tryQueueGame(game, userID))
                {
                    // user has been successfully queued into a game
                    break;
                }
            }
            // no game found
            currentGames._1v1.Add(new _1v1(userID));
        }
        /// <summary>
        /// Queues a user for the Knockout game type.
        /// </summary>
        /// <param name="userID"></param>
        public static void queue_knockout(string userID)
        {
            foreach (var game in currentGames.knockout)
            {
                if (tryQueueGame(game, userID))
                {
                    // user has been successfully queued into a game
                    break;
                }
            }
            // no game found
            currentGames.knockout.Add(new knockout(userID));
        }

        /// <summary>
        /// Sends the user's submission to the associated game class
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="input"></param>
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
