using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;

namespace server_app.games
{
    public class @knockout(string userID) : abstractGame(userID, 12), IPlayable
    {
        public const bool online = true;

        private List<string> aliveUsers = [];
        public override void startGame()
        {
            aliveUsers = [.. userIDs];

            base.startGame();
            submissionPhase();          
        }

        public async void submissionPhase()
        {
            char letter = (char)(rnd.Next(0, 26) + 65);
            letters.Add(letter);

            startTime = DateTime.UtcNow;
            await new connection().sendLetter(aliveUsers, letter);
        }
        public void loadResponse(string userID, double[] input)
        {
            currentResponses.Add(userID, (input, DateTime.UtcNow));
            if (currentResponses.Count == aliveUsers.Count)
            {
                evaluationPhase(letters[^1]);
            }
        }
        public void evaluationPhase(char letter)
        {
            evaluate[] evaluates = new evaluate[getPlayerCount()];
            for (int i = 0; i < aliveUsers.Count; i++)
            {
                evaluateSubmission(ref evaluates, i, userIDs, letter);
            }

            if (aliveUsers.Count > 1)
            {
                submissionPhase();
            }
        }
    }
}
