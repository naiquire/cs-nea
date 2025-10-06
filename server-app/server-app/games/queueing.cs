using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;

namespace server_app.games
{
	public static class @queueing
	{
		private static readonly List<IPlayable> currentGames = [];

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
					break;
				case "versus":
					newGame = new versus(userID, context);
					break;
				case "knockout":
					newGame = new knockout(userID, context);
					break;
				default:
					database.outputException($"Could not find game with type {gameType}");
					return string.Empty;
			}

			currentGames.Add(newGame);
			tryQueueGame(newGame, userID);

			static bool tryQueueGame(IPlayable game, string userID)
			{
				if (game.getPlayerCount() < game.getMaxPlayers() && !game.hasStarted())
				{
					return game.queueUser(userID);
				}
				return false;
			}

			return newGame.getGameID();
		}
		
		public static bool userJoined(string gameID)
		{
			foreach (IPlayable game in currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.updateUsers();
					if (game.getPlayerCount() == game.getMaxPlayers() && !game.hasStarted())
					{
						game.startGame();
					}
					return true;
				}
			}
			return false;
		}

		public static void dequeueUser(string gameID, string userID)
		{
			foreach (IPlayable game in currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.dequeueUser(userID);
					if (game.getPlayerCount() <= 0)
					{
						currentGames.Remove(game);
					}
					break;
				}
			}
		}

		public static void loadSubmission(string gameID, string userID, byte[] input)
		{
			foreach (IPlayable game in currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.loadResponse(userID, input);
					return;
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
