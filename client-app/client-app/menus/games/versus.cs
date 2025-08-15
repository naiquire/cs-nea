using client_app.menus.games;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace client_app.games
{
	public class versus : abstractGame, IPlayable
	{
		public versus(main main) : base(main, "versus")
		{

		}

		public override void queueGame()
		{
			base.queueGame();
		}

		public async override Task joinGameLobby()
		{
			await base.joinGameLobby();
		}

		public override void awaitStart()
		{
			base.awaitStart();
		}

		public override void startGame()
		{
			base.startGame();
		}

		public override void submissionPhase(char letter)
		{
			base.submissionPhase(letter);
		}

		public override void evaluationPhase(bool correct, double accuracy, TimeSpan time)
		{
			base.evaluationPhase(correct, accuracy, time);
		}

		public void versusResults(string winner)
		{
			TextBox txt_winner = new TextBox()
			{
				Location = new Point(100, 100),
				Size = new Size(300, 50),
				Text = winner,
			};

			main.panel_main.Controls.Add(txt_winner);
		}

		public override void endGame()
		{
			base.endGame();
		}

		public void updateRank(int currentRank)
		{
			int previousRank = main.userData.rank;
			int change = currentRank - previousRank;

			main.userData.rank = currentRank;

			// display graphic showing change on endscreen
		}
	}
}
