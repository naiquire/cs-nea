using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using System.Threading.Tasks;

namespace server_app.connections
{
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
		public async Task updateFriendData(string userID, string friendID)
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
		public async void sendInvite(string userID, string senderID)
		{
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
			if (database.addFriends(user1, user2))
			{
				await updateFriendData(user1, user2);
				await updateFriendData(user2, user1);
			}
			else
			{
				database.outputException($"Failed to add <{user1}> and <{user2}> as friends");
			}
		}
	}
}
