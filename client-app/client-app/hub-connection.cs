using client_app.menus.games;
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
        public static HubConnection addLoginHandles(HubConnection connection)
        {
            connection.On<int, string>("loginSuccess", (success, userID) =>
            {
                login.lbl_information.Invoke(new Action(() => { login.handleLoginSuccess(success, userID); }));
            });
            connection.On<int, string>("accountSuccess", (success, userID) =>
            {
                login.lbl_information.Invoke(new Action(() => { login.handleAccountCreationSuccess(success, userID); }));
            });

            return connection;
        }
        public static HubConnection addHandles(HubConnection connection)
        {
            connection.On<userData>("receiveUserData", (userData) =>
            {
                main.Invoke(new Action(() => { main.clientConnected(userData); }));
            });
            connection.On<List<string>>("receiveInvites", (invites) =>
            {
                main.Invoke(new Action(() => { main.handleInvites(invites); }));
            });
            connection.On<friendData>("updateFriendData", (data) =>
            {
                for (int i = 0; i < main.userData.friends.Count; i++)
                {
                    if (main.userData.friends[i].userID == data.userID)
                    {
                        main.userData.friends[i] = data;
                        return;
                    }
                }
				main.userData.friends.Add(data);
			});
            connection.On<char, double, TimeSpan, int>("updateStatistics", (letter, accuracy, time, total) =>
            {
                main.userData.statistics[letter] = (accuracy, time, total);
            });

            connection.On<List<friendData>>("updateUsers", (users) =>
            {
                main.panel_main.Invoke(new Action(() => { menu.game.updateUsers(users); }));
            });

            connection.On("awaitStart", () => menu.game.awaitStart());
            connection.On("startGame", () => menu.game.startGame());

            connection.On<char>("receiveLetter", (letter) =>
            {
                menu.game.submissionPhase(letter);
            });

            connection.On<bool, double, TimeSpan>("receiveResults", (correct, accuracy, time) =>
            {
                menu.game.updateStats(correct, accuracy, time);
                menu.game.evaluationPhase();
            });



            return connection;
        }
    }
}
