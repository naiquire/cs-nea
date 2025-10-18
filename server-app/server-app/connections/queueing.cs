using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.games;

namespace server_app.connections
{
	public partial class @connection : Hub
	{
		public string queueGame(string gameType, string userID)
		{
			if (Program.hubContext != null)
			{
				Logger.Log("QUEUE", "blue", $"<{userID}> has queued for <{gameType}>");
				return queueing.queueGame(gameType, userID, Program.hubContext);
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
			queueing.dequeueUser(gameID, userID);
		}
		public bool userJoined(string gameID)
		{
			return queueing.userJoined(gameID);
		}
	}
}
