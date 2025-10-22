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

		public async Task<bool> updateUserData(string userID, string aboutMe, string localisation)
		{
			if (!database.updateUserData(userID, aboutMe, localisation))
			{
				database.outputException($"Failed to update userData for <{userID}>");
				return false;
			}

			Logger.Log("ACCOUNT", "fuchsia", $"<{userID}> has updated their profile");
			if (map.TryGetValue(userID, out string? connectionID))
			{
				await Clients.Client(connectionID).SendAsync("updateUserData", userID, aboutMe, localisation);
			}

			if (!database.loadFriends(userID, out var friends))
			{
				database.outputException($"Failed to load friends for <{userID}>");
				return false;
			}

			foreach (var friend in friends)
			{
				if (map.TryGetValue(friend, out connectionID))
				{
					await Clients.Client(connectionID).SendAsync("updateUserData", userID, aboutMe, localisation);
				}
			}

			return true;
		}
		public async Task<bool> updateFriendData(string userID, string friendID, bool delete)
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
				if (!database.loadFriendData(friendID, out friendData friendData))
				{
					database.outputException($"Failed to retrieve userData for <{friendID}>");
					return false;
				}

				if (map.TryGetValue(userID, out string? connectionID))
				{
					await Clients.Client(connectionID).SendAsync("updateFriendData", friendData);
				}
			}
			return true;
		}

		public async Task<bool> sendInvite(string userID, string senderID)
		{
			Logger.Log("SOCIAL", "cyan", $"<{senderID}> has sent a friend invite to <{userID}>");
			if (map.TryGetValue(userID, out string? connectionID))
			{
				await Clients.Client(connectionID).SendAsync("receiveInvites", new List<string>() { senderID });
			}
			else
			{
				if (!database.saveInvite(userID, senderID))
				{
					database.outputException($"Failed to save invite for <{userID}> from <{senderID}>");
					return false;
				}
			}
			return true;
		}
		public async void loadInvites(string userID)
		{
			if (!database.loadInvites(userID, out List<string> invites))
			{
				database.outputException($"Failed to load invites for user <{userID}>");
				return;
			}

			if (map.TryGetValue(userID, out string? connectionID))
			{
				await Clients.Client(connectionID).SendAsync("receiveInvites", invites);
			}
		}
		public async Task<bool> addFriends(string user1, string user2)
		{
			Logger.Log("SOCIAL", "cyan", $"<{user1}> has accepted a friend invite from <{user2}>");
			if (database.addFriends(user1, user2))
			{
				database.outputException($"Failed to add <{user1}> and <{user2}> as friends");
				return false;
			}

			bool isRemoved = false;
			if (!await updateFriendData(user1, user2, isRemoved))
			{
				if (map.TryGetValue(user1, out string? connectionID))
				{
					await Clients.Client(connectionID).SendAsync("alert", $"Failed to retrieve data for friend <{user2}>");
				}
			}
			if (!await updateFriendData(user2, user1, isRemoved))
			{
				return false;
			}

			return true;
		}
		public async Task<bool> removeFriends(string user1, string user2)
		{
			Logger.Log("SOCIAL", "cyan", $"<{user1}> has removed <{user2}> from their friends list");
			if (database.removeFriends(user1, user2))
			{
				bool isRemoved = true;
				await updateFriendData(user1, user2, isRemoved);
				await updateFriendData(user2, user1, isRemoved);
				return true;
			}
			else
			{
				database.outputException($"Failed to remove <{user1}> and <{user2}> as friends");
				return false;
			}
		}
	}
}
