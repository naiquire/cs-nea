using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
	public partial class @connection : Hub
	{
		public static readonly Dictionary<string, string> map = [];
		public async Task<bool> clientConnected(string userID)
		{
			map.Remove(userID); // failsafe for logouts on server crash
			map.Add(userID, Context.ConnectionId);

			if (!await loadUserData(userID))
			{
				map.Remove(userID);
				return false;
			}
			await updateOnline(userID, true);
			return true;
		}
		public async void clientDisconnected(string userID)
		{
			await updateOnline(userID, false);
			map.Remove(userID);
			Logger.Log("LOGOUT", "lime", $"<{userID}> has disconnected");
		}

		public async Task<bool> loadUserData(string userID)
		{
			if (!database.loadUserData(userID, out userData userData))
			{
                return false;
            }

            if (map.TryGetValue(userID, out string? connectionID))
            {
                await Clients.Client(connectionID).SendAsync("receiveUserData", userData);
            }
            return true;
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
