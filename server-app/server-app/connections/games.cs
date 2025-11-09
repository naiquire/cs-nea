using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Drawing;

namespace server_app.connections
{
	public partial class Connection : Hub
	{
		public void receiveSubmission(string gameID, string userID, byte[] input)
		{
			Queueing.loadSubmission(gameID, userID, input);
		}

		public void requestRound(string gameID, string userID)
		{
			Queueing.requestRound(gameID, userID);
		}
	}
}
