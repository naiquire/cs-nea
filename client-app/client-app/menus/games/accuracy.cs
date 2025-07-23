using client_app.components;
using client_app.menus;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.games
{
	// accuracy
	public partial class accuracy : abstractMenu
	{
		private static main main;

		/// <summary>
		/// Queues a user into the accuracy game type.
		/// </summary>
		/// <param name="main"></param>
		public static async void queue_accuracy(main main)
		{
			accuracy.main = main;
			return;
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
		public void join_accuracy()
		{
			// lobby
			initialiseLobby(main, game.users);
		}

		/// <summary>
		/// Runs after the server starts the game, and loads the game menu.
		/// </summary>
		public void start_accuracy()
		{
			// load game
			
			InitializeComponent(); // initialiseAccuracy
		}

		public static async void round_accuracy(char letter)
		{
			// load input class and return the array drawn
			double[] input = null;

			await main.connection.InvokeAsync("receiveSubmission", "accuracy", main.userData.userID, input); // likely incorrect
		}
	}
}
