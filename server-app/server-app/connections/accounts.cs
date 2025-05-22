using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles login and account requests
    public partial class @connection : Hub
    {
        private static Dictionary<string, string> map = [];
        public void clientConnected(string userID)
        {
            map.TryAdd(userID, Context.ConnectionId);
        }
        public void clientDisconnected(string userID)
        {
            map.Remove(userID);
        }
        public async void loginRequest(string userID, string password)
        {
            int success = database.loginRequest(userID, password);
            await Clients.Caller.SendAsync("loginSuccess", success);
        }
        public async void accountRequest(string userID, string password)
        {
            bool success = database.accountRequest(userID, password);
            await Clients.Caller.SendAsync("accountSuccess", success ? 1 : -1);
        }
      
    }
}
