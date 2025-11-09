using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.connections;
using System.Security.Cryptography;
using System.Text;

namespace server_app
{
    public class @accounts : Hub
    {
		private static string hashPassword(string input) => Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(input)));
		public async void loginRequest(string userID, string password)
        {
            if (Connection.map.ContainsKey(userID))
            {
                await Clients.Caller.SendAsync("loginSuccess", 3, userID);
                return;
            }

            if (database.loginRequest(userID, hashPassword(password), out int success))
            {
                Logger.Log("LOGIN", "lime", $"<{userID}> logged in with success <{success}> ");
            }
            else
            {
                Logger.Log("WARN", "yellow", $"Login failed for user <{userID}>");
            }
            await Clients.Caller.SendAsync("loginSuccess", success, userID);
        }

        public async void accountRequest(string userID, string password, string localisation)
        {
            if (database.accountRequest(userID, hashPassword(password), localisation, out int success))
            {
                Logger.Log("ACCOUNT", "fuchsia", $"<{userID}> created account with success <{success}>");
            }
            else
            {
                Logger.Log("WARN", "yellow", $"Account creation failed for user <{userID}>");
            }
            await Clients.Caller.SendAsync("accountSuccess", success, userID);
        }
    }
}
