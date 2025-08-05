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
		public static readonly string address = "http://86.11.15.228:5252/cs-nea";
		public static Dictionary<string, Dictionary<string, string>> localisation;

		public main(string userID)
		{
			hub_connection.injectForm(null, this);

			/// <summary>
			/// this is a possible way of implementing localisation efficiently

			/// 
			/// first string is the word in english
			/// the second dictionary stores the translations in the form (language, translation)
			/// 
			/// for example if the localisation is set to french:
			/// <code>
			/// localisation["friends"]["fr"]
			/// </code>
			/// "amis" would be outputted
			/// 
			/// </summary>
			/// 

			#region temp
			//userData.userID = userID;
			//userData.rank = 1200;
			//userData.localisation = "en";
			#endregion

			localisation = languages.localisation;
			interfaces.InitializeComponent(this);
			initialiseConnection(userID);
			//InitializeComponent(); // temp

			

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

			main.userData.friends = new List<friendData>()
			{
				new friendData()
				{
					userID = "beetel",
					online = true,
				},
				new friendData()
				{
					userID = "papp",
					online = false,
				},
				new friendData()
				{
					userID = "andrew",
					online = true,
				}
			};

			InitializeComponent();
		}

		public async void handleInvites(List<string> invites)
		{
			foreach (string invite in invites)
			{
				if (new confirm($"Received a friend invite from {invite}").DialogResult == System.Windows.Forms.DialogResult.OK)
				{
					await connection.InvokeAsync("addFriends", invite, userData.userID);
				}
			}
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
			
		}
		private void btn_queueKnockout_Click(object sender, EventArgs e)
		{
			
		}
	}
}
