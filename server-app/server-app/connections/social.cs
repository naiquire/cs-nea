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
        public async void loadInvites(string userID)
        {
            if (database.loadInvites(userID, out List<string> invites))
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("receiveInvites", invites);
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
        }
    }
}
