using client_app.menus;
using client_app.menus.games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client_app.games
{
	public class knockout : abstractGame, IPlayable
	{
		private List<string> aliveUsers;
		public knockout(main main) : base(main, "knockout")
		{
			aliveUsers = new List<string>();
		}

		public override void queueGame()
		{
			base.queueGame();
		}

		public async override Task joinGameLobby()
		{
			await base.joinGameLobby();
		}

		public override void updateUsers(List<friendData> users)
		{
			if (started)
			{
				// left panel should only display alive users
				this.users = users;

				List<friendData> alive = new List<friendData>();
				foreach (var user in this.users)
				{
					if (aliveUsers.Contains(user.userID))
					{
						alive.Add(user);
					}
				}

				interfaces.configLeftGamePanel(main.panel_left, alive);
			}
			else
			{
				base.updateUsers(users);
			}
		}

		public override void awaitStart()
		{
			foreach (var user in users)
			{
				aliveUsers.Add(user.userID);
			}

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

		public void knockoutResults(List<string> aliveUsers)
		{
			this.aliveUsers = aliveUsers;
			updateUsers(users);

			if (aliveUsers.Contains(main.userData.userID))
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
