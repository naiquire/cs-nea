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
    public partial class profile : Form
    {
        private readonly main main;
        private readonly userData user;
        public profile(main main, userData user)
        {
            this.main = main;
            this.user = user;

            InitializeComponent();
        }

        private async void btn_addFriends_Click(object sender, EventArgs e)
        {
            btn_addFriends.Enabled = false;
            await main.connection.InvokeAsync("sendInvite", user.userID, main.userData.userID);
        }

        private async void btn_removeFriends_Click(object sender, EventArgs e)
        {
            btn_removeFriends.Enabled = false;
            await main.connection.InvokeAsync("removeFriends", user.userID, main.userData.userID);
            btn_addFriends.Enabled = true;
        }
    }
}
