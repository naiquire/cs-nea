using System.Drawing;
using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.databases;
using server_app.neuralNetwork;

namespace server_app.games
{
    public struct gameStats
    {
        public List<bool> correct;
        public List<double> accuracy;
        public List<TimeSpan> time;

        public gameStats()
        {
            correct = [];
            accuracy = [];
            time = [];
        }

        public readonly void Update(double accuracy, TimeSpan time, bool correct)
        {
            this.accuracy.Add(accuracy);
            this.correct.Add(correct);
            this.time.Add(time);
        }
    }

    public enum Games
    {
        Accuracy,
        Versus,
        Elimination,
    }

    public interface IPlayable
    {
        bool QueueUser(string userID);
        bool DequeueUser(string userID);
        Task UpdateUsers();
        Task StartGame();
        Task SubmissionPhase();
        void LoadResponse(string userID, byte[] input);
        void EvaluationPhase(char letter);
        Task ContinueRequest(string userID);
        void EndGame();
        Games GetGameType();
        string GetGameID();
        bool HasStarted();
        int GetPlayerCount();
        int GetMaxPlayers();
    }

    public abstract class Game
    {
        protected IHubContext<Connection> hubContext;

        protected string gameID;
        protected int maxPlayers;
        protected Games type;
        private bool started;

        protected List<string> userIDs;
        protected List<friendData> userDatas;
        protected Dictionary<string, gameStats> gameStats;

        protected Random rnd;
        protected List<char> letters;
        protected int roundCount;

        protected DateTime startTime;
        protected Dictionary<string, (double[] submission, DateTime time)> currentResponses;
        protected List<string> continueRequests;

        public Games GetGameType() => type;
        public string GetGameID() => gameID;
        public bool HasStarted() => started;
        public int GetMaxPlayers() => maxPlayers;
        public int GetPlayerCount() => userIDs.Count;

        public Game(IHubContext<Connection> context, Games type, string userID, int maxPlayers)
        {
            hubContext = context;

            gameID = userID + DateTime.UtcNow.ToString();
            this.maxPlayers = maxPlayers;
            this.type = type;
            started = false;

            userIDs = [];
            userDatas = [];
            gameStats = [];

            rnd = new();
            letters = [];
            roundCount = 0;

            currentResponses = [];
            continueRequests = [];
        }

        public bool QueueUser(string userID)
        {
            if (!Database.LoadFriendData(userID, out friendData data))
            {
                return false;
            }

            userIDs.Add(userID);
            userDatas.Add(data);
            return true;
        }

        public virtual bool DequeueUser(string userID)
        {
            int index = -1;
            for (int i = 0; i < GetPlayerCount(); i++)
            {
                if (userDatas[i].userID == userID)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                return false;

            userIDs.Remove(userID);
            userDatas.RemoveAt(index);

            return true;
        }

        public async Task UpdateUsers()
        {
            foreach (var user in userIDs)
            {
                if (Connection.map.TryGetValue(user, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("updateUsers", userDatas);
                }
            }
        }

        public virtual async Task StartGame()
        {
            started = true;

            foreach (string user in userIDs)
            {
                gameStats.Add(user, new gameStats());
            }

            foreach (string userID in userIDs)
            {
                if (Connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("awaitStart");
                }
            }

            await Task.Delay(5000);

            foreach (string userID in userIDs)
            {
                if (Connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("startGame");
                }
            }
        }

        protected List<char> GenerateLetters(int count)
        {
            List<char> letters = [];

            Random rnd = new();
            for (int i = 0; i < count; i++)
            {
                letters.Add((char)(rnd.Next(0, 26) + 65));
            }
            return letters;
        }

        protected virtual async Task AwaitRound()
        {
            continueRequests.Clear();
            foreach (string userID in userIDs)
            {
                if (Connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("awaitRound");
                }
            }
        }

        protected async Task SendLetter(List<string> userIDs, char letter)
        {
            foreach (string userID in userIDs)
            {
                if (Connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("receiveLetter", letter);
                }
            }
        }

        public virtual void LoadResponse(string userID, byte[] input)
        {
            DateTime endTime = DateTime.UtcNow;
            double[] array;

            using (var ms = new MemoryStream(input))
            {
                var bmp = new Bitmap(ms);
                array = Data.PreprocessImage(bmp);
            }

            currentResponses.Add(userID, (array, endTime));
        }

        protected bool EvaluateSubmission(string userID, char character)
        {
            int letter = character - 65;

            Network network = new(currentResponses[userID].submission);
            bool correct = network.GetResult() == letter;

            if (gameStats.TryGetValue(userID, out gameStats currentStats))
            {
                DateTime endTime = currentResponses[userID].time;
                double accuracy = network.GetAccuracy(letter);

                currentStats.Update(accuracy, endTime - startTime, correct);
            }
            gameStats[userID] = currentStats;
            return correct;
        }

        protected async Task SendResult(string userID, gameStats stats)
        {
            bool correct = stats.correct[roundCount];
            double accuracy = stats.accuracy[roundCount];
            TimeSpan time = stats.time[roundCount];

            if (Connection.map.TryGetValue(userID, out string? connectionID))
            {
                await hubContext.Clients.Client(connectionID).SendAsync("receiveResults", correct, accuracy, time);
            }
        }

        public virtual async void EndGame()
        {
            foreach (string userID in userIDs)
            {
                if (Connection.map.TryGetValue(userID, out string? connectionID))
                {
                    await hubContext.Clients.Client(connectionID).SendAsync("endGame");
                }
            }

            foreach (string userID in userIDs)
            {
                // iterate through each round the current user completed
                for (int i = 0; i < gameStats[userID].accuracy.Count; i++)
                {
                    // reload userData after each update for duplicate letters
                    if (Database.LoadUserData(userID, out userData userData))
                    {
                        await UpdateStatistics(userData, letters[i], i);
                    }
                    else
                    {
                        Database.outputException("Failed to retrieve statistics");
                    }
                }
            }
        }

        private async Task UpdateStatistics(userData userData, char letter, int round)
        {
            double accuracy = userData.statistics[letter].accuracy;
            TimeSpan time = userData.statistics[letter].time;
            int total = userData.statistics[letter].total;

            double updatedAccuracy =
                (accuracy * total + gameStats[userData.userID].accuracy[round]) / (total + 1);
            TimeSpan updatedTime =
                (time * total + gameStats[userData.userID].time[round]) / (total + 1);

            if (!Database.UpdateStatistics(userData.userID, letter, updatedAccuracy, updatedTime, total + 1))
            {
                Database.outputException("Failed to update statistics");
                return;
            }

            if (Connection.map.TryGetValue(userData.userID, out string? connectionID))
            {
                // update statistics client-side for the current round
                statistics updated = new(updatedAccuracy, updatedTime, total + 1);
                await hubContext.Clients.Client(connectionID).SendAsync("updateStatistics", letter, updated);
            }
        }
    }
}
