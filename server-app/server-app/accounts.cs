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
            Console.WriteLine($"Client connected: <{Context.ConnectionId}>");
			return base.OnConnectedAsync();
		}

        /// <summary>
        /// Handles a login request from a client, and returns a success code.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        public async void loginRequest(string userID, string password)
        {
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

        /// <summary>
        /// Handles an account creation request from a client, and returns a success code.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
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
