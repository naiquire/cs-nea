using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Windows.Forms;

namespace client_app.menus
{
	public partial class Profile : Form
	{
		private readonly Main main;
		private readonly userData user;
		public string GetUserID() => user.userID;
		public userData GetUserData() => user;
		public Profile(Main main, userData user)
		{
			this.main = main;
			this.user = user;

			InitialiseComponent();
			SetupButtons();
		}
		private void SetupButtons()
		{
			if (user.userID == Main.userData.userID)
			{
				btn_addFriends.Enabled = false;
				btn_addFriends.Hide();
				btn_removeFriends.Enabled = false;
				btn_removeFriends.Hide();
			}

			bool isFriend = false;
			foreach (var friend in Main.userData.friends)
			{
				if (friend.userID == user.userID)
				{
					isFriend = true;
					break;
				}
			}

			if (isFriend)
			{
				btn_addFriends.Enabled = false;
			}
			else
			{
				btn_removeFriends.Enabled = false;
			}
		}

		private async void btn_addFriends_Click(object sender, EventArgs e)
		{
			if (Main.connection.State != HubConnectionState.Connected)
			{
				return;
			}

			btn_addFriends.Enabled = false;
			if (!await Main.connection.InvokeAsync<bool>("sendInvite", user.userID, Main.userData.userID))
			{
				Main.LoadAlert("Failed to send friend invite. Please try again.");
			}
		}
		private async void btn_removeFriends_Click(object sender, EventArgs e)
		{
			if (Main.connection.State != HubConnectionState.Connected)
			{
				return;
			}

			btn_removeFriends.Enabled = false;
			if (!await Main.connection.InvokeAsync<bool>("removeFriends", user.userID, Main.userData.userID))
			{
				Main.LoadAlert("Failed to remove friend. Please try again");
			}
		}
	}
}
