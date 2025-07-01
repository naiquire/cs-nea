using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public struct @stats
    {
        public int correct;
        public int epochs;
        public List<double> accuracy;
        public List<TimeSpan> time;

        public void update(evaluate evaluate, int letter, TimeSpan time, bool correct)
        {
            this.accuracy.Add(evaluate.activatedValues[evaluate.layerCount - 1][letter]);
            this.epochs++;
            this.correct += correct ? 1 : 0;
            this.time.Add(time);
        }
    }
    public abstract class abstractGame
    {
        protected List<string> userIDs;
        public string gameID;
        protected int maxPlayers;
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
        protected async Task<TaskCompletionSource<bool>> awaitResponses(TaskCompletionSource<bool> receivedAll)
        {
            if (currentResponses.Count == getPlayerCount())
            {
                receivedAll.TrySetResult(true);
            }

            await receivedAll.Task;
            return receivedAll;
        }
        protected bool evaluateSubmission(ref evaluate[] evaluates, int i, List<string> userIDs, int character)
        {
            int letter = character - 65;
            evaluates[i] = new evaluate(currentResponses[userIDs[i]].submission);
            bool correct = evaluates[i].result == letter;

            if (stats.TryGetValue(userIDs[i], out stats currentStats))
            {
                DateTime endTime = currentResponses[userIDs[i]].time;
                currentStats.update(evaluates[i], letter, endTime - startTime,  correct);
            }
            stats[userIDs[i]] = currentStats;

            return correct;
        }
        protected List<char> generateLetters(int count)
        {
            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < count; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }
            return letters;
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
        public virtual async void runGame()
        {
            // base tasks for when a game is started
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
