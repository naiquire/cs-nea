using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using client_app.Properties;

namespace client_app
{
	public partial class login : Form
	{
		private HubConnection connection;
		private int languageIndex = 0;

		public int getLanguageIndex() => languageIndex;

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
			connection = hub_connection.configConnection($"{Main.address}/accounts");
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
			connection = await hub_connection.startConnection(connection);
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
					Main main = new Main(userID, languages.languageCodes[languageIndex]);
					main.ShowDialog();
					main.Dispose();
					Show();
					break;
				case USER_DOES_NOT_EXIST:
					lbl_information.Text = languages.localisation["Account does not exist"][languages.languageCodes[languageIndex]];
					break;
				case ERROR_OCCURED:
					lbl_information.Text = languages.localisation["An error occurred. Please wait and try again"][languages.languageCodes[languageIndex]];
					break;
				case USER_LOGGED_IN_ON_OTHER_DEVICE:
					lbl_information.Text = languages.localisation["User is currently logged in on another device"][languages.languageCodes[languageIndex]];
					break;
				default:
					throw new Exception($"{languages.localisation["Unrecognised success code"][languages.languageCodes[languageIndex]]} < {success} >");
			}
		}
		public void handleAccountCreationSuccess(int success, string userID)
		{
			switch (success)
			{
				case VALID:
					this.Controls.Add(btn_login);
					this.Controls.Add(btn_createAccount);

					this.Controls.Remove(txt_passwordconfirm);
					this.Controls.Remove(btn_requestAccount);

					this.txt_password.Location = new Point(560, 250);
					this.lbl_information.Location = new Point(560, 290);

					this.txt_password.ResetText();
					this.txt_passwordconfirm.ResetText();

					Hide();
					Main main = new Main(userID, languages.languageCodes[languageIndex]);
					main.ShowDialog();
					main.Dispose();
					Show();
					break;
				case USERID_TAKEN:
					lbl_information.Text = languages.localisation["Username is not available"][languages.languageCodes[languageIndex]];
					break;
				case ERROR_OCCURED:
					lbl_information.Text = languages.localisation["An error occured. Please wait and try again"][languages.languageCodes[languageIndex]];
					break;
				default:
					throw new Exception($"{languages.localisation["Unrecognised success code"][languages.languageCodes[languageIndex]]} < {success} >");
			}
		}

		#region Button logic
		private async void btn_login_Click(object sender, EventArgs e)
		{
			string userID = txt_userID.Text.Trim();
			string password = txt_password.Text;
			txt_password.ResetText();
			this.lbl_information.ResetText();

			if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrEmpty(password))
			{
				return;
			}

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

			if (string.IsNullOrWhiteSpace(userID) || string.IsNullOrEmpty(password))
			{
				return;
			}

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
