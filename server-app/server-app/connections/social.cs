using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles requests related to leaderboards etc
    public partial class @connection : Hub
    {
        public userData requestProfile(string userID)
        {
            userData userData = database.loadUserData(userID) ?? throw new Exception($"userID <{userID}> does not exist");
            return userData;
        }
    }
}
