using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Diagnostics.Metrics;

namespace server_app.connections
{
    public partial class @connection : Hub
    {
        public async Task sendStartRequest(string gameID, List<string> userIDs)
        {
            foreach (string userID in userIDs)
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("startGame", gameID);
                }
                else
                {
                    throw new Exception($"Client <{userID}> disconnected");
                }
            }
        }
        public async Task sendLetter(List<string> userIDs, char letter)
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
        public async Task sendResults(string userID, stats stats, bool correct)
        {
            if (map.TryGetValue(userID, out string? connectionID))
            {
                await Clients.Client(connectionID).SendAsync("receiveResults", stats, correct);
            }
            else
            {
                throw new Exception($"Client <{userID}> disconnected");
            }
        }
        public void receiveSubmission(string userID, double[] input)
        {
            abstractGame.loadResponse(userID, input);
        }
    }
}
