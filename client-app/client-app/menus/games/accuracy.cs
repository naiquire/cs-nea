using client_app.components;
using client_app.menus;
using client_app.menus.games;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace client_app.games
{
	// accuracy
	public partial class accuracy : abstractGame, IPlayable
	{

		/// <summary>
		/// Queues a user into the accuracy game type.
		/// </summary>
		/// <param name="main"></param>
		public async void queueGame(main main)
		{
			base.main = main;
			if (await main.connection.InvokeAsync<bool>("queueGame", "accuracy", main.userData.userID))
			{
				// show loading or something idk
			}
			else
			{
				// queueing failed
			}
		   
		}

		/// <summary>
		/// Runs after a user has successfully queued into a game.
		/// </summary>
		public void joinGame()
		{
			// lobby
			abstractMenu.initialiseLobby(main, game.users);
		}

		public void awaitStart_accuracy()
		{

		}

		/// <summary>
		/// Runs after the server starts the game, and loads the game menu.
		/// </summary>
		public void startGame()
		{
			// load game
			InitializeComponent();
		}

		public static async void round_accuracy(char letter)
		{
			// load input class and return the array drawn
			double[] input = null;

			await main.connection.InvokeAsync("receiveSubmission", "accuracy", main.userData.userID, input); // likely incorrect
		}
	}
}
