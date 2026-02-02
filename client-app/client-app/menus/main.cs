using client_app.components;
using client_app.menus;
using client_app.menus.games;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app
{
	public struct userData
	{
		public string userID { get; set; }
		public string aboutMe { get; set; }
		public List<friendData> friends { get; set; }
		public string localisation { get; set; }
		public DateTime dateCreated { get; set; }
		public int rank { get; set; }
		public Dictionary<char, statistics> statistics { get; set; }
	}
	public struct @statistics
	{
		public double accuracy { get; set; }
		public TimeSpan time { get; set; }
		public int total { get; set; }
	}
	public struct friendData
	{
		public string userID { get; set; }
		public bool online { get; set; }
		public int rank { get; set; }
	}
	public struct menu
	{
		public static Main main;
		public static Profile profile;
		public static IPlayable game;
	}

	public partial class Main : Form
	{
		public static HubConnection connection;
		public static userData userData;
		public const string address = "http://192.168.0.251:5252/cs-nea";

		public Main(string userID, string defaultLocalisation)
		{
			userData.localisation = defaultLocalisation;
			hub_connection.InjectForm(null, this);

			UXelements.InitialiseComponent(this);
			InitialiseConnection(userID);
		}
		private async void InitialiseConnection(string userID)
		{
			connection = hub_connection.configConnection($"{address}/connections");
			connection = hub_connection.addHandles(connection);
			connection = await hub_connection.startConnection(connection);

			connection.Closed += ConnectionClosed;

			if (connection.State != HubConnectionState.Connected)
			{
				Close();
			}

			if (!await connection.InvokeAsync<bool>("clientConnected", userID))
			{
				LoadAlert("Failed to connect to server. Quitting application.");
				Close();
			}
		}

		private Task ConnectionClosed(Exception ex)
		{
			try { btn_home.Invoke(new Action(() => btn_home.PerformClick())); } catch { }

			var alert = LoadAlert("Disconnected from server. Restarting application...");
			try { Invoke(new Action(() => Close())); } catch { }
			return Task.CompletedTask;
		}

		public static AlertForm LoadAlert(string message, bool addCloseButton = true, bool autoShow = true)
		{
			return new AlertForm(message, addCloseButton, autoShow);
		}
		public async void ClientConnected(userData userData)
		{
			Main.userData = userData;

			InitialiseComponent();

			if (connection.State == HubConnectionState.Connected)
			{
				await connection.InvokeAsync("loadInvites", userData.userID);
			}
		}
		public void UpdateUserData(string userID, string aboutMe, string localisation)
		{
			if (userID != userData.userID)
			{
				return;
			}

			userData.aboutMe = aboutMe;
			userData.localisation = localisation;

			if (menu.game != null)
			{
				return;
			}

			if (menu.profile == null)
			{
				// if home then refresh all
				btn_home.PerformClick();
			}
			else
			{
				UpdatePageText("Profile");
				ConfigFriendsPanel();
				UXelements.ConfigUserDataPanel(this, userData);
				if (menu.profile.GetUserID() == userData.userID)
				{
					// if viewing own profile then refresh
					menu.profile = new Profile(this, userData);
				}
			}
		}

		public void UpdateOnline(string user, bool online)
		{
			int index = 0;
			for (int i = 0; i < userData.friends.Count; i++)
			{
				if (userData.friends[i].userID == user)
				{
					index = i;
					break;
				}
			}
			friendData copy = userData.friends[index];
			copy.online = online;
			userData.friends[index] = copy;

			if (menu.game == null)
			{
				// user is not in-game
				ConfigFriendsPanel();
			}
		}
		public void UpdateFriendData(friendData data)
		{
			bool exists = false;
			for (int i = 0; i < userData.friends.Count; i++)
			{
				if (userData.friends[i].userID == data.userID)
				{
					exists = true;
					userData.friends[i] = data;
				}
			}
			if (!exists)
			{
				userData.friends.Add(data);
			}

			if (menu.game == null)
			{
				// user is not in-game
				ConfigFriendsPanel();
			}
		}
		public void RemoveFriend(string friendID)
		{
			foreach (var friend in userData.friends)
			{
				if (friend.userID == friendID)
				{
					userData.friends.Remove(friend);
					break;
				}
			}

			if (menu.game == null)
			{
				// user is not in-game
				ConfigFriendsPanel();
			}
		}

		public async void HandleInvites(List<string> invites)
		{
			foreach (string invite in invites)
			{
				if (new ConfirmForm($"Received a friend invite from {invite}").DialogResult == DialogResult.OK)
				{
					if (connection.State != HubConnectionState.Connected)
					{
						LoadAlert("Failed to accept friend invite.");
					}
					if (!await connection.InvokeAsync<bool>("addFriends", invite, userData.userID))
					{
						LoadAlert("Failed to accept friend invite.");
					}

				}
			}
		}

		public static (string, string, string) CalculateStatsOverview(userData user)
		{
			string rank = user.rank.ToString();

			int totalLetters = 0;
			double meanAccuracy = 0;
			foreach (var letter in user.statistics.Keys)
			{
				totalLetters += user.statistics[letter].total;
				meanAccuracy += user.statistics[letter].accuracy;
			}
			
			string total = totalLetters.ToString();
			meanAccuracy /= user.statistics.Count;
			string accuracy = (Math.Round(100 * meanAccuracy, 2)).ToString();

			return (rank, total, accuracy);
		}

		private async void btn_userSearch_Click(object sender, EventArgs e)
		{
			string userID = txt_userSearch.Text;
			if (!string.IsNullOrWhiteSpace(userID))
			{
				txt_userSearch.ResetText();
				await RequestProfile(userID);
			}
		}
		public async Task RequestProfile(string userID)
		{
			if (connection.State != HubConnectionState.Connected)
			{
				return;
			}

			userData user = await connection.InvokeAsync<userData>("requestProfile", userID);
			if (user.userID != userID)
			{
				LoadAlert($"Could not find user with username: {userID}");
			}
			else
			{
				UpdatePageText("Profile");
				menu.profile = new Profile(this, user);
			}
		}

		public void UpdatePageText(string text)
		{
			panel_topLeft.Controls.Clear();
			lbl_menu = new Guna.UI2.WinForms.Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift", 24.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(247, 113, 163),
				Location = new Point(0, 0),
				Name = "lbl_menu",
				Size = new Size(300, 100),
				TabIndex = 0,
				Text = Languages.localisation[text][userData.localisation],
				TextAlignment = ContentAlignment.MiddleCenter,
			};

			panel_topLeft.Controls.Add(lbl_menu);
		}

		private void btn_queueAccuracy_Click(object sender, EventArgs e)
		{
			UpdatePageText("Accuracy");

			menu.game = new Accuracy(this);
			menu.game.QueueGame();
		}
		private void btn_queueVersus_Click(object sender, EventArgs e)
		{
			UpdatePageText("Versus");

			menu.game = new Versus(this);
			menu.game.QueueGame();
		}
		private void btn_queueElimination_Click(object sender, EventArgs e)
		{
			UpdatePageText("Knockout");

			menu.game = new Elimination(this);
			menu.game.QueueGame();
		}

		public async void btn_home_Click(object sender, EventArgs e)
		{
			UpdatePageText("Home");
			if (connection.State != HubConnectionState.Connected)
			{
				return;
			}

			if (menu.game != null)
			{
				await connection.InvokeAsync("dequeueGame", menu.game.GetGameID(), userData.userID);
			}

			// dispose all other classes
			menu.profile = null;
			menu.game = null;

			InitialiseComponent();
		}
		public async void btn_close_Click(object sender, EventArgs e)
		{
			if (connection.State != HubConnectionState.Connected)
			{
				return;
			}

			Hide();
			if (menu.game != null)
			{
				await connection.InvokeAsync("dequeueGame", menu.game.GetGameID(), userData.userID);
			}
			await connection.InvokeAsync("clientDisconnected", userData.userID);

			// dispose all other classes
			menu.profile = null;
			menu.game = null;

			Close();
		}
	}
}
