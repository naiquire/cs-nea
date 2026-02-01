namespace client_app.menus.games
{
	public class Versus : Game, IPlayable
	{
		private int rankDelta;
		public Versus(Main main) : base(main, Games.Versus, 2)
		{
			rankDelta = 0;
		}

		public void VersusResults(string winner)
		{
			UXelements.configVersusResults(panel_results, winner);
		}

		public void UpdateRank(int currentRank)
		{
			int previousRank = Main.userData.rank;
			rankDelta = currentRank - previousRank;

			Main.userData.rank = currentRank;
			UXelements.configVersusEndgame(main.panel_main, rankDelta);
		}
	}
}
