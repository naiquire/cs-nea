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

namespace client_app
{
    public partial class login : Form
    {
        HubConnection connection;
        public login()
        {
            InitializeComponent();
            initialiseConnection();

            btn_requestAccount.Enabled = false;
            btn_requestAccount.Visible = false;
        }
        private void initialiseConnection()
        {
            connection = hub_connection.configConnection(main.address + "/accounts");
            connection = hub_connection.addHandles(connection);
            connection = hub_connection.startConnection(connection);
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            string userID = txt_userID.Text.Trim();
            string password = txt_password.Text;

            await connection.InvokeAsync("loginRequest", userID, password);
        }

        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            btn_login.Enabled = false;
            btn_login.Visible = false;
            btn_createAccount.Enabled = false;
            btn_createAccount.Visible = false;

            btn_requestAccount.Enabled = true;
            btn_requestAccount.Visible = true;
        }
        private async void btn_requestAccount_Click(object sender, EventArgs e)
        {
            string userID = txt_userID.Text.Trim();
            string password = txt_password.Text;

            await connection.InvokeAsync("accountRequest", userID, password);
        }
    }
}
