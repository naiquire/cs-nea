using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.games;

namespace server_app.connections
{
	public partial class Connection : Hub
	{
		public string queueGame(Games gameType, string userID)
		{
			if (Program.hubContext != null)
			{
				Logger.Log("QUEUE", "blue", $"<{userID}> has queued for <{gameType}>");
				return Queueing.QueueGame(gameType, userID, Program.hubContext);
			}
			else
			{
				database.outputException("IHubContext was null when attempting to queue a user");
				return string.Empty;
			}
		}
		public void dequeueGame(string gameID, string userID)
		{
			Logger.Log("QUEUE", "blue", $"<{userID}> has dequeued");
			Queueing.DequeueUser(gameID, userID);
		}
		public bool userJoined(string gameID)
		{
			return Queueing.UserJoined(gameID);
		}
	}
}
