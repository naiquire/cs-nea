using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using System.Security.Cryptography;
using System.Text;

namespace server_app
{
    public class @accounts : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public async void loginRequest(string userID, string password)
        {
            if (database.loginRequest(userID, hashPassword(password), out int success))
            {
                await Clients.Caller.SendAsync("loginSuccess", success); 
            }
        }
        public async void accountRequest(string userID, string password)
        {
            if (database.accountRequest(userID, hashPassword(password), out int success))
            {
                await Clients.Caller.SendAsync("accountSuccess", success);
            }
        }
        private static string hashPassword(string input) => Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(input)));
    }
}
