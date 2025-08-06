using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    public partial class @connection : Hub
    {
        /// <summary>
        /// Receives a submission from a user.
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        /// <param name="input"></param>
        public void receiveSubmission(string gameID, string userID, double[] input)
        {
            queueing.loadSubmission(gameID, userID, input);
        }

        /// <summary>
        /// Requests to start the next submission phase of the game.
        /// </summary>
        /// <param name="gameID"></param>
        /// <param name="userID"></param>
        public void requestRound(string gameID, string userID)
        {
            queueing.requestRound(gameID, userID);
        }
    }
}
