using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles login and account requests
    public partial class @connection : Hub
    {
        private static Dictionary<string, string> map = [];
        public userData clientConnected(string userID)
        {
            map.Add(userID, Context.ConnectionId);

            userData userData = database.loadUserData(userID) ?? throw new Exception($"userID <{userID}> does not exist");
            return userData;
        }
        public void clientDisconnected(string userID)
        {
            map.Remove(userID);
        }
    }
}
