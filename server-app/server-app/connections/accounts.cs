using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
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
			map.Remove(userID);
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

			await updateOnline(userID, true);
		}
		public async void clientDisconnected(string userID)
		{
			await updateOnline(userID, false);
			map.Remove(userID);
		}

		public async Task updateOnline(string userID, bool online)
		{
			if (database.loadFriends(userID, out List<string> friends))
			{
				foreach (string friend in friends)
				{
					if (map.TryGetValue(friend, out string? connectionID))
					{
						await Clients.Client(connectionID).SendAsync("updateOnline", userID, online);
					}
				}
			}
		}
	}
}
