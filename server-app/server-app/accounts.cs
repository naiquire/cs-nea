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
                Console.WriteLine($"User <{userID}> logged in with success <{success}>");
                await Clients.Caller.SendAsync("loginSuccess", success, userID); 
            }
            else
            {
                database.outputException($"Login failed for user <{userID}>");
            }
        }

        public async void accountRequest(string userID, string password, string localisation)
        {
            if (database.accountRequest(userID, hashPassword(password), localisation, out int success))
            {
				Console.WriteLine($"User <{userID}> created account with success <{success}>");
				await Clients.Caller.SendAsync("accountSuccess", success, userID);
            }
			else
			{
				database.outputException($"Account creation failed for user <{userID}>");
			}
		}
        private static string hashPassword(string input) => Encoding.UTF8.GetString(SHA512.HashData(Encoding.UTF8.GetBytes(input)));
    }
}
