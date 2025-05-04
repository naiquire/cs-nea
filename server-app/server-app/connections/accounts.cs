using Microsoft.AspNetCore.SignalR;

namespace server_app.connections
{
    // handles login and account requests
    public partial class @connection : Hub
    {
        private Dictionary<string, string> map = [];
        public async void loginRequest(string userID, string password)
        {
            var database = new databases.database();
            int success = database.loginRequest(userID, password);

            if (map.TryGetValue(userID, out string? connectionID))
            {
                await Clients.Client(connectionID).SendAsync("loginSuccess", success);
            }
        }
        public void clientConnected(string userID)
        {
            map.TryAdd(userID, Context.ConnectionId);
        }
        public void clientDisconnected(string userID)
        {
            map.Remove(userID);
        }
    }
}
