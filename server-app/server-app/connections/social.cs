using Microsoft.AspNetCore.SignalR;

namespace server_app.connections
{
    // handles requests related to leaderboards etc
    public class @social : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
