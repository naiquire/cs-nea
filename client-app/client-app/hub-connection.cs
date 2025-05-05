using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        break;
                    case 1:
                        break;
                    case -1:
                        break;
                    default:
                        throw new Exception($"Unrecognised login success code < {success} >");
                }
            });


            return connection;
        }
    }
}
