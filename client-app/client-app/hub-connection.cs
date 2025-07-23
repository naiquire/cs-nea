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
                .WithUrl(address, options =>
                {
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets |
                                         Microsoft.AspNetCore.Http.Connections.HttpTransportType.ServerSentEvents |
                                         Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                })
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
                    case 2:
                        // account does not exist
                        MessageBox.Show("account does not exist");
                        break;
                    case -1:
                        // error occured
                        MessageBox.Show("an error occurred try again");
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
                    case 0:
                        // userID already exists
                        MessageBox.Show("user already exists");
                        break;
                    case -1:
                        // error occured
                        MessageBox.Show("an error occurred try again");
                        break;
                    default:
                        throw new Exception($"Unrecognised account success code < {success} >");
                }
            });

            connection.On<string, string, List<friendData>>("receiveJoinConfirm", (gameID, type, users) =>
            {
                game.gameID = gameID;
                game.type = type;
                game.users = users;

				MethodInfo methodInfo = typeof(main).GetMethod($"join_{game.type}") ?? throw new Exception($"GameID <{game.type}> could not be found");
				methodInfo.Invoke(methodInfo, null);
			});
            connection.On<string>("startGame", (aaa) =>
            {
                MethodInfo methodInfo = typeof(main).GetMethod($"start_{game.type}") ?? throw new Exception($"GameID <{game.type}> could not be found");
                methodInfo.Invoke(methodInfo, null);
            });

            connection.On<char>("receiveLetter", (letter) =>
            {
                MethodInfo methodInfo = typeof(main).GetMethod($"round_{game.type}") ?? throw new Exception($"GameID <{game.type}> could not be found");
                methodInfo.Invoke(methodInfo, new object[letter]);
            });




            return connection;
        }
    }
}
