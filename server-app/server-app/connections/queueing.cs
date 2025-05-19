using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    // handles requests for queueing games
    public partial class @connection : Hub
    {
        public void queueGame(string gameID, string userID) // maybe store online in db
        {
            
            queueing.queueGame(userID, gameID);



            
        }
    }
}
