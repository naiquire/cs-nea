using System.Collections.Generic;
using System.Linq;

namespace client_app.menus.games
{
    public class Elimination : Game, IPlayable
    {
        private bool eliminated;
        private List<string> aliveUsers;

        public Elimination(Main main) : base(main, Games.Elimination, 12)
        {
            eliminated = false;
            aliveUsers = new List<string>();
        }

        public override void UpdateUsers(List<friendData> users)
        {
            if (HasStarted())
            {
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
                base.UpdateUsers(users);
            }
        }

        public override void AwaitStart()
        {
            foreach (var user in users)
            {
                aliveUsers.Add(user.userID);
            }
            UXelements.configLeftGamePanel(this, users, new List<friendData>());
            base.AwaitStart();
        }

        public void EliminationResults(List<string> aliveUsers)
        {
            this.aliveUsers = aliveUsers;
            if (!aliveUsers.Contains(Main.userData.userID))
            {
                eliminated = true;
            }

            UpdateUsers(users);
            UXelements.configKnockoutResults(panel_results, eliminated, gameStats.correct.Last());
        }

        public override void EndGame()
        {
            base.EndGame();

            bool winner = aliveUsers.Contains(Main.userData.userID);
            UXelements.configKnockoutEndgame(main.panel_main, winner);
        }
    }
}
