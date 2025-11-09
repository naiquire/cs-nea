using client_app.menus.games;
using System;
using System.Threading.Tasks;
using client_app.menus;

namespace client_app.games
{
	public class Versus : Game, IPlayable
	{
		private int _rankDelta = 0;
		public Versus(Main main) : base(main, "versus", 2)
		{

		}

		public void VersusResults(string winner)
		{
			UXelements.configVersusResults(panel_results, winner);
		}

		public override void EndGame()
		{
			base.EndGame();
			UXelements.configVersusEndgame(main.panel_main, _rankDelta);
		}

		public void UpdateRank(int currentRank)
		{
			int previousRank = Main.userData.rank;
			_rankDelta = currentRank - previousRank;

			Main.userData.rank = currentRank;
		}
	}
}
