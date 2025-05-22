using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    // handles requests for queueing games
    public partial class @connection : Hub
    {
        public void queueGame(string gameID, string userID)
        {
            // convert gameID to method name
            MethodInfo methodInfo = typeof(queueing).GetMethod($"queue_{gameID}") ?? throw new Exception($"GameID <{gameID}> could not be found");
            methodInfo.Invoke(methodInfo, [userID]);
            
        }
        
        
    }
}
