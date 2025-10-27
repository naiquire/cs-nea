using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using client_app.components;
using client_app.games;
using client_app.menus;
using client_app.menus.games;
using Microsoft.AspNetCore.SignalR.Client;

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
		public string aboutMe { get; set; }
		public bool online { get; set; }

		public string localisation { get; set; }
		//public DateTime dateCreated { get; set; }

		public int rank { get; set; }
		//public Dictionary<char, statistics> statistics { get; set; }
	}
	public struct menu
	{
		public static main main;
		public static profile profile;

		public static IPlayable game;
	}

	public partial class main : Form
	{
		public static HubConnection connection;
		public static userData userData;
		//public const string address = "http://[2a0e:cb01:184:e500:8c9:b6dd:4a72:f90e]:5252/cs-nea";
		public const string address = "http://localhost:3900/cs-nea";


		public main(string userID)
		{
			hub_connection.injectForm(null, this);

			UXelements.InitializeComponent(this);
			initialiseConnection(userID);			

		}
		private async void initialiseConnection(string userID)
		{
			connection = hub_connection.configConnection($"{address}/connections");
			connection = hub_connection.addHandles(connection);
			connection = await hub_connection.startConnection(connection);

			connection.Closed += connectionClosed;

			if (!await connection.InvokeAsync<bool>("clientConnected", userID))
			{
				new alert("Failed to connect to server. Quitting application.");
				Application.Exit();
			}
		}
		private async Task connectionClosed(Exception arg)
		{
			var reconnectTask = hub_connection.startConnection(connection);
			new alert("Disconnected from server. Attempting to reconnect...");
			if (reconnectTask.Wait(30000))
			{
				await reconnectTask;
				return;
			}
			Application.Exit();
		}

		public void loadAlert(string message)
		{
			new alert(message);
		}
		public async void clientConnected(userData userData)
		{
			main.userData = userData;

			InitializeComponent();
			Application.ApplicationExit += (sender, e) => btn_close_Click(sender, e);
			await connection.InvokeAsync("loadInvites", userData.userID);
		}
		public void updateUserData(string userID, string aboutMe, string localisation)
		{
			if (userID == userData.userID)
			{
				userData.aboutMe = aboutMe;
				userData.localisation = localisation;
			}
			else
			{
				int index = -1;
				for (int i = 0; i < userData.friends.Count; i++)
				{
					if (userData.friends[i].userID == userID)
					{
						index = i;
						break;
					}
				}
				if (index == -1)
				{
                    new alert("Failed to update user data.");
                    return;
                }
				
                var friend = userData.friends[index];
                friend.aboutMe = aboutMe;
                friend.localisation = localisation;
                userData.friends[index] = friend;
            }
			
			bool isPlaying = menu.game != null;
			bool isHome = menu.profile == null;
			bool isSelfUpdate = userID == userData.userID;

			if (!isPlaying)
			{
				if (isHome && isSelfUpdate)
				{
					btn_home.PerformClick();
				}
				else if (menu.profile.getUserID() == userID)
				{
					userData refresh = menu.profile.getUserData();
					menu.profile = new profile(this, refresh);
					if (userID == userData.userID)
					{
						UXelements.configUserDataPanel(this, refresh);
					}
				}
			}
		}

		public void updateOnline(string user, bool online)
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
			var copy = userData.friends[index];
			copy.online = online;
			userData.friends[index] = copy;

			if (menu.game == null) // user is not in-game
			{
				configFriendsPanel();
			}
		}
		public void updateFriendData(friendData data)
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

			if (menu.game == null) // user is not in-game
			{
				configFriendsPanel();
			}
		}
		public void removeFriend(string friendID)
		{
			foreach (var friend in userData.friends)
			{
				if (friend.userID == friendID)
				{
					userData.friends.Remove(friend);
					break;
				}
			}

			if (menu.game == null) // user is not in-game
			{
				configFriendsPanel();
			}
		}

		public async void handleInvites(List<string> invites)
		{
			foreach (string invite in invites)
			{
				if (new confirm($"Received a friend invite from {invite}").DialogResult == DialogResult.OK)
				{
					if (!await connection.InvokeAsync<bool>("addFriends", invite, userData.userID))
					{
						new alert("Failed to accept friend invite.");
					}
				}
			}
		}

		public static (string, string, string) calculateStatsOverview(userData user)
		{
			string rank = user.rank.ToString();

			int sum = 0;
			foreach (var letter in user.statistics.Keys)
			{
				sum += user.statistics[letter].total;
			}
			string total = sum.ToString();

			double mean = 0;
			foreach (var letter in user.statistics.Keys)
			{
				mean += user.statistics[letter].accuracy;
			}
			mean /= user.statistics.Count;
			string accuracy = (Math.Round((100 * mean), 2)).ToString();

			return (rank, total, accuracy);
		}

		private async void btn_userSearch_Click(object sender, EventArgs e)
		{
			string userID = txt_userSearch.Text;
			if (!string.IsNullOrWhiteSpace(userID))
			{
				txt_userSearch.ResetText();
				await requestProfile(userID);
			}
		}
		public async Task requestProfile(string userID)
		{
			userData user = await connection.InvokeAsync<userData>("requestProfile", userID);
			if (user.userID != userID)
			{
				new alert($"Could not find user with username: {userID}");
			}
			else
			{
				menu.profile = new profile(this, user);
			}
		}

		private void btn_queueAccuracy_Click(object sender, EventArgs e)
		{
			menu.game = new accuracy(this);
			menu.game.queueGame();
		}
		private void btn_queue1v1_Click(object sender, EventArgs e)
		{
			menu.game = new versus(this);
			menu.game.queueGame();
		}
		private void btn_queueKnockout_Click(object sender, EventArgs e)
		{
			menu.game = new knockout(this);
			menu.game.queueGame();
		}

		public async void btn_home_Click(object sender, EventArgs e)
		{
			if (menu.game != null)
			{
				await connection.InvokeAsync("dequeueGame", menu.game.getGameID(), userData.userID);
			}

			// dispose all other classes
			menu.profile = null;
			menu.game = null;

			InitializeComponent();
		}
		public async void btn_close_Click(object sender, EventArgs e)
		{
			Hide();
			if (menu.game != null)
			{
				await connection.InvokeAsync("dequeueGame", menu.game.getGameID(), userData.userID);
			}
			await connection.InvokeAsync("clientDisconnected", main.userData.userID);
			Close();
		}
	}
}
