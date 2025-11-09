using client_app.menus;
using client_app.menus.games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client_app.games
{
	public class Elimination : Game, IPlayable
	{
		private bool _eliminated;
		private List<string> _aliveUsers;
		public Elimination(Main main) : base(main, "knockout", 12)
		{
			_aliveUsers = new List<string>();
		}

		public override void SubmissionPhase(char letter)
		{
			if (_eliminated)
			{
				EndGame();
			}
			else
			{
				base.SubmissionPhase(letter);
			}
		}

		public override void UpdateUsers(List<friendData> users)
		{
			if (hasStarted())
			{
				// left panel should only display alive users
				this.users = users;

				List<friendData> alive = new List<friendData>();
				List<friendData> dead = new List<friendData>();
				foreach (var user in this.users)
				{
					if (_aliveUsers.Contains(user.userID))
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
				base.UpdateUsers(users);
			}
		}

		public override void AwaitStart()
		{
			_eliminated = false;
			foreach (var user in users)
			{
				_aliveUsers.Add(user.userID);
			}

			base.AwaitStart();
		}

		public void KnockoutResults(List<string> aliveUsers)
		{
			this._aliveUsers = aliveUsers;
			UpdateUsers(users);

			UXelements.configKnockoutResults(panel_results, _eliminated, stats.correct.Last());
		}

		public override void EndGame()
		{
			base.EndGame();

			bool winner = !(_aliveUsers.Count > 1);
			UXelements.configKnockoutEndgame(main.panel_main, winner);
		}
	}
}
