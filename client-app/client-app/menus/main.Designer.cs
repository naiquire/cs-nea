using client_app.menus;
using client_app.Properties;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace client_app
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		/// <summary>
		/// hex codes for graphics
		/// 
		/// f771a3
		/// c571f7
		/// 
		/// </summary>

		const int gamePanelX = 20;
		const int gamePanelY = 100;
		const int gamePanelSizeX = 800;
		const int gamePanelSizeY = 90;
		const int gamePanelSpacing = 50;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            abstractMenu.resetLayout(this);

			this.panel_friendList = new System.Windows.Forms.Panel();
            this.seperator = new System.Windows.Forms.PictureBox();
            this.lbl_friendsLabel = new System.Windows.Forms.Label();

			this.panel_topBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seperator)).BeginInit();
            this.panel_main.SuspendLayout();

			this.SuspendLayout();
            // 
            // panel_friendList
            // 
            this.panel_friendList.AutoScroll = true;
            this.panel_friendList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panel_friendList.Location = new System.Drawing.Point(20, 90);
            this.panel_friendList.Name = "panel_friendList";
            this.panel_friendList.Size = new System.Drawing.Size(260, 384);
            this.panel_friendList.TabIndex = 2;
            // 
            // seperator
            // 
            this.seperator.Image = global::client_app.Properties.Resources.seperator;
            this.seperator.InitialImage = null;
            this.seperator.Location = new System.Drawing.Point(50, 60);
            this.seperator.Name = "seperator";
            this.seperator.Size = new System.Drawing.Size(200, 5);
            this.seperator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.seperator.TabIndex = 1;
            this.seperator.TabStop = false;
            // 
            // txt_friendsLabel
            // 
            this.lbl_friendsLabel.BackColor = this.panel_left.BackColor;
            this.lbl_friendsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_friendsLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_friendsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
            this.lbl_friendsLabel.Location = new System.Drawing.Point(0, 20);
            this.lbl_friendsLabel.Name = "txt_friendsLabel";
            this.lbl_friendsLabel.Size = new System.Drawing.Size(300, 33);
            this.lbl_friendsLabel.TabIndex = 0;
            this.lbl_friendsLabel.Text = localisation["Friends"][userData.localisation];
            this.lbl_friendsLabel.TextAlign = (ContentAlignment)System.Windows.Forms.HorizontalAlignment.Center;
			//
			// main
			//
			this.panel_left.Controls.Add(panel_friendList);
            this.panel_left.Controls.Add(seperator);
            this.panel_left.Controls.Add(lbl_friendsLabel);


			((System.ComponentModel.ISupportInitialize)(this.seperator)).EndInit();
            this.panel_main.ResumeLayout(false);
			this.ResumeLayout(false);


            configFriendsPanel();
			configGamePanels();

			abstractMenu.configUserDataPanel(this, userData);
        }

		/// <summary>
		/// Handles the click event for the "Home" button, resetting the application state to the home screen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void btn_home_Click(object sender, EventArgs e)
		{
			/// <no server functionality>
			//if (game.gameID != null)
			//{
			//    connection.InvokeAsync("leaveGame", game.gameID, userData.userID);
			//}

			// dispose all other classes
			menu.profile = null;
            menu.game = null;

			InitializeComponent();
		}

		/// <summary>
		/// Configures and initializes the game panels on the home screen for different game modes.
		/// </summary>
        public void configGamePanels()
        {
			const int padding = 10;
			const int lblSizeX = 180;
			const int btnSizeX = 160;
			const int txtSizeY = gamePanelSizeY - 2 * padding;

			this.panel_accuracy = new Guna.UI2.WinForms.Guna2GradientPanel()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Location = new System.Drawing.Point(gamePanelX, gamePanelY),
				Name = "panel_accuracy",
				Size = new System.Drawing.Size(gamePanelSizeX, gamePanelSizeY),
				TabIndex = 0,
				UseTransparentBackground = true,
			};
			this.lbl_accuracy = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "Accuracy",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(2 * padding, padding),
				Margin = new System.Windows.Forms.Padding(6),
				Name = "lbl_accuracy",
				PlaceholderForeColor = System.Drawing.Color.Transparent,
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(lblSizeX, gamePanelSizeY - 2 * padding),
				TabStop = false,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
				TextOffset = new System.Drawing.Point(0, -2),
			};
			this.btn_queueAccuracy = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(gamePanelSizeX - 2 * padding - btnSizeX, 2 * padding),
				Name = "btn_queueAccuracy",
				Size = new System.Drawing.Size(btnSizeX, gamePanelSizeY - 4 * padding),
				TabIndex = 3,
				Text = "Queue",
			};
			this.txt_accuracy = new Guna.UI2.WinForms.Guna2TextBox() 
			{
				BorderRadius = 10,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "PLAYERS  :  1         |         ROUNDS : 10         |         UNRANKED",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				Font = new System.Drawing.Font("Bahnschrift", 9.75F),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(lbl_accuracy.Right + 2 * padding, padding),
				Multiline = true,
				Name = "txt_accuracy",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(gamePanelSizeX - (lbl_accuracy.Right + 2 * padding) - (4 * padding + btnSizeX), txtSizeY),
				TabIndex = 15,
				TabStop = false,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
				TextOffset = new System.Drawing.Point(0, 19),
			};

			this.panel_1v1 = new Guna.UI2.WinForms.Guna2GradientPanel()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Location = new System.Drawing.Point(gamePanelX, gamePanelY + gamePanelSizeY + gamePanelSpacing),
				Name = "panel_1v1",
				Size = new System.Drawing.Size(gamePanelSizeX, gamePanelSizeY),
				TabIndex = 16,
				UseTransparentBackground = true,
			};
			this.lbl_1v1 = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41))))),
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "Versus",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(2 * padding, padding),
				Margin = new System.Windows.Forms.Padding(6),
				Name = "lbl_1v1",
				PlaceholderForeColor = System.Drawing.Color.Transparent,
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(lblSizeX, gamePanelSizeY - 2 * padding),
				TabIndex = 13,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
				TextOffset = new System.Drawing.Point(0, -2),
			};
			this.btn_queue1v1 = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(gamePanelSizeX - 2 * padding - btnSizeX, 2 * padding),
				Name = "btn_queue1v1",
				Size = new System.Drawing.Size(btnSizeX, gamePanelSizeY - 4 * padding),
				TabIndex = 3,
				Text = "Queue",
			};
			this.txt_1v1 = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderRadius = 10,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "PLAYERS  :  2         |         ROUNDS : 10         |         RANKED",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				Font = new System.Drawing.Font("Bahnschrift", 9.75F),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(lbl_1v1.Right + 2 * padding, padding),
				Multiline = true,
				Name = "txt_1v1",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(380, txtSizeY),
				TabIndex = 15,
				TabStop = false,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
				TextOffset = new System.Drawing.Point(0, 19),
			};

			this.panel_knockout = new Guna.UI2.WinForms.Guna2GradientPanel()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Location = new System.Drawing.Point(gamePanelX, gamePanelY + 2 * (gamePanelSizeY + gamePanelSpacing)),
				Name = "panel_knockout",
				Size = new System.Drawing.Size(gamePanelSizeX, gamePanelSizeY),
				TabIndex = 17,
				UseTransparentBackground = true,
			};
			this.lbl_knockout = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41))))),
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "Knockout",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(2 * padding, padding),
				Margin = new System.Windows.Forms.Padding(6),
				Name = "lbl_knockout",
				PlaceholderForeColor = System.Drawing.Color.Transparent,
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(lblSizeX, gamePanelSizeY - 2 * padding),
				TabIndex = 13,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
				TextOffset = new System.Drawing.Point(0, -2),
			};
			this.btn_queueKnockout = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(gamePanelSizeX - 2 * padding - btnSizeX, 2 * padding),
				Name = "btn_queueKnockout",
				Size = new System.Drawing.Size(btnSizeX, gamePanelSizeY - 4 * padding),
				TabIndex = 3,
				Text = "Queue",
			};
			this.txt_knockout = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderRadius = 10,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "PLAYERS  :  12         |         ELIMINATION         |         UNRANKED",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				Font = new System.Drawing.Font("Bahnschrift", 9.75F),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(lbl_knockout.Right + 2 * padding, padding),
				Multiline = true,
				Name = "txt_knockout",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(380, txtSizeY),
				TabIndex = 15,
				TabStop = false,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
				TextOffset = new System.Drawing.Point(0, 19),
			};

			this.panel_accuracy.Controls.Add(this.lbl_accuracy);
			this.panel_accuracy.Controls.Add(this.btn_queueAccuracy);
			this.panel_accuracy.Controls.Add(this.txt_accuracy);

			this.panel_1v1.Controls.Add(this.lbl_1v1);
			this.panel_1v1.Controls.Add(this.btn_queue1v1);
			this.panel_1v1.Controls.Add(this.txt_1v1);

			this.panel_knockout.Controls.Add(this.lbl_knockout);
			this.panel_knockout.Controls.Add(this.btn_queueKnockout);
			this.panel_knockout.Controls.Add(this.txt_knockout);

			this.btn_queueAccuracy.Click += btn_queueAccuracy_Click;
			this.btn_queue1v1.Click += btn_queue1v1_Click;
			this.btn_queueKnockout.Click += btn_queueKnockout_Click;

			this.panel_main.Controls.Add(this.panel_knockout);
			this.panel_main.Controls.Add(this.panel_1v1);
			this.panel_main.Controls.Add(this.panel_accuracy);
		}

		/// <summary>
		/// Configures the friends panel on the home screen.
		/// </summary>
		private void configFriendsPanel()
        {
            // seperate friends into online/offline
            List<friendData> onlineList = new List<friendData>();
            List<friendData> offlineList = new List<friendData>();

            const int buttonX = 200;
            const int buttonY = 30;
            const int padding = 5;

            foreach (var friend in userData.friends)
            {
                if (friend.online)
                {
                    onlineList.Add(friend);
                }
                else
                {
                    offlineList.Add(friend);
                }
            }

			int y_offset = 10;
			// online text
			Label online = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(10, y_offset),
                Name = "txt_online",
                Size = new System.Drawing.Size(200, buttonY),
                TabIndex = 0,
                Text = localisation["ONLINE"][userData.localisation],
            };
            panel_friendList.Controls.Add(online);

            // online count
            Label onlineCount = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(230, 10),
                Name = "txt_onlineCount",
                Size = new System.Drawing.Size(30, 16),
                TabIndex = 0,
                Text = onlineList.Count.ToString(),
                TextAlign = ContentAlignment.MiddleRight,
            };
			panel_friendList.Controls.Add(onlineCount);
			y_offset += 30;

			// add online friends
			for (int i = 0; i < onlineList.Count; i++, y_offset += buttonY + padding)
            {
                Button user = new Button()
                {
                    BackColor = this.panel_friendList.BackColor,
                    Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(10, y_offset),
                    Name = onlineList[i].userID,
                    Size = new System.Drawing.Size(buttonX, buttonY),
                    TabIndex = 0,
                    Text = onlineList[i].userID,
					FlatStyle = FlatStyle.Flat,
				};
                user.Click += async (sender, e) =>
                {
                    string userID = ((Button)sender).Name;
                    await requestProfile(userID);
                };
                panel_friendList.Controls.Add(user);
            }

            // offline text
            y_offset += 30;
            Label offline = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(10, y_offset),
                Name = "txt_appName",
                Size = new System.Drawing.Size(200, buttonY),
                TabIndex = 0,
                Text = localisation["OFFLINE"][userData.localisation],
            };
            panel_friendList.Controls.Add(offline);

            // offline count
            Label offlineCount = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(230, y_offset),
                Name = "txt_offlineCount",
                Size = new System.Drawing.Size(30, 16),
                TabIndex = 0,
                Text = offlineList.Count.ToString(),
				TextAlign = ContentAlignment.MiddleRight,
			};
			panel_friendList.Controls.Add(offlineCount);
			y_offset += 30;

			// add offline friends
			for (int i = 0; i < offlineList.Count; i++, y_offset += buttonY + padding)
            {
                Button user = new Button()
                {
                    BackColor = this.panel_friendList.BackColor,
                    Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(10, y_offset),
                    Name = offlineList[i].userID,
                    Size = new System.Drawing.Size(buttonX, buttonY),
                    TabIndex = 0,
                    Text = offlineList[i].userID,
                    FlatStyle = FlatStyle.Flat,
                };
                user.Click += async (sender, e) =>
                {
                    string userID = ((Button)sender).Name;
                    await requestProfile(userID);
                };
                panel_friendList.Controls.Add(user);
            }
        }
		public async void btn_close_Click(object sender, EventArgs e)
		{
			Hide();
			await main.connection.InvokeAsync("clientDisconnected", main.userData.userID);
			Close();
		}

		public Panel panel_topBorder;
		public Button btn_close;
		public Label lbl_appName;
		public Panel panel_left;
		public Panel panel_topLeft;
		public Panel panel_main;
		public Panel panel_right;
		public Button btn_home;

		private Panel panel_friendList;
        private PictureBox seperator;
        private Label lbl_friendsLabel;

		private Guna.UI2.WinForms.Guna2GradientPanel panel_accuracy;
		private Guna.UI2.WinForms.Guna2TextBox lbl_accuracy;
		private Guna.UI2.WinForms.Guna2TextBox txt_accuracy;
		private Guna.UI2.WinForms.Guna2GradientButton btn_queueAccuracy;
		private Guna.UI2.WinForms.Guna2GradientPanel panel_1v1;
		private Guna.UI2.WinForms.Guna2GradientButton btn_queue1v1;
		private Guna.UI2.WinForms.Guna2TextBox txt_1v1;
		private Guna.UI2.WinForms.Guna2TextBox lbl_1v1;
		private Guna.UI2.WinForms.Guna2GradientPanel panel_knockout;
		private Guna.UI2.WinForms.Guna2GradientButton btn_queueKnockout;
		private Guna.UI2.WinForms.Guna2TextBox txt_knockout;
		private Guna.UI2.WinForms.Guna2TextBox lbl_knockout;

		#endregion

	}
}

