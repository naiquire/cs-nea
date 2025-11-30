using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using System.ComponentModel;

namespace server_app.games
{
	public static class Queueing
	{
		private static readonly List<IPlayable> _currentGames = [];

		public static string QueueGame(Games gameType, string userID, IHubContext<Connection> context)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.GetGameType() != gameType) continue;
				if (game.GetPlayerCount() >= game.GetMaxPlayers() || game.HasStarted()) continue;
				if (game.QueueUser(userID))
				{
					return game.GetGameID();
				}
				return string.Empty;
			}

			IPlayable newGame = gameType switch
			{
				Games.Accuracy => new Accuracy(userID, context),
				Games.Versus => new Versus(userID, context),
				Games.Knockout => new Elimination(userID, context),

				_ => throw new InvalidEnumArgumentException()
			};

			_currentGames.Add(newGame);
			if (newGame.QueueUser(userID))
			{
				return newGame.GetGameID();
			}
			return string.Empty;
		}
		
		public static bool UserJoined(string gameID)
		{
			// client acknowledgement on joining a game
			foreach (IPlayable game in _currentGames)
			{
				if (game.GetGameID() == gameID)
				{
					game.UpdateUsers();
					if (game.GetPlayerCount() == game.GetMaxPlayers() && !game.HasStarted())
					{
						game.StartGame();
					}
					return true;
				}
			}
			return false;
		}

		public static void DequeueUser(string gameID, string userID)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.GetGameID() == gameID)
				{
					game.DequeueUser(userID);
					if (game.GetPlayerCount() <= 0)
					{
						_currentGames.Remove(game);
					}
					break;
				}
			}
		}

		public static void LoadSubmission(string gameID, string userID, byte[] input)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.GetGameID() == gameID)
				{
					game.LoadResponse(userID, input);
					return;
				}
			}
		}
		public static void RequestRound(string gameID, string userID)
		{
			foreach (IPlayable game in _currentGames)
			{
				if (game.GetGameID() == gameID)
				{
					game.ContinueRequest(userID);
				}
			}
		}
	}
}
