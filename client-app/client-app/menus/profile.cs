using client_app.components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus
{
    public partial class Profile : Form
    {
        private readonly Main _main;
        private readonly userData _user;
        public Profile(Main main, userData user)
        {
            this._main = main;
            this._user = user;

            InitializeComponent();
            SetupButtons();
        }
        public string getUserID() => _user.userID;
        public userData getUserData() => _user;
        private void SetupButtons()
        {
            if (_user.userID == Main.userData.userID)
            {
				btn_addFriends.Enabled = false;
				btn_addFriends.Hide();
				btn_removeFriends.Enabled = false;
				btn_removeFriends.Hide();
            }

            bool isFriend = false;
            foreach (var friend in Main.userData.friends)
            {
                if (friend.userID == _user.userID)
                {
                    isFriend = true;
                    
                    break;
                }
            }

            if (isFriend)
            {
				btn_addFriends.Enabled = false;
				//btn_addFriends.Hide();
			}
            else
            {
				btn_removeFriends.Enabled = false;
				//btn_removeFriends.Hide();
			}
        }

        private async void btn_addFriends_Click(object sender, EventArgs e)
        {
            if (Main.connection.State != HubConnectionState.Connected)
            {
                return;
            }

            btn_addFriends.Enabled = false;
            if (!await Main.connection.InvokeAsync<bool>("sendInvite", _user.userID, Main.userData.userID))
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
            if (!await Main.connection.InvokeAsync<bool>("removeFriends", _user.userID, Main.userData.userID))
            {
                Main.LoadAlert("Failed to remove friend. Please try again");
            }
        }
    }
}
