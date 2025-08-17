using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
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
		public DateTime dateCreated { get; set; }

		public int rank { get; set; }
		public Dictionary<char, statistics> statistics { get; set; }
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
		public const string address = "http://86.11.15.228:5252/cs-nea";

		public main(string userID)
		{
			hub_connection.injectForm(null, this);

			interfaces.InitializeComponent(this);
			initialiseConnection(userID);			

		}
		private async void initialiseConnection(string userID)
		{
			connection = hub_connection.configConnection(address + "/connections");
			connection = hub_connection.addHandles(connection);
			connection = await hub_connection.startConnection(connection);

			await connection.InvokeAsync("clientConnected", userID);
		}
		public void clientConnected(userData userData)
		{
			main.userData = userData;

			InitializeComponent();
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

			if (menu.profile == null && menu.game == null) // user is on home screen
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
					await connection.InvokeAsync("addFriends", invite, userData.userID);
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

		public async Task requestProfile(string userID)
		{
			userData user = await connection.InvokeAsync<userData>("requestProfile", userID);
			menu.profile = new profile(this, user);
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
	}
}
