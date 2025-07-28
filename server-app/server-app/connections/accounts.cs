using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.games;
using System.Threading.Tasks;

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
        public static readonly Dictionary<string, string> map = [];
        public async Task clientConnected(string userID)
        {
            map.Add(userID, Context.ConnectionId);

            if (database.loadUserData(userID, out userData userData))
            {
                if (map.TryGetValue(userID, out string? connectionID))
                {
                    await Clients.Client(connectionID).SendAsync("receiveUserData", userData);
                }
                else
                {
                    throw new DisconnectException(userID);
                }
            }
            // else logic
        }
        public void clientDisconnected(string userID, string? gameID)
        {
            map.Remove(userID);
			
			if (gameID != null)
			{
				foreach (var game in queueing.currentGames)
				{
					if (game.getGameID() == gameID)
					{
						game.dequeueUser(userID);
					}
				}
			}
        }
    }
}
