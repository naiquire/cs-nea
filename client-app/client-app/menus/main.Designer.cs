using client_app.menus;
using client_app.Properties;
using Guna.UI2.WinForms;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace client_app
{
	partial class Main
	{
		private System.ComponentModel.IContainer components = null;

		private void InitialiseComponent()
		{
			UXelements.ResetLayout(this);

			this.panel_friendList = new Panel();
			this.seperator = new PictureBox();
			this.lbl_friendsLabel = new Guna2HtmlLabel();
			this.lbl_menu = new Guna2HtmlLabel();
			this.txt_userSearch = new Guna2TextBox();
			this.btn_userSearch = new Guna2CircleButton();

			this.panel_topBorder.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.seperator)).BeginInit();
			this.panel_main.SuspendLayout();
			this.panel_left.SuspendLayout();

			this.SuspendLayout();
			// 
			// panel_friendList
			// 
			this.panel_friendList.AutoScroll = true;
			this.panel_friendList.BackColor = System.Drawing.Color.FromArgb(46, 46, 46);
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
			this.lbl_friendsLabel.AutoSize = false;
			this.lbl_friendsLabel.BackColor = Color.Transparent;
			this.lbl_friendsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lbl_friendsLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_friendsLabel.ForeColor = System.Drawing.Color.FromArgb(247, 113, 163);
			this.lbl_friendsLabel.Location = new System.Drawing.Point(0, 20);
			this.lbl_friendsLabel.Name = "txt_friendsLabel";
			this.lbl_friendsLabel.Size = new System.Drawing.Size(300, 40);
			this.lbl_friendsLabel.TabIndex = 0;
			this.lbl_friendsLabel.Text = Languages.localisation["Friends"][userData.localisation];
			this.lbl_friendsLabel.TextAlignment = ContentAlignment.MiddleCenter;
			//
			// txt_userSearch
			//
			this.txt_userSearch.BorderRadius = 8;
			this.txt_userSearch.BorderThickness = 0;
			this.txt_userSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txt_userSearch.FillColor = System.Drawing.Color.FromArgb(208, 208, 208);
			this.txt_userSearch.ForeColor = System.Drawing.Color.FromArgb(26, 26, 26);
			this.txt_userSearch.Location = new Point(20, 500);
			this.txt_userSearch.Name = "txt_userSearch";
			this.txt_userSearch.PlaceholderForeColor = System.Drawing.Color.Gray;
			this.txt_userSearch.PlaceholderText = "Search players";
			this.txt_userSearch.Size = new Size(200, 50);
			//
			// btn_userSearch
			//
			this.btn_userSearch.BorderThickness = 0;
			this.btn_userSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.btn_userSearch.FillColor = System.Drawing.Color.FromArgb(208, 208, 208);
			this.btn_userSearch.ForeColor = System.Drawing.Color.FromArgb(26, 26, 26);
			this.btn_userSearch.Name = "btn_userSearch";
			this.btn_userSearch.Image = global::client_app.Properties.Resources.search;
			this.btn_userSearch.Location = new Point(230, 500);
			this.btn_userSearch.Size = new Size(50, 50);
			this.btn_userSearch.Click += btn_userSearch_Click;
			//
			// main
			//
			this.panel_left.Controls.Add(panel_friendList);
			this.panel_left.Controls.Add(seperator);
			this.panel_left.Controls.Add(lbl_friendsLabel);
			this.panel_left.Controls.Add(txt_userSearch);
			this.panel_left.Controls.Add(btn_userSearch);
			this.panel_topLeft.Controls.Add(lbl_menu);

			((System.ComponentModel.ISupportInitialize)(this.seperator)).EndInit();

			ConfigFriendsPanel();
			ConfigGamePanels();
			UpdatePageText("Home");

			this.panel_main.ResumeLayout(false);
			this.panel_left.ResumeLayout(false);
			this.ResumeLayout(false);

			UXelements.ConfigUserDataPanel(this, userData);
		}

		private void ConfigGamePanels()
		{
			const int gamePanelX = 60;
			const int gamePanelY = 100;
			const int gamePanelSizeX = 1000;
			const int gamePanelSizeY = 90;
			const int gamePanelYSpacing = 50;

			const int padding = 10;
			const int lbl_nameSizeX = 300;
			const int btn_queueSizeX = 160;
			const int paddedSizeY = gamePanelSizeY - 2 * padding;

			Guna2GradientPanel createGamePanel((int X, int Y) location, (int X, int Y) size)
			{
				return new Guna2GradientPanel()
				{
					BackColor = System.Drawing.Color.Transparent,
					BorderRadius = 20,
					FillColor = System.Drawing.Color.FromArgb(208, 208, 208),
					FillColor2 = System.Drawing.Color.FromArgb(208, 208, 208),
					Location = new Point(location.X, location.Y),
					Size = new Size(size.X, size.Y),
					TabIndex = 0,
					UseTransparentBackground = true,
				};
			}
			Guna2HtmlLabel createGameLabel(string text, (int X, int Y) location, (int X, int Y) size)
			{
				return new Guna2HtmlLabel()
				{
					AutoSize = false,
					BackColor = System.Drawing.Color.Transparent,
					Cursor = System.Windows.Forms.Cursors.Arrow,
					Text = text,
					Font = new Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
					ForeColor = System.Drawing.Color.White,
					Location = new Point(location.X, location.Y),
					Margin = new System.Windows.Forms.Padding(6),
					Size = new Size(size.X, size.Y),
					TabStop = false,
					TextAlignment = ContentAlignment.MiddleCenter,
				};
			}
			Guna2GradientButton createQueueButton((int X, int Y) location, (int X, int Y) size)
			{
				return new Guna2GradientButton()
				{
					AutoRoundedCorners = true,
					FillColor = System.Drawing.Color.FromArgb(247, 113, 163),
					FillColor2 = System.Drawing.Color.FromArgb(197, 113, 247),
					Font = new Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
					ForeColor = System.Drawing.Color.White,
					Location = new Point(location.X, location.Y),
					Size = new Size(size.X, size.Y),
					Text = Languages.localisation["Queue"][Main.userData.localisation],
				};
			}
			Guna2TextBox createGameInfoTextbox(string text, (int X, int Y) location, (int X, int Y) size)
			{
				var t = new Guna2TextBox()
				{
					BorderRadius = 10,
					Cursor = Cursors.Arrow,
					DefaultText = text,
					FillColor = Color.FromArgb(156, 156, 156),
					Font = new Font("Bahnschrift", 9.75F),
					ForeColor = Color.White,
					Location = new Point(location.X, location.Y),
					ReadOnly = true,
					Size = new Size(size.X, size.Y),
					TabStop = false,
					TextAlign = HorizontalAlignment.Center,
				};

				t.HoverState.BorderColor = Color.Transparent;

				return t;
			}

			this.panel_accuracy = createGamePanel((gamePanelX, gamePanelY), (gamePanelSizeX, gamePanelSizeY));
			this.panel_1v1 = createGamePanel((gamePanelX, gamePanelY + gamePanelSizeY + gamePanelYSpacing), (gamePanelSizeX, gamePanelSizeY));
			this.panel_knockout = createGamePanel((gamePanelX, gamePanelY + 2 * (gamePanelSizeY + gamePanelYSpacing)), (gamePanelSizeX, gamePanelSizeY));

			this.lbl_accuracy = createGameLabel(Languages.localisation["Accuracy"][Main.userData.localisation], (2 * padding, padding), (lbl_nameSizeX, paddedSizeY));
			this.lbl_1v1 = createGameLabel(Languages.localisation["Versus"][Main.userData.localisation], (2 * padding, padding), (lbl_nameSizeX, paddedSizeY));
			this.lbl_knockout = createGameLabel(Languages.localisation["Knockout"][Main.userData.localisation], (2 * padding, padding), (lbl_nameSizeX, paddedSizeY));

			this.btn_queueAccuracy = createQueueButton((gamePanelSizeX - 2 * padding - btn_queueSizeX, 2 * padding), (btn_queueSizeX, gamePanelSizeY - 4 * padding));
			this.btn_queue1v1 = createQueueButton((gamePanelSizeX - 2 * padding - btn_queueSizeX, 2 * padding), (btn_queueSizeX, gamePanelSizeY - 4 * padding));
			this.btn_queueKnockout = createQueueButton((gamePanelSizeX - 2 * padding - btn_queueSizeX, 2 * padding), (btn_queueSizeX, gamePanelSizeY - 4 * padding));

			string accuracyInfo =    "PLAYERS  :  1          |         ROUNDS : 10         |         UNRANKED";
			string versusInfo =      "PLAYERS  :  2          |         ROUNDS : 10         |           RANKED";
			string eliminationInfo = "PLAYERS  :  12         |         ELIMINATION         |         UNRANKED";
			this.txt_accuracy = createGameInfoTextbox(accuracyInfo, (4 * padding + lbl_nameSizeX, padding), (gamePanelSizeX - (4 * padding + lbl_nameSizeX) - (4 * padding + btn_queueSizeX), paddedSizeY));
			this.txt_1v1 = createGameInfoTextbox(versusInfo, (4 * padding + lbl_nameSizeX, padding), (gamePanelSizeX - (4 * padding + lbl_nameSizeX) - (4 * padding + btn_queueSizeX), paddedSizeY));
			this.txt_knockout = createGameInfoTextbox(eliminationInfo, (4 * padding + lbl_nameSizeX, padding), (gamePanelSizeX - (4 * padding + lbl_nameSizeX) - (4 * padding + btn_queueSizeX), paddedSizeY));

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
			this.btn_queue1v1.Click += btn_queueVersus_Click;
			this.btn_queueKnockout.Click += btn_queueElimination_Click;

			this.panel_main.Controls.Add(this.panel_knockout);
			this.panel_main.Controls.Add(this.panel_1v1);
			this.panel_main.Controls.Add(this.panel_accuracy);
		}
		private void ConfigFriendsPanel()
		{
			panel_friendList.Controls.Clear();

			List<friendData> onlineList = new List<friendData>();
			List<friendData> offlineList = new List<friendData>();

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

			const int buttonX = 260;
			const int buttonY = 30;
			const int padding = 5;
			int y_offset = 10;

			Guna2HtmlLabel createLabel(string text, (int X, int Y) location, (int X, int Y) size, ContentAlignment align)
			{
				return new Guna2HtmlLabel()
				{
					AutoSize = false,
					BackColor = System.Drawing.Color.Transparent,
					Cursor = System.Windows.Forms.Cursors.Arrow,
					Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
					ForeColor = System.Drawing.Color.White,
					Location = new Point(location.X, location.Y),
					Margin = new System.Windows.Forms.Padding(6),
					Size = new Size(size.X, size.Y),
					TabStop = false,
					Text = text,
					TextAlignment = align,
				};
			}
			Guna2Button createUserButton(string text, (int X, int Y) location, (int X, int Y) size)
			{
				var b = new Guna2Button()
				{
					BackColor = Color.Transparent,
					Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0),
					FillColor = Color.Transparent,
					Location = new System.Drawing.Point(location.X, location.Y),
					Name = text,
					PressedColor = Color.Transparent,
					Size = new System.Drawing.Size(size.X, size.Y),
					TabIndex = 0,
					Text = text,
					TextAlign = HorizontalAlignment.Left,
				};

				b.HoverState.BorderColor = Color.Transparent;

				b.Click += async (sender, e) =>
				{
					string userID = ((Guna2Button)sender).Name;
					await RequestProfile(userID);
				};

				return b;
			}

			Guna2HtmlLabel online = createLabel(Languages.localisation["ONLINE"][userData.localisation], (10, y_offset), (200, buttonY), ContentAlignment.MiddleLeft);
			Guna2HtmlLabel onlineCount = createLabel(onlineList.Count.ToString(), (230, 10), (25, 20), ContentAlignment.MiddleRight);

			panel_friendList.Controls.Add(online);
			panel_friendList.Controls.Add(onlineCount);

			y_offset += 30;

			for (int i = 0; i < onlineList.Count; i++, y_offset += buttonY + padding)
			{
				Guna2Button user = createUserButton(onlineList[i].userID, (0, y_offset), (buttonX, buttonY));
				panel_friendList.Controls.Add(user);
			}

			y_offset += 30;

			Guna2HtmlLabel offline = createLabel(Languages.localisation["OFFLINE"][userData.localisation], (10, y_offset), (200, buttonY), ContentAlignment.MiddleLeft);
			Guna2HtmlLabel offlineCount = createLabel(offlineList.Count.ToString(), (230, y_offset), (25, 20), ContentAlignment.MiddleRight);

			panel_friendList.Controls.Add(offline);
			panel_friendList.Controls.Add(offlineCount);

			y_offset += 30;

			for (int i = 0; i < offlineList.Count; i++, y_offset += buttonY + padding)
			{
				Guna2Button user = createUserButton(offlineList[i].userID, (0, y_offset), (buttonX, buttonY));
				panel_friendList.Controls.Add(user);
			}
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
		private Guna2HtmlLabel lbl_friendsLabel;
		private Guna2HtmlLabel lbl_menu;
		private Guna2TextBox txt_userSearch;
		private Guna2CircleButton btn_userSearch;

		private Guna2GradientPanel panel_accuracy;
		private Guna2HtmlLabel lbl_accuracy;
		private Guna2TextBox txt_accuracy;
		private Guna2GradientButton btn_queueAccuracy;
		private Guna2GradientPanel panel_1v1;
		private Guna2GradientButton btn_queue1v1;
		private Guna2TextBox txt_1v1;
		private Guna2HtmlLabel lbl_1v1;
		private Guna2GradientPanel panel_knockout;
		private Guna2GradientButton btn_queueKnockout;
		private Guna2TextBox txt_knockout;
		private Guna2HtmlLabel lbl_knockout;

	}
}
