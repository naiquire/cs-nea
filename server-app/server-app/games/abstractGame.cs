using server_app.connections;

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
        protected int maxPlayers { get; }

        protected Dictionary<string, stats> stats;
        protected static Dictionary<string, double[]> currentResponses = [];

        public abstractGame(string userID, int maxPlayers)
        {
            userIDs = [];
            this.maxPlayers = maxPlayers;
            stats = [];

            queueUser(userID);                       
        }
        public void queueUser(string userID)
        {
            userIDs.Add(userID);
        }
        public static void loadResponse(string userID, double[] input)
        {
            currentResponses.Add(userID, input);
        }
        public virtual async void startGame(string gameID)
        {
            // generic stuff for any game
            foreach (string user in userIDs)
            {
                stats.Add(user, new stats());
            }
            await new connection().sendStartRequest(gameID, userIDs);
        }
        public int getMaxPlayers() => maxPlayers;
        public int getPlayerCount() => userIDs.Count;



    }
    
}
