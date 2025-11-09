using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;

namespace server_app.games
{
	public static class Queueing
	{
		private static readonly List<IPlayable> _currentGames = [];

		public static string queueGame(string gameType, string userID, IHubContext<Connection> context)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.getType() != gameType) continue;
				if (game.getPlayerCount() >= game.getMaxPlayers() || game.hasStarted()) continue;
				if (game.queueUser(userID))
				{
					return game.getGameID();
				}
				return string.Empty;
			}

			IPlayable newGame;
			switch (gameType)
			{
				case "accuracy":
					newGame = new Accuracy(userID, context);
					break;
				case "versus":
					newGame = new Versus(userID, context);
					break;
				case "knockout":
					newGame = new Elimination(userID, context);
					break;
				default:
					database.outputException($"Could not find game with type {gameType}");
					return string.Empty;
			}

			_currentGames.Add(newGame);
			if (newGame.queueUser(userID))
			{
				return newGame.getGameID();
			}
			return string.Empty;
		}
		
		public static bool userJoined(string gameID)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.updateUsers();
					if (game.getPlayerCount() == game.getMaxPlayers() && !game.hasStarted())
					{
						game.StartGame();
					}
					return true;
				}
			}
			return false;
		}

		public static void dequeueUser(string gameID, string userID)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.DequeueUser(userID);
					if (game.getPlayerCount() <= 0)
					{
						_currentGames.Remove(game);
					}
					break;
				}
			}
		}

		public static void loadSubmission(string gameID, string userID, byte[] input)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.LoadResponse(userID, input);
					return;
				}
			}
		}
		public static void requestRound(string gameID, string userID)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.getGameID() == gameID)
				{
					game.ContinueRequest(userID);
				}
			}
		}
	}
}
