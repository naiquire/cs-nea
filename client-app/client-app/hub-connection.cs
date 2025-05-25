using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app
{
    public static class hub_connection
    {
        public static HubConnection configConnection(string address)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(address)
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
            connection.On<int, string>("loginSuccess", (success, userID) =>
            {
                switch (success)
                {
                    case 0:
                        // incorrect password
                        MessageBox.Show("incorrect password");
                        break;
                    case 1:
                        // login user
                        main main = new main(userID);
                        login.ActiveForm.Hide();
                        main.ShowDialog();
                        break;
                    case -1:
                        // account does not exist
                        MessageBox.Show("account does not exist");
                        break;
                    default:
                        throw new Exception($"Unrecognised login success code < {success} >");
                }
            });
            connection.On<int, string>("accountSuccess", (success, userID) =>
            {
                switch (success)
                {
                    case 1:
                        // login user
                        main main = new main(userID);
                        login.ActiveForm.Hide();
                        main.ShowDialog();
                        break;
                    case -1:
                        // userID already exists
                        MessageBox.Show("user already exists");
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
