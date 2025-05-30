using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles login and account requests
    public partial class @connection : Hub
    {
        private static Dictionary<string, string> map = [];
        public async void clientConnected(string userID)
        {
            map.TryAdd(userID, Context.ConnectionId);

            if (map.TryGetValue(userID, out string? connectionID))
            {
                userData userData = database.loadUserData(userID) ?? throw new Exception($"userID <{userID}> does not exist");
                await Clients.Client(connectionID).SendAsync("receiveUserData", userData);
            }
            else
            {
                throw new Exception($"Client <{userID}> disconnected");
            }
        }
        public void clientDisconnected(string userID)
        {
            map.Remove(userID);
        }
    }
}
