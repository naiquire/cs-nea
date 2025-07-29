using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app
{
    public static class hub_connection
    {
        private static login login;
        private static main main;
        public static void injectForm(login l, main m)
        {
            main = m;
            login = l;
        }
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
        public async static Task<HubConnection> startConnection(HubConnection connection)
        {
            await connection.StartAsync();
            return connection;
        }
        public static HubConnection addHandles(HubConnection connection)
        {
            connection.On<int, string>("loginSuccess", (success, userID) =>
            {
                login.lbl_information.Invoke(new Action(() => { login.handleLoginSuccess(success, userID); }));
            });
            connection.On<int, string>("accountSuccess", (success, userID) =>
            {
				login.lbl_information.Invoke(new Action(() => { login.handleAccountCreationSuccess(success, userID); }));
			});
            connection.On<userData>("receiveUserData", (userData) =>
            {
                main.Invoke(new Action(() => { main.clientConnected(userData); }));
            });
            connection.On<List<string>>("receiveInvites", (invites) =>
            {
                main.Invoke(new Action(() => { main.handleInvites(invites); }));
            });

            connection.On<string, string, List<friendData>>("receiveJoinConfirm", (gameID, type, users) =>
            {
                game.gameID = gameID;
                game.type = type;
			});
            connection.On<List<friendData>>("updateUsers", (datas) =>
            {
                game.users = datas;
            });
            connection.On<string>("startGame", (aaa) =>
            {
                // no work
                MethodInfo methodInfo = typeof(main).GetMethod($"start_{game.type}") ?? throw new Exception($"GameID <{game.type}> could not be found");
                methodInfo.Invoke(methodInfo, null);
            });

            connection.On<char>("receiveLetter", (letter) =>
            {
                // no work either
                MethodInfo methodInfo = typeof(main).GetMethod($"round_{game.type}") ?? throw new Exception($"GameID <{game.type}> could not be found");
                methodInfo.Invoke(methodInfo, new object[letter]);
            });




            return connection;
        }
    }
}
