using client_app.menus.games;
using System;
using System.Threading.Tasks;
namespace client_app.menus.games
{
	public class Versus : Game, IPlayable
	{
		private int rankDelta = 0;
		public Versus(Main main) : base(main, Games.Versus, 2)
		{

		}

		public void VersusResults(string winner)
		{
			UXelements.configVersusResults(panel_results, winner);
		}

		public override void EndGame()
		{
			base.EndGame();
			UXelements.configVersusEndgame(main.panel_main, rankDelta);
		}

		public void UpdateRank(int currentRank)
		{
			int previousRank = Main.userData.rank;
			rankDelta = currentRank - previousRank;

			Main.userData.rank = currentRank;
		}
	}
}
