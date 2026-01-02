using client_app.components;
using client_app.menus.games;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace client_app
{
	public static class hub_connection
	{
		private static Login login;
		private static Main main;

		public static void InjectForm(Login l, Main m)
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
					login?.Invoke(new Action(() => login.lbl_information.Text = Languages.localisation["An error occured while connecting to the server"][Languages.languageCodes[login.GetLanguageIndex()]]));
				}
			}

			return connection;
		}

		public static HubConnection addLoginHandles(HubConnection connection)
		{
			connection.On<int, string>("loginSuccess", (success, userID) =>
			{
				login.lbl_information.Invoke(new Action(() => login.HandleLoginSuccess(success, userID)));
			});
			connection.On<int, string>("accountSuccess", (success, userID) =>
			{
				login.lbl_information.Invoke(new Action(() => login.HandleAccountCreationSuccess(success, userID)));
			});

			return connection;
		}

		public static HubConnection addHandles(HubConnection connection)
		{
			connection.On<userData>("receiveUserData", (userData) =>
			{
				main.Invoke(new Action(() => main.ClientConnected(userData)));
			});
			connection.On<string, string, string>("updateUserData", (userID, aboutMe, localisation) =>
			{
				main.Invoke(new Action(() => main.UpdateUserData(userID, aboutMe, localisation)));
			});
			connection.On<List<string>>("receiveInvites", (invites) =>
			{
				main.Invoke(new Action(() => main.HandleInvites(invites)));
			});
			connection.On<friendData>("updateFriendData", (data) =>
			{
				main.Invoke(new Action(() => main.UpdateFriendData(data)));
			});
			connection.On<string>("removeFriend", (friendID) =>
			{
				main.Invoke(new Action(() => main.RemoveFriend(friendID)));
			});
			connection.On<string, bool>("updateOnline", (user, online) =>
			{
				main.Invoke(new Action(() => main.UpdateOnline(user, online)));
			});

			connection.On<char, statistics>("updateStatistics", (letter, statistics) =>
			{
				Main.userData.statistics[letter] = statistics;
			});

			connection.On<List<friendData>>("updateUsers", (users) =>
			{
				main.panel_main.Invoke(new Action(() => menu.game.UpdateUsers(users)));
			});

			connection.On("awaitStart", () =>
			{
				main.Invoke(new Action(() => menu.game.AwaitStart()));
			});
			connection.On("startGame", () =>
			{
				main.Invoke(new Action(() => menu.game.StartGame()));
			});

			connection.On("awaitRound", () =>
			{
				main.Invoke(new Action(() => menu.game.AwaitRound()));
			});
			connection.On<char>("receiveLetter", (letter) =>
			{
				main.Invoke(new Action(() => menu.game.SubmissionPhase(letter)));
			});

			connection.On<bool, double, TimeSpan>("receiveResults", (correct, accuracy, time) =>
			{
				main.Invoke(new Action(() => menu.game.EvaluationPhase(correct, accuracy, time)));
			});
			connection.On<string>("receiveVersusResult", (winner) =>
			{
				if (menu.game.GetGameType() == Games.Versus)
				{
					main.Invoke(new Action(() =>
					{
						Versus game = (Versus)menu.game;
						game.VersusResults(winner);
					}));
				}
				else
				{
					throw new Exception("unexpected game type");
				}
			});
			connection.On<List<string>>("receiveKnockoutResult", (aliveUsers) =>
			{
				if (menu.game.GetGameType() == Games.Elimination)
				{
					main.Invoke(new Action(() =>
					{
						Elimination game = (Elimination)menu.game;
						game.EliminationResults(aliveUsers);
					}));
				}
				else
				{
					throw new Exception("unexpected game type");
				}
			});

			connection.On("endGame", () =>
			{
				main.Invoke(new Action(() => menu.game.EndGame()));
			});
			connection.On<int>("updateRank", (rank) =>
			{
				if (menu.game.GetGameType() == Games.Versus)
				{
					main.Invoke(new Action(() =>
					{
						Versus game = (Versus)menu.game;
						game.UpdateRank(rank);
					}));
				}
				else
				{
					throw new Exception("unexpected game type");
				}
			});

			connection.On<string>("alert", (message) =>
			{
				main.Invoke(new Action(() => Main.LoadAlert(message)));
			});

			return connection;
		}
	}
}
