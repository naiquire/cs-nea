using client_app.menus.games;
using System;
using System.Threading.Tasks;

namespace client_app.games
{
	public class accuracy : abstractGame, IPlayable
	{
		public accuracy(main main) : base(main, "accuracy")
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
	}
}
