using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app
{
    public partial class login : Form
    {
        private HubConnection connection;
        private int languageIndex = 0;

		const int VALID = 1;
		const int INCORRECT_PASSWORD = 0;
		const int USERID_TAKEN = 0;
		const int USER_DOES_NOT_EXIST = 2;
		const int ERROR_OCCURED = -1;

		public login()
        {
            InitializeComponent();
			btn_language.Text = languages.supportedLanguages[languageIndex];
			hub_connection.injectForm(this, null);
			initialiseConnection();
        }
        private async void initialiseConnection()
        {
            connection = hub_connection.configConnection($"{main.address}/accounts");
            connection = hub_connection.addLoginHandles(connection);
            connection = await hub_connection.startConnection(connection);

            this.lbl_connection.Text = "Connected";
            pic_connecting.Stop();
			
        }

		public void handleLoginSuccess(int success, string userID)
        {
			switch (success)
			{
				case INCORRECT_PASSWORD:
                    lbl_information.Text = "Incorrect Password";
					break;
				case VALID:
					Hide();
					new main(userID).ShowDialog();
					Close();
					break;
				case USER_DOES_NOT_EXIST:
					lbl_information.Text = "Account does not exist";
					break;
				case ERROR_OCCURED:
					lbl_information.Text = "An error occurred. Please wait and try again";
					break;
				default:
					throw new Exception($"Unrecognised login success code < {success} >");
			}
		}
		public void handleAccountCreationSuccess(int success, string userID)
        {
			switch (success)
			{
				case VALID:
					Hide();
					new main(userID).ShowDialog();
					Close();
					break;
				case USERID_TAKEN:
					lbl_information.Text = "Username is not available";
					break;
				case ERROR_OCCURED:
					lbl_information.Text = "An error occurred. Please wait and try again";
					break;
				default:
					throw new Exception($"Unrecognised account success code < {success} >");
			}
		}

		#region Button logic
		private async void btn_login_Click(object sender, EventArgs e)
        {
            string userID = txt_userID.Text.Trim();
            string password = txt_password.Text;
			this.lbl_information.ResetText();
			await connection.InvokeAsync("loginRequest", userID, password);
        }
        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            this.Controls.Remove(btn_login);
            this.Controls.Remove(btn_createAccount);

            this.txt_password.Location = new Point(560, 260);
			this.lbl_information.Location = new Point(lbl_information.Location.X, txt_passwordconfirm.Location.Y + txt_passwordconfirm.Size.Height);

            this.Controls.Add(txt_passwordconfirm);
            this.Controls.Add(btn_requestAccount);
        }
        private async void btn_requestAccount_Click(object sender, EventArgs e)
        {
			lbl_information.ResetText();
			if (txt_password.Text != txt_passwordconfirm.Text)
			{
				lbl_information.Text = "Passwords do not match";
				return;
			}

            string userID = txt_userID.Text.Trim();
            string password = txt_password.Text;
            string localisation = languages.languageCodes[languageIndex];
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
		#endregion

	}
}
