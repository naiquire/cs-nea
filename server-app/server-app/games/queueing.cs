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
	    private static bool tryQueueGame(IPlayable game, string userID)
        {
            if (game.getPlayerCount() < game.getMaxPlayers() && !game.hasStarted())
            {
                game.queueUser(userID);
				return true;
            }
            return false;
        }

        public static bool userJoined(string gameID)
        {
			foreach (IPlayable game in currentGames)
			{
				if (game.getGameID() == gameID)
				{
                    game.updateUsers();
					if (game.getPlayerCount() == game.getMaxPlayers())
					{
						game.startGame();
                        return true;
					}
				}
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
        /// Queues a user into a game of the specified type.
        /// </summary>
        /// <param name="gameType"></param>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        /// <returns><see langword="true"/> if the user was successfully queued; otherwise <see langword="false"/></returns>
        public static string queueGame(string gameType, string userID, IHubContext<connection> context)
        {
            foreach (IPlayable game in currentGames)
            {
                if (game.getType() == gameType)
                {
                    if (tryQueueGame(game, userID))
                    {
                        return game.getGameID();
                    }
                }
            }


			IPlayable newGame;
			switch (gameType)
            {
				case "accuracy":
					newGame = new accuracy(userID, context);
                    currentGames.Add(newGame);
                    tryQueueGame(newGame, userID);
                    return newGame.getGameID();
		        case "versus":
					newGame = new versus(userID, context);
                    currentGames.Add(newGame);
					tryQueueGame(newGame, userID);
					return newGame.getGameID();
				case "knockout":
					newGame = new knockout(userID, context);
                    currentGames.Add(newGame);
                    tryQueueGame(newGame, userID);
                    return newGame.getGameID();
                default:
					database.outputException($"Could not find game with type {gameType}");
					return "";
			}
		}

        /// <summary>
        /// Dequeues a user from the specified game.
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        public static void dequeueUser(string gameID, string userID)
        {
			foreach (IPlayable game in currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.dequeueUser(userID);
				}
                if (game.getPlayerCount() <= 0)
                {
                    currentGames.Remove(game);
				}
			}
		}

        public static void requestRound(string gameID, string userID)
        {
            foreach (IPlayable game in currentGames)
            {
                if (game.getGameID() == gameID)
                {
                    game.continueRequest(userID);
                }
            }
        }
    }
}
