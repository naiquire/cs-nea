using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles login and account requests
    public class DisconnectException : Exception
    {
        public DisconnectException(string userID)
        {
            database.outputException($"Client has disconnected : <{userID}>");
        }
    }
    public partial class @connection : Hub
    {
        public static Dictionary<string, string> map = [];
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
