using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace client_app
{
    public static class hub_connection
    {
        public static HubConnection configConnection()
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(main.address)
                .Build();
            return connection;
        }
        public static HubConnection startConnection(HubConnection connection)
        {
            connection.StartAsync();
            return connection;
        }
        public static HubConnection addHandles(HubConnection connection)
        {
            connection.On<int>("loginSuccess", (success) =>
            {
                switch (success)
                {
                    case 0:
                        // incorrect password
                        break;
                    case 1:
                        // login user
                        break;
                    case -1:
                        // account does not exist
                        break;
                    default:
                        throw new Exception($"Unrecognised login success code < {success} >");
                }
            });
            connection.On<int>("accountSuccess", (success) =>
            {
                switch (success)
                {
                    case 1:
                        // login user
                        break;
                    case -1:
                        // userID already exists
                        break;
                    default:
                        throw new Exception($"Unrecognised account success code < {success} >");
                }
            });

            connection.On<string>("startGame", (gameID) =>
            {
                MethodInfo methodInfo = typeof(main).GetMethod($"initialise_{gameID}") ?? throw new Exception($"GameID <{gameID}> could not be found");
                methodInfo.Invoke(methodInfo, null);
            });



            return connection;
        }
    }
}
