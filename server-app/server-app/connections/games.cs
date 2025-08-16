using Microsoft.AspNetCore.SignalR;
using server_app.games;

namespace server_app.connections
{
	public partial class @connection : Hub
	{
		public void receiveSubmission(string gameID, string userID, double[] input)
		{
			queueing.loadSubmission(gameID, userID, input);
		}

		public void requestRound(string gameID, string userID)
		{
			queueing.requestRound(gameID, userID);
		}
	}
}
