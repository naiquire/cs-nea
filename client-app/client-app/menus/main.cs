using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using client_app.games;
using client_app.menus;
using Microsoft.AspNetCore.SignalR.Client;

namespace client_app
{
	public struct userData
	{
		public string userID;
		public List<friendData> friends;

		public string localisation;

		public int rank;
		public Dictionary<char, (double accuracy, TimeSpan time, int total)> statistics;
	}
	public struct friendData
	{
		public string userID;
		public string aboutMe;
		public bool online;

		public string localisation;
		public DateTime dateCreated;

		public int rank;
		public Dictionary<char, (double accuracy, TimeSpan time, int total)> statistics;
	}
	public struct game
	{
		public static string gameID;
		public static string type;
		public static List<friendData> users;
	}
	public struct menu
	{
		public static main main;
		public static profile profile;

		public static accuracy accuracy;
	}

	public partial class main : abstractMenu
	{
		public static HubConnection connection;
		public static userData userData;
		public static readonly string address = "http://86.11.15.228:5252/cs-nea";
		public static Dictionary<string, Dictionary<string, string>> localisation;

		public main(string userID)
		{
			#region temp
			userData = new userData()
			{
				userID = userID,
				localisation = "en",
				rank = 1200,
				friends = new List<friendData>()
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
				}
			};
			#endregion


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

			localisation = languages.localisation;
			base.InitializeComponent();
			//initialiseConnection();
			InitializeComponent(); // move to after userData loaded etc
			
		}
		private async void initialiseConnection()
		{
			connection = hub_connection.configConnection(address + "/connections");
			connection = hub_connection.addHandles(connection);
			connection = hub_connection.startConnection(connection);

			userData = await connection.InvokeAsync<userData>("clientConnected", userData.userID);
		}

		private async Task requestProfile(string userID)
		{
			//userData user = await connection.InvokeAsync<userData>("requestProfile", userID);

			menu.profile = new profile(this, userData);
		}

		private void btn_queueAccuracy_Click(object sender, EventArgs e)
		{
			accuracy.queue_accuracy(this);
			menu.accuracy = new accuracy();
			menu.accuracy.join_accuracy();
		}
	}
}
