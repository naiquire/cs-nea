using Microsoft.AspNetCore.SignalR;
using server_app.databases;
using server_app.connections;
using System.Security.Cryptography;
using System.Text;

namespace server_app
{
    public class @accounts : Hub
    {
        public async void loginRequest(string userID, string password)
        {
            if (connection.map.ContainsKey(userID))
            {
				await Clients.Caller.SendAsync("loginSuccess", 3, userID);
                return;
			}

            if (database.loginRequest(userID, hashPassword(password), out int success))
            {
                Logger.Log("LOGIN", ConsoleColor.Green, $"<{ userID}> logged in with success <{success}> ");
                await Clients.Caller.SendAsync("loginSuccess", success, userID); 
            }
            else
            {
                Logger.Log("WARN", ConsoleColor.Yellow, $"Login failed for user <{userID}>");
            }
        }

        public async void accountRequest(string userID, string password, string localisation)
        {
            if (database.accountRequest(userID, hashPassword(password), localisation, out int success))
            {
                Logger.Log("ACCOUNT", ConsoleColor.Green, $"<{userID}> created account with success <{success}>");
				await Clients.Caller.SendAsync("accountSuccess", success, userID);
            }
			else
			{
                Logger.Log("WARN", ConsoleColor.Yellow, $"Account creation failed for user <{userID}>");
			}
		}
        private static string hashPassword(string input) => Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(input)));
    }
}
