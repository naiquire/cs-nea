using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app.connections
{
	public partial class @connection : Hub
	{
		public userData requestProfile(string userID)
		{
			if (database.loadUserData(userID, out userData userData))
			{
				return userData;
			}
			return new userData();
		}

		public async Task updateUserData(string userID, string aboutMe, string localisation)
		{
			if (database.updateUserData(userID, aboutMe, localisation))
			{
				Logger.Log("ACCOUNT", ConsoleColor.Magenta, $"<{userID}> has updated their profile");
				if (map.TryGetValue(userID, out string? connectionID))
				{
					await Clients.Client(connectionID).SendAsync("updateUserData", aboutMe, localisation);
				}
			}
			else
			{
				database.outputException($"Failed to update userData for <{userID}>");
			}
		}
		public async Task updateFriendData(string userID, string friendID, bool delete)
		{
			if (delete)
			{
				if (map.TryGetValue(userID, out string? connectionID))
				{
					await Clients.Client(connectionID).SendAsync("removeFriend", friendID);
				}
			}
			else
			{
				if (database.loadFriendData(friendID, out friendData friendData))
				{
					if (map.TryGetValue(userID, out string? connectionID))
					{
						await Clients.Client(connectionID).SendAsync("updateFriendData", friendData);
					}
				}
				else
				{
					database.outputException($"Failed to retrieve userData for <{friendID}>");
				}
			}
		}

		public async void sendInvite(string userID, string senderID)
		{
			Logger.Log("SOCIAL", ConsoleColor.Cyan, $"<{senderID}> has sent a friend invite to <{userID}>");
			if (map.TryGetValue(userID, out string? connectionID))
			{
				await Clients.Client(connectionID).SendAsync("receiveInvites", new List<string>() { senderID });
			}
			else
			{
				if (!database.saveInvite(userID, senderID))
				{
					database.outputException($"Failed to save invite for <{userID}> from <{senderID}>");
				}
			}
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
		public async void addFriends(string user1, string user2)
		{
			Logger.Log("SOCIAL", ConsoleColor.Cyan, $"<{user1}> has accepted a friend invite from <{user2}>");
			if (database.addFriends(user1, user2))
			{
				await updateFriendData(user1, user2, false);
				await updateFriendData(user2, user1, false);
			}
			else
			{
				database.outputException($"Failed to add <{user1}> and <{user2}> as friends");
			}
		}
		public async void removeFriends(string user1, string user2)
		{
			Logger.Log("SOCIAL", ConsoleColor.Cyan, $"<{user1}> has removed <{user2}> from their friends list");
			if (database.removeFriends(user1, user2))
			{
				await updateFriendData(user1, user2, true);
				await updateFriendData(user2, user1, true);
			}
			else
			{
				database.outputException($"Failed to remove <{user1}> and <{user2}> as friends");
			}
		}
	}
}
