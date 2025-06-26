using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    // handles requests for queueing games
    public partial class @connection : Hub
    {
        public void queueGame(string gameType, string userID)
        {
            // convert gameID to method name
            try
            {
                MethodInfo? methodInfo = typeof(queueing).GetMethod($"queue_{gameType}");
                methodInfo?.Invoke(methodInfo, [userID]);
            }
            catch (ArgumentNullException ex)
            {
                database.outputException($"Game <{gameType}> could not be found");
                database.outputException(ex);
            }
            
            
        }
    }
}
