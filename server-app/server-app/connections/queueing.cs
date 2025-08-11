using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
	public partial class @connection : Hub
	{
		public string queueGame(string gameType, string userID)
		{
			if (Program.hubContext != null)
			{
				return queueing.queueGame(gameType, userID, Program.hubContext);
			}
			else
			{
				database.outputException("IHubContext was null when attempting to queue a user");
				throw new Exception("[FATAL] IHubContext was null when attempting to queue a user");
			}
		}
		public void dequeueGame(string gameID, string userID)
		{
			queueing.dequeueUser(gameID, userID);
		}
		public bool userJoined(string gameID)
		{
			return queueing.userJoined(gameID);
		}
	}
}
