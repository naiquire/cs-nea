using client_app.components;
using client_app.games;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace client_app
{
	public static class hub_connection
	{
		private static login login;
		private static main main;

		public static void injectForm(login l, main m)
		{
			if (m != null) main = m;
			if (l != null) login = l;
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
			while (connection.State == HubConnectionState.Disconnected)
			{
				try
				{
					await connection.StartAsync();
					login?.lbl_information.ResetText();
				}
				catch
				{
					login?.Invoke(new Action(() => login.lbl_information.Text = languages.localisation["An error occured while connecting to the server"][main.userData.localisation]));
				}
			}

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
				main.Invoke(new Action(() => main.clientConnected(userData)));
			});
			connection.On<string, string, string>("updateUserData", (userID, aboutMe, localisation) =>
			{
				main.Invoke(new Action(() => main.updateUserData(userID, aboutMe, localisation)));
			});
			connection.On<List<string>>("receiveInvites", (invites) =>
			{
				main.Invoke(new Action(() => main.handleInvites(invites)));
			});
			connection.On<friendData>("updateFriendData", (data) =>
			{
				main.Invoke(new Action(() => main.updateFriendData(data)));
			});
			connection.On<string>("removeFriend", (friendID) =>
			{
				main.Invoke(new Action(() => main.removeFriend(friendID)));
			});
			connection.On<string, bool>("updateOnline", (user, online) =>
			{
				main.Invoke(new Action(() => main.updateOnline(user, online)));
			});

			connection.On<char, statistics>("updateStatistics", (letter, statistics) =>
			{
				main.userData.statistics[letter] = statistics;
			});

			connection.On<List<friendData>>("updateUsers", (users) =>
			{
				main.panel_main.Invoke(new Action(() => menu.game.updateUsers(users)));
			});

			connection.On("awaitStart", () =>
			{
				main.Invoke(new Action(() => menu.game.awaitStart()));
			});
			connection.On("startGame", () =>
			{
				main.Invoke(new Action(() => menu.game.startGame()));
			});

			connection.On("awaitRound", () =>
			{
				main.Invoke(new Action(() => menu.game.awaitRound()));
			});
			connection.On<char>("receiveLetter", (letter) =>
			{
				main.Invoke(new Action(() => menu.game.submissionPhase(letter)));
			});

			connection.On<bool, double, TimeSpan>("receiveResults", (correct, accuracy, time) =>
			{
				main.Invoke(new Action(() => menu.game.evaluationPhase(correct, accuracy, time)));
			});
			connection.On<string>("receiveVersusResult", (winner) =>
			{
				if (menu.game.getType() == "versus")
				{
					main.Invoke(new Action(() =>
					{
						versus game = (versus)menu.game;
						game.versusResults(winner);
					}));
				}
				else
				{
					throw new Exception("unexpected game type");
				}
			});
			connection.On<List<string>>("receiveKnockoutResult", (aliveUsers) =>
			{
				if (menu.game.getType() == "knockout")
				{
					main.Invoke(new Action(() =>
					{
						knockout game = (knockout)menu.game;
						game.knockoutResults(aliveUsers);
					}));
				}
				else
				{
					throw new Exception("unexpected game type");
				}
			});

			connection.On("endGame", () =>
			{
				main.Invoke(new Action(() => menu.game.endGame()));
			});
			connection.On<int>("updateRank", (rank) =>
			{
				if (menu.game.getType() == "versus")
				{
					main.Invoke(new Action(() =>
					{
						versus game = (versus)menu.game;
						game.updateRank(rank);
					}));
				}
				else
				{
					throw new Exception("unexpected game type");
				}
			});

			connection.On<string>("alert", (message) =>
			{
				main.Invoke(new Action(() => main.loadAlert(message)));
			});

			return connection;
		}
	}
}
