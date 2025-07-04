using Microsoft.AspNetCore.SignalR;
using server_app.connections;
using server_app.neuralNetwork;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace server_app.games
{
    public class @accuracy(string userID) : abstractGame(userID, 1), IPlayable
    {
        public const bool online = false;
        private int count = 0;
        public override void startGame()
        {
            base.startGame();
            submissionPhase();
        }
        public async void submissionPhase()
        {
            char letter = (char)(rnd.Next(0, 26) + 65);
            letters.Add(letter);

            startTime = DateTime.UtcNow; // might be better to handle timing on users end to reduce the effect of latency but that feels vulnerable to cheats
            await new connection().sendLetter(userIDs, letters[count]);
        }
        public void loadResponse(string userID, double[] input)
        {
            currentResponses.Add(userID, (input, DateTime.UtcNow));
            if (currentResponses.Count == getPlayerCount())
            {
                evaluationPhase(letters[count]);
            }
        }
        public async void evaluationPhase(char letter)
        {
            evaluate[] evaluates = new evaluate[getPlayerCount()];
            for (int i = 0; i < userIDs.Count; i++)
            {
                evaluateSubmission(ref evaluates, i, userIDs, letter);
                await new connection().sendResult(userIDs[i], stats[userIDs[i]]);
            }

            if (count++ < 10)
            {
                submissionPhase();
            }
        }
    }
}
