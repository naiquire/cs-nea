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
            Console.WriteLine("connected");
            return base.OnConnectedAsync();
        }
        public async void loginRequest(string userID, string password)
        {
            int success = database.loginRequest(userID, hashPassword(password));
            await Clients.Caller.SendAsync("loginSuccess", success);
        }
        public async void accountRequest(string userID, string password)
        {
            bool success = database.accountRequest(userID, hashPassword(password));
            await Clients.Caller.SendAsync("accountSuccess", success ? 1 : -1);
        }
        private static string hashPassword(string input) => Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(input)));
    }
}
