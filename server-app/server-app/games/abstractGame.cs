namespace server_app.games
{
    public struct @stats
    {
        public int correct;
        public int epochs;
        public List<double> accuracy;
        public List<TimeSpan> time;
    }
    public abstract class abstractGame
    {
        protected List<string> userIDs;
        protected int maxPlayers;

        protected Dictionary<string, stats> stats;
        // some kind of structure saving the progress

        public abstractGame(string userID, int maxPlayers)
        {
            userIDs = [];
            userIDs.Add(userID);

            this.maxPlayers = maxPlayers;
            stats = [];
        }
        public void queueUser(string userID)
        {
            userIDs.Add(userID);
        }
        public virtual void startGame()
        {
            // generic stuff for any game
            foreach (string user in userIDs)
            {
                stats.Add(user, new stats());
            }
 
        }
        public int getMaxPlayers() => maxPlayers;
        public int getPlayerCount() => userIDs.Count;

    }
    
}
