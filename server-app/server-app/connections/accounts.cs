using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles login and account requests
    public partial class @connection : Hub
    {
        private static Dictionary<string, string> map = [];
        public userData? clientConnected(string userID)
        {
            map.Add(userID, Context.ConnectionId);

            if (database.loadUserData(userID, out userData userData))
            {
                return userData;
            }
            return null;
        }
        public void clientDisconnected(string userID)
        {
            map.Remove(userID);
        }
    }
}
