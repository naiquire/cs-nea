using Microsoft.AspNetCore.SignalR;
using server_app.games;

namespace server_app.connections
{
    // handles requests for queueing games
    public class @queueing : Hub
    {
        
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public void queueGame(string gameID, bool online, string userID) // maybe store online in db
        {
            if (online)
            {

            }
            else
            {
                if (gameID == "accuracy")
                {
                    new accuracy(userID);
                }
            }
        }
    }
}
