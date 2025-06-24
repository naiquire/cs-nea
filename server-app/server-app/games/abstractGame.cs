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
        public string gameID;
        protected int maxPlayers { get; }
        protected DateTime startTime;

        protected Dictionary<string, stats> stats;
        protected Dictionary<string, (double[] submission, DateTime time)> currentResponses = [];

        public abstractGame(string userID, int maxPlayers)
        {
            userIDs = [];
            this.maxPlayers = maxPlayers;
            stats = [];

            gameID = userID + DateTime.UtcNow.ToString();
            queueUser(userID);                       
        }
        public async Task<TaskCompletionSource<bool>> awaitResponses(TaskCompletionSource<bool> receivedAll)
        {
            if (currentResponses.Count == getPlayerCount())
            {
                receivedAll.TrySetResult(true);
            }

            await receivedAll.Task;
            return receivedAll;
        }
        public async void queueUser(string userID)
        {
            userIDs.Add(userID);
            await new connection().sendJoinConfirm(userID, gameID);
        }
        public void loadResponse(string userID, double[] input)
        {
            currentResponses.Add(userID, (input, DateTime.UtcNow));
        }
        public virtual async void startGame()
        {
            // generic stuff for any game
            foreach (string user in userIDs)
            {
                stats.Add(user, new stats());
            }
            await new connection().sendStartRequest(userIDs);
        }
        public int getMaxPlayers() => maxPlayers;
        public int getPlayerCount() => userIDs.Count;



    }
    
}
