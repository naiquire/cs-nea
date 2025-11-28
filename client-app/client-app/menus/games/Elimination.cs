using System.Collections.Generic;
using System.Linq;

namespace client_app.menus.games
{
	public class Elimination : Game, IPlayable
	{
		private bool _eliminated;
		private List<string> _aliveUsers;
		public Elimination(Main main) : base(main, Games.Knockout, 12)
		{
			_eliminated = false;
			_aliveUsers = new List<string>();
		}

		public override void UpdateUsers(List<friendData> users)
		{
			if (HasStarted())
			{
				// left panel should only display alive users
				this._users = users;

				List<friendData> alive = new List<friendData>();
				List<friendData> dead = new List<friendData>();
				foreach (var user in this._users)
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
			foreach (var user in _users)
			{
				_aliveUsers.Add(user.userID);
			}

			base.AwaitStart();
		}

		public void KnockoutResults(List<string> aliveUsers)
		{
			this._aliveUsers = aliveUsers;
			if (!aliveUsers.Contains(Main.userData.userID))
			{
				_eliminated = true;
			}

			UpdateUsers(_users);
			UXelements.configKnockoutResults(panel_results, _eliminated, _stats.correct.Last());
		}

		public override void EndGame()
		{
			base.EndGame();

			bool winner = _aliveUsers.Contains(Main.userData.userID);
			UXelements.configKnockoutEndgame(main.panel_main, winner);
		}
	}
}
