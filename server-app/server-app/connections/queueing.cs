using Microsoft.AspNetCore.SignalR;
using server_app.games;
using System.Reflection;

namespace server_app.connections
{
    // handles requests for queueing games
    public partial class @connection : Hub
    {
        public void queueGame(string gameID, bool online, string userID) // maybe store online in db
        {
            // format gameID
            gameID = $"server_app.games.{gameID}";

            // get class object from string input
            var assembly = Assembly.GetCallingAssembly();
            var type = assembly.GetType(gameID);
            if (type == null)
            {
                throw new Exception($"gameID < {gameID} > could not be found");
            }

            // call constructor method
            ConstructorInfo[] constructor = type.GetConstructors();
            constructor[0].Invoke([userID]);



            
        }
    }
}
