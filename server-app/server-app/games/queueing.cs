using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace server_app.games
{
    // contains all instances of running games
    public static class @queueing
    {
		public static readonly List<IPlayable> currentGames = [];

		/// <summary>
		/// Attempts to queue a user into the current game, and starts the game if the lobby is full.
		/// </summary>
		/// <param name="game"></param>
		/// <param name="userID"></param>
		/// <returns>A boolean value representing if the user was queued into the game</returns>
		private static bool tryQueueGame(IPlayable game, string userID)
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
        /// Sends the user's submission to the associated game class
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="input"></param>
        public static void loadSubmission(string gameID, string userID, double[] input)
        {
            foreach (var game in currentGames)
            {
                if (game.getGameID() == gameID)
                {
                    game.loadResponse(userID, input);
                    return;
                }
            }
        }

        /// <summary>
        /// Queues a user into a game.
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        public static void queueGame(string gameType, string userID, IHubContext<connection> context)
        {
            bool queued = false;
            foreach (IPlayable game in currentGames)
            {
                if (game.getType() == gameType)
                {
                    if (tryQueueGame(game, userID))
                    {
                        queued = true;
                        break;
                    }
                }
            }
            if (!queued)
            {
                Type? type = Type.GetType(gameType);
                if (type != null)
                {
                    ConstructorInfo[] c = type.GetConstructors();
                    c[0].Invoke([userID, context]);
                }
                else
                {
                    database.outputException($"Could not find game with type {gameType}");
                }
            }
        }
    }
}
