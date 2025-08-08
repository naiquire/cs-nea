using client_app.menus.games;
using System;
using System.Threading.Tasks;

namespace client_app.games
{
	public class knockout : abstractGame, IPlayable
	{
		public knockout(main main) : base(main, "knockout")
		{

		}

		public override void queueGame()
		{
			base.queueGame();
		}

		public async override Task joinGame()
		{
			await base.joinGame();
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

		public void knockoutResults(bool passed)
		{
			if (passed)
			{
				// add to results screen
			}
			else
			{
				// show exit screen
			}
		}
	}
}
