using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
    // handles requests related to leaderboards etc
    public partial class @connection : Hub
    {
        public userData? requestProfile(string userID)
        {
            if (database.loadUserData(userID, out userData userData))
            {
                return userData;
            }
            return null;
        }
    }
}
