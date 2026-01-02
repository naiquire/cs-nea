using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
	public partial class Connection : Hub
	{
		public static readonly Dictionary<string, string> map = [];
		public async Task<bool> clientConnected(string userID)
		{
			// failsafe for server crash
			map.Remove(userID);
			map.Add(userID, Context.ConnectionId);

			if (!await LoadUserData(userID))
			{
				map.Remove(userID);
				return false;
			}

			await UpdateOnline(userID, true);
			return true;
		}
		public async void clientDisconnected(string userID)
		{
			await UpdateOnline(userID, false);
			map.Remove(userID);
			Logger.Log("LOGOUT", "lime", $"<{userID}> has disconnected");
		}
		public override Task OnDisconnectedAsync(Exception? exception)
		{
			// unexpected disconnection
			string connectionID = Context.ConnectionId;
			foreach (string user in map.Keys)
			{
				if (map[user] == connectionID)
				{
					Logger.Log("WARN", "yellow", $"<{user}> has unexpectedly disconnected");
					map.Remove(user);
					break;
				}
			}

			return base.OnDisconnectedAsync(exception);
		}

		private async Task<bool> LoadUserData(string userID)
		{
			if (!Database.LoadUserData(userID, out userData userData))
			{
                return false;
            }

            if (map.TryGetValue(userID, out string? connectionID))
            {
                await Clients.Client(connectionID).SendAsync("receiveUserData", userData);
            }

            return true;
        }

		private async Task UpdateOnline(string userID, bool online)
		{
			if (!Database.LoadFriends(userID, out List<string> friends))
			{
				return;
			}

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
