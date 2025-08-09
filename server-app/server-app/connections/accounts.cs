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
			// add user to connectionID map
			if (map.ContainsKey(userID))
			{
				map.Remove(userID); // temp fix
			}
			map.Add(userID, Context.ConnectionId);
			
			// send userData to client
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

			// update online status for friends
			foreach (friendData friend in userData.friends)
			{
				if (map.TryGetValue(friend.userID, out string? connectionID))
				{
					await Clients.Client(connectionID).SendAsync("updateOnline", userID, true);
				}
			}
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
