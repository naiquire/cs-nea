using Microsoft.AspNetCore.SignalR;
using server_app.databases;

namespace server_app
{
    public class @accounts : Hub
    {
        public async void loginRequest(string userID, string password)
        {
            int success = database.loginRequest(userID, password);
            await Clients.Caller.SendAsync("loginSuccess", success);
        }
        public async void accountRequest(string userID, string password)
        {
            bool success = database.accountRequest(userID, password);
            await Clients.Caller.SendAsync("accountSuccess", success ? 1 : -1);
        }
    }
}
