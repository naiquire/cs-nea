using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    // handles requests for queueing games
    public partial class @connection : Hub
    {
        public void queueGame(string gameID, string userID)
        {
            // convert gameID to method name
            MethodInfo? methodInfo = typeof(queueing).GetMethod($"queue_{gameID}") ?? throw new Exception($"GameID <{gameID}> could not be found");
            methodInfo.Invoke(methodInfo, [userID]);
            
        }
        public async void sendStartRequest(List<string> userIDs)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("startGame");
                }
                else
                {
                    throw new Exception($"Client <{userID}> disconnected");
                }
            }
        }
        public async void sendLetter(List<string> userIDs, char letter)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("receiveLetter", letter);
                }
                else
                {
                    throw new Exception($"Client <{userID}> disconnected");
                }
            }
        }
        public void receiveSubmission(double[] input, char letter)
        {
            neuralNetwork.evaluate network = new neuralNetwork.evaluate(input);
            if (network.result == letter - 65)
            {

            }
        }
    }
}
