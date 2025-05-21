using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.SignalR;
using server_app.games;

namespace server_app.connections
{
    public partial class @connection : Hub
    {
        public async Task sendStartRequest(List<string> userIDs)
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
        public async Task accuracyGame(List<string> userIDs)
        {
            // load game on client side
            await sendStartRequest(userIDs);

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
        public void receiveSubmission(string userID, double[] input)
        {
            abstractGame.loadResponse(userID, input);
        }
    }
}
