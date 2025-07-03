using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public struct @stats
    {
        public List<bool> correct;
        public List<double> accuracy;
        public List<TimeSpan> time;
        /// <summary>
        /// Updates the current statistics for the user.
        /// </summary>
        /// <param name="evaluate"></param>
        /// <param name="letter"></param>
        /// <param name="time"></param>
        /// <param name="correct"></param>
        public void update(evaluate evaluate, int letter, TimeSpan time, bool correct)
        {
            this.accuracy.Add(evaluate.activatedValues[evaluate.layerCount - 1][letter]);
            this.correct.Add(correct);
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

        /// <summary>
        /// Base initialisation for the game classes. Automatically queues the user into the respective game.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="maxPlayers"></param>
        public abstractGame(string userID, int maxPlayers)
        {
            userIDs = [];
            this.maxPlayers = maxPlayers;
            stats = [];

            gameID = userID + DateTime.UtcNow.ToString();
            queueUser(userID);                       
        }
        

        /// <summary>
        /// Evaluates a user's submission and updates their current statistics.
        /// </summary>
        /// <param name="evaluates"></param>
        /// <param name="i"></param>
        /// <param name="userIDs"></param>
        /// <param name="character"></param>
        /// <returns>A boolean value representing if the submission is correct.</returns>
        protected bool evaluateSubmission(ref evaluate[] evaluates, int i, List<string> userIDs, int character)
        {
            // evaluate the submission
            int letter = character - 65;
            evaluates[i] = new evaluate(currentResponses[userIDs[i]].submission);
            bool correct = evaluates[i].result == letter;

            // update the statistics for the current game
            if (stats.TryGetValue(userIDs[i], out stats currentStats))
            {
                DateTime endTime = currentResponses[userIDs[i]].time;
                currentStats.update(evaluates[i], letter, endTime - startTime,  correct);
            }
            stats[userIDs[i]] = currentStats;

            return correct;
        }
        /// <summary>
        /// Generates a fixed number of random characters from A-Z.
        /// </summary>
        /// <param name="count"></param>
        /// <returns>A list of random characters.</returns>
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
        /// <summary>
        /// Queues a user into the current game and sends a confirmation to the user.
        /// </summary>
        /// <param name="userID"></param>
        public async void queueUser(string userID)
        {
            userIDs.Add(userID);
            await new connection().sendJoinConfirm(userID, gameID);
        }
        /// <summary>
        /// Loads a submission into the game class.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="input"></param>
        public void loadResponse(string userID, double[] input)
        {
            currentResponses.Add(userID, (input, DateTime.UtcNow));
        }
        /// <summary>
        /// Base function for starting a game. Initialises values for statistics for each user.
        /// </summary>
        public virtual async void runGame()
        {
            foreach (string user in userIDs)
            {
                stats.Add(user, new stats());
            }
            await new connection().sendStartRequest(userIDs);
        }
        /// <summary>
        /// Gets the maximum number of players that can join the game.
        /// </summary>
        public int getMaxPlayers() => maxPlayers;
        /// <summary>
        /// Gets the number of players currently in the game
        /// </summary>
        public int getPlayerCount() => userIDs.Count;



    }
    
}
