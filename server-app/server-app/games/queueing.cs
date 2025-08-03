using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using System.Reflection;

namespace server_app.games
{
    public static class @queueing
    {
	    public static readonly List<IPlayable> currentGames = [];

	    /// <summary>
	/// Attempts to queue a user into the current game, and starts the game if the lobby is full.
	/// </summary>
	/// <param name="game"></param>
	/// <param name="userID"></param>
	/// <returns>A boolean value representing if the user was queued into the game</returns>
	    private static async Task<bool> tryQueueGame(IPlayable game, string userID)
        {
            if (game.getPlayerCount() < game.getMaxPlayers() && !game.hasStarted())
            {
                await game.queueUser(userID);
				return true;
            }
            return false;
        }

        public static void checkGameStart(string gameID)
        {
			foreach (IPlayable game in queueing.currentGames)
			{
				if (game.getGameID() == gameID)
				{
					if (game.getPlayerCount() == game.getMaxPlayers())
					{
						game.startGame();
					}
				}
			}
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
        /// Queues a user into a game of the specified type.
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        /// <returns><see langword="true"/> if the user was successfully queued; otherwise <see langword="false"/></returns>
        public static async Task<string> queueGame(string gameType, string userID, IHubContext<connection> context)
        {
            foreach (IPlayable game in currentGames)
            {
                if (game.getType() == gameType)
                {
                    if (await tryQueueGame(game, userID))
                    {
                        return game.getGameID();
                    }
                }
            }

            IPlayable g;

			switch (gameType)
            {
                case "accuracy":
                    g = new accuracy(userID, context);
                    currentGames.Add(g);
                    await tryQueueGame(g, userID);
                    return g.getGameID();
		        case "versus":
                    g = new versus(userID, context);
                    currentGames.Add(g);
					await tryQueueGame(g, userID);
					return g.getGameID();
				case "knockout":
                    g = new knockout(userID, context);
                    currentGames.Add(g);
                    await tryQueueGame(g, userID);
                    return g.getGameID();
                default:
					database.outputException($"Could not find game with type {gameType}");
					return "";                
			}
		}
    }
}
