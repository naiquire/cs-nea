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
		public accuracy(main main) : base(main, "accuracy")
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

		public async override Task evaluationPhase()
		{
			await base.evaluationPhase();
		}
	}
}
