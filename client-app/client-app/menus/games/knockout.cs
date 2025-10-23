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
		public knockout(main main) : base(main, "knockout", 12)
		{
			aliveUsers = new List<string>();
		}

		public override void updateUsers(List<friendData> users)
		{
			if (hasStarted())
			{
				// left panel should only display alive users
				this.users = users;

				List<friendData> alive = new List<friendData>();
				List<friendData> dead = new List<friendData>();
				foreach (var user in this.users)
				{
					if (aliveUsers.Contains(user.userID))
					{
						alive.Add(user);
					}
					else
					{
						dead.Add(user);
					}
				}

				UXelements.configLeftGamePanel(this, alive, dead);
				main.panel_left.Controls.Add(main.btn_home);
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

		public void knockoutResults(List<string> aliveUsers)
		{
			this.aliveUsers = aliveUsers;
			updateUsers(users);

			if (aliveUsers.Contains(main.userData.userID))
			{
				UXelements.configKnockoutResults(main.panel_main);
			}
			else
			{
				endGame();
			}
		}

		public override void endGame()
		{
			base.endGame();

			if (aliveUsers.Count > 1)
			{
				// did not win
			}
			else
			{
				// yay
			}
		}
	}
}
