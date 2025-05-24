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
        
      
    }
}
