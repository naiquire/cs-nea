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
        private int languageIndex = 0;
        public login()
        {
            InitializeComponent();
            controlEventConfigs();
			btn_language.Text = languages.supportedLanguages[languageIndex];
			initialiseConnection();
        }
        private async void initialiseConnection()
        {
            connection = hub_connection.configConnection(main.address + "/accounts");
            connection = hub_connection.addHandles(connection);
            connection = await hub_connection.startConnection(connection);

            this.txt_connection.Text = "Connected";
            pic_connecting.Stop();
        }

        private async void btn_login_Click(object sender, EventArgs e)
        {
            string userID = txt_userID.Text.Trim();
            string password = txt_password.Text;

            await connection.InvokeAsync("loginRequest", userID, password);
        }

        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(btn_login);
            this.Controls.Remove(btn_createAccount);

            this.txt_password.Location = new Point(560, 260);

            this.Controls.Add(txt_passwordconfirm);
            this.Controls.Add(btn_requestAccount);
        }
        private async void btn_requestAccount_Click(object sender, EventArgs e)
        {
            string userID = txt_userID.Text.Trim();
            string password = txt_password.Text;
            string localisation = languages.supportedLanguages[languageIndex];

            await connection.InvokeAsync("accountRequest", userID, password, localisation);
        }

		private void btn_language_Click(object sender, EventArgs e)
		{
            languageIndex++;
            if (languageIndex == languages.supportedLanguages.Count)
            {
                languageIndex = 0;
            }
			btn_language.Text = languages.supportedLanguages[languageIndex];

            txt_userID.PlaceholderText = languages.localisation["Username"][languages.languageCodes[languageIndex]];
            txt_password.PlaceholderText = languages.localisation["Password"][languages.languageCodes[languageIndex]];
            lbl_header.Text = languages.localisation["Account"][languages.languageCodes[languageIndex]];
		}
	}
}
