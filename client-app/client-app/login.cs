using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Drawing;
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
		const int USER_LOGGED_IN_ON_OTHER_DEVICE = 3;

		const int ERROR_OCCURED = -1;

		public login()
		{
			InitializeComponent();

			txt_userID.TextChanged += (sender, e) => lbl_information.ResetText();
			txt_password.TextChanged += (sender, e) => lbl_information.ResetText();
			txt_passwordconfirm.TextChanged += (sender, e) => lbl_information.ResetText();

			btn_language.Text = languages.supportedLanguages[languageIndex];
			hub_connection.injectForm(this, null);
			initialiseConnection();
		}
		private async void initialiseConnection()
		{
			connection = hub_connection.configConnection($"{main.address}/accounts");
			connection = hub_connection.addLoginHandles(connection);
			connection = await hub_connection.startConnection(connection);

			connection.Closed += connectionClosed;

			this.lbl_connection.Text = "Connected";
			this.pic_connecting.Stop();
		}

		private async Task connectionClosed(Exception arg)
		{
			this.Invoke(new Action(() =>
			{
				this.lbl_connection.Text = "Reconnecting";
				this.pic_connecting.Start();
			}));
			await hub_connection.startConnection(connection);
			this.Invoke(new Action(() =>
			{
				this.lbl_connection.Text = "Connected";
				this.pic_connecting.Stop();
			}));
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
					Show();
					//Close();
					break;
				case USER_DOES_NOT_EXIST:
					lbl_information.Text = "Account does not exist";
					break;
				case ERROR_OCCURED:
					lbl_information.Text = "An error occurred. Please wait and try again";
					break;
				case USER_LOGGED_IN_ON_OTHER_DEVICE:
					lbl_information.Text = "User is currently logged in on another device";
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
					Show();
					//Close();
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

            // System.InvalidOperationException

			if (connection.State == HubConnectionState.Connected)
			{
                await connection.InvokeAsync("loginRequest", userID, password);
            }
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

			if (connection.State == HubConnectionState.Connected)
			{
				await connection.InvokeAsync("accountRequest", userID, password, localisation);
			}
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
