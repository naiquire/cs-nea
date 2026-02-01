using client_app.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace client_app.menus
{
	partial class Profile
	{
		private System.ComponentModel.IContainer components = null;

		private void InitialiseComponent()
		{
			main.panel_main.Controls.Clear();

			this.lbl_username = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbl_rank = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbl_total = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbl_accuracy = new Guna.UI2.WinForms.Guna2TextBox();
			this.pic_seperator = new System.Windows.Forms.PictureBox();
			this.panel_stats = new Panel();
			this.btn_addFriends = new Guna.UI2.WinForms.Guna2GradientButton();
			this.btn_removeFriends = new Guna.UI2.WinForms.Guna2GradientButton();
			((System.ComponentModel.ISupportInitialize)(this.pic_seperator)).BeginInit();
			// 
			// lbl_username
			// 
			this.lbl_username.BorderRadius = 20;
			this.lbl_username.BorderThickness = 0;
			this.lbl_username.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_username.DefaultText = "username";
			this.lbl_username.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
			this.lbl_username.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
			this.lbl_username.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_username.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_username.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_username.Font = new System.Drawing.Font("Bahnschrift SemiBold", 31.75F, System.Drawing.FontStyle.Bold);
			this.lbl_username.ForeColor = System.Drawing.Color.FromArgb(26, 26, 26);
			this.lbl_username.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_username.IconLeft = global::client_app.Properties.Resources.account;
			this.lbl_username.IconLeftOffset = new System.Drawing.Point(10, 0);
			this.lbl_username.IconLeftSize = new System.Drawing.Size(45, 50);
			this.lbl_username.Location = new System.Drawing.Point(20, 20);
			this.lbl_username.Name = "lbl_username";
			this.lbl_username.PlaceholderText = "";
			this.lbl_username.ReadOnly = true;
			this.lbl_username.SelectedText = "";
			this.lbl_username.Size = new System.Drawing.Size(1080, 70);
			this.lbl_username.TabIndex = 0;
			this.lbl_username.TabStop = false;
			this.lbl_username.TextOffset = new System.Drawing.Point(20, -3);
			// 
			// lbl_rank
			// 
			this.lbl_rank.BorderRadius = 10;
			this.lbl_rank.BorderThickness = 0;
			this.lbl_rank.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_rank.DefaultText = "rank";
			this.lbl_rank.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
			this.lbl_rank.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
			this.lbl_rank.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_rank.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_rank.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_rank.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
			this.lbl_rank.ForeColor = System.Drawing.Color.FromArgb(52, 52, 52);
			this.lbl_rank.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_rank.IconLeft = global::client_app.Properties.Resources.rank;
			this.lbl_rank.IconLeftOffset = new System.Drawing.Point(10, 0);
			this.lbl_rank.Location = new System.Drawing.Point(20, 100);
			this.lbl_rank.Name = "lbl_rank";
			this.lbl_rank.PlaceholderText = "";
			this.lbl_rank.ReadOnly = true;
			this.lbl_rank.SelectedText = "";
			this.lbl_rank.Size = new System.Drawing.Size(350, 30);
			this.lbl_rank.TabIndex = 1;
			this.lbl_rank.TabStop = false;
			this.lbl_rank.TextOffset = new System.Drawing.Point(10, -1);
			// 
			// lbl_total
			// 
			this.lbl_total.BorderRadius = 10;
			this.lbl_total.BorderThickness = 0;
			this.lbl_total.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_total.DefaultText = "total";
			this.lbl_total.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
			this.lbl_total.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
			this.lbl_total.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_total.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_total.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_total.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
			this.lbl_total.ForeColor = System.Drawing.Color.FromArgb(52, 52, 52);
			this.lbl_total.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_total.IconLeft = global::client_app.Properties.Resources.total;
			this.lbl_total.IconLeftOffset = new System.Drawing.Point(10, 0);
			this.lbl_total.Location = new System.Drawing.Point(385, 100);
			this.lbl_total.Name = "lbl_total";
			this.lbl_total.PlaceholderText = "";
			this.lbl_total.ReadOnly = true;
			this.lbl_total.SelectedText = "";
			this.lbl_total.Size = new System.Drawing.Size(350, 30);
			this.lbl_total.TabIndex = 2;
			this.lbl_total.TabStop = false;
			this.lbl_total.TextOffset = new System.Drawing.Point(10, -1);
			// 
			// lbl_accuracy
			// 
			this.lbl_accuracy.BorderRadius = 10;
			this.lbl_accuracy.BorderThickness = 0;
			this.lbl_accuracy.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_accuracy.DefaultText = "accuracy";
			this.lbl_accuracy.DisabledState.BorderColor = System.Drawing.Color.FromArgb(208, 208, 208);
			this.lbl_accuracy.DisabledState.FillColor = System.Drawing.Color.FromArgb(226, 226, 226);
			this.lbl_accuracy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_accuracy.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(138, 138, 138);
			this.lbl_accuracy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_accuracy.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
			this.lbl_accuracy.ForeColor = System.Drawing.Color.FromArgb(52, 52, 52);
			this.lbl_accuracy.HoverState.BorderColor = System.Drawing.Color.FromArgb(94, 148, 255);
			this.lbl_accuracy.IconLeft = global::client_app.Properties.Resources.accuracy;
			this.lbl_accuracy.IconLeftOffset = new System.Drawing.Point(10, 0);
			this.lbl_accuracy.Location = new System.Drawing.Point(750, 100);
			this.lbl_accuracy.Name = "lbl_accuracy";
			this.lbl_accuracy.PlaceholderText = "";
			this.lbl_accuracy.ReadOnly = true;
			this.lbl_accuracy.SelectedText = "";
			this.lbl_accuracy.Size = new System.Drawing.Size(350, 30);
			this.lbl_accuracy.TabIndex = 3;
			this.lbl_accuracy.TabStop = false;
			this.lbl_accuracy.TextOffset = new System.Drawing.Point(10, -1);
			// 
			// pic_seperator
			// 
			this.pic_seperator.Image = global::client_app.Properties.Resources.seperator;
			this.pic_seperator.Location = new System.Drawing.Point(305, 150);
			this.pic_seperator.Name = "pic_seperator";
			this.pic_seperator.Size = new System.Drawing.Size(510, 5);
			this.pic_seperator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pic_seperator.TabStop = false;
			// 
			// panel_stats
			// 
			this.panel_stats.AutoScroll = true;
			this.panel_stats.AutoSize = false;
			this.panel_stats.BorderStyle = BorderStyle.None;
			this.panel_stats.BackColor = System.Drawing.Color.White;
			this.panel_stats.Location = new System.Drawing.Point(40, 175);
			this.panel_stats.Name = "panel_stats";
			this.panel_stats.Size = new System.Drawing.Size(1040, UXelements.clientY - 30 - 175 - 40 - 70);
			this.panel_stats.TabStop = false;
			//
			// btn_addFriends
			//
			this.btn_addFriends.AutoRoundedCorners = true;
			this.btn_addFriends.BorderRadius = 14;
			this.btn_addFriends.FillColor = System.Drawing.Color.FromArgb(247, 113, 163);
			this.btn_addFriends.FillColor2 = System.Drawing.Color.FromArgb(197, 113, 247);
			this.btn_addFriends.ForeColor = System.Drawing.Color.White;
			this.btn_addFriends.Location = new Point(40, 900);
			this.btn_addFriends.Name = "btn_login";
			this.btn_addFriends.Size = new Size(200, 40);
			this.btn_addFriends.Text = Languages.localisation["Add Friend"][Main.userData.localisation];
			this.btn_addFriends.Click += new System.EventHandler(this.btn_addFriends_Click);
			//
			// btn_removeFriends
			//
			this.btn_removeFriends.AutoRoundedCorners = true;
			this.btn_removeFriends.BorderRadius = 14;
			this.btn_removeFriends.FillColor = System.Drawing.Color.FromArgb(247, 113, 163);
			this.btn_removeFriends.FillColor2 = System.Drawing.Color.FromArgb(197, 113, 247);
			this.btn_removeFriends.ForeColor = System.Drawing.Color.White;
			this.btn_removeFriends.Location = new Point(240, 900);
			this.btn_removeFriends.Name = "btn_login";
			this.btn_removeFriends.Size = new Size(200, 40);
			this.btn_removeFriends.Text = Languages.localisation["Remove Friend"][Main.userData.localisation];
			this.btn_removeFriends.Click += new System.EventHandler(this.btn_removeFriends_Click);

			(string rank, string total, string accuracy) = Main.CalculateStatsOverview(user);

			this.lbl_username.Text = user.userID;
			this.lbl_rank.Text = rank;
			this.lbl_total.Text = total;
			this.lbl_accuracy.Text = accuracy + "%";

			ConfigStats();
			panel_stats.ResumeLayout(false);

			main.panel_main.Controls.Add(this.panel_stats);
			main.panel_main.Controls.Add(this.pic_seperator);
			main.panel_main.Controls.Add(this.lbl_accuracy);
			main.panel_main.Controls.Add(this.lbl_total);
			main.panel_main.Controls.Add(this.lbl_rank);
			main.panel_main.Controls.Add(this.lbl_username);
			main.panel_main.Controls.Add(this.btn_addFriends);
			main.panel_main.Controls.Add(this.btn_removeFriends);

			((System.ComponentModel.ISupportInitialize)(this.pic_seperator)).EndInit();
		}

		private void ConfigStats()
		{
			const int X = 10;
			int y = 10;

			const int panelX = 900;
			const int panelY = 50;
			const int padding = 5;
			const int defaultSize = panelY - 2 * padding;

			panel_stats.SuspendLayout();

			foreach (var stat in user.statistics)
			{
				string letter = stat.Key.ToString();
				int total = stat.Value.total;
				double accuracy = stat.Value.accuracy;
				TimeSpan time = stat.Value.time;

				(int r, int g, int b) colour = ((int)(255 * (1 - accuracy)), (int)(255 * (accuracy)), 0);

				Label lbl_letter = new System.Windows.Forms.Label()
				{
					Location = new System.Drawing.Point(0 + padding, 0 + padding),
					Name = "lbl_letter",
					Size = new System.Drawing.Size(defaultSize, defaultSize),
					TabIndex = 0,
					Text = letter,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_total = new System.Windows.Forms.Label()
				{
					Location = new System.Drawing.Point(panelX - 2 * defaultSize - padding, padding),
					Name = "lbl_total",
					Size = new System.Drawing.Size(2 * defaultSize, defaultSize),
					TabIndex = 1,
					Text = total.ToString(),
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_time = new System.Windows.Forms.Label()
				{
					Location = new System.Drawing.Point(lbl_total.Location.X - 2 * defaultSize - padding, padding),
					Name = "lbl_time",
					Size = new System.Drawing.Size(2 * defaultSize, defaultSize),
					TabIndex = 2,
					Text = $"{time.TotalSeconds}",
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_percentage = new System.Windows.Forms.Label()
				{
					Location = new System.Drawing.Point(lbl_time.Location.X - defaultSize - padding, padding),
					Name = "lbl_percentage",
					Size = new System.Drawing.Size(defaultSize, defaultSize),
					TabIndex = 3,
					Text = $"{100 * accuracy}%",
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Guna2Panel bar_base = new Guna2Panel()
				{
					BackColor = System.Drawing.SystemColors.ControlLight,
					BorderRadius = 3,
					FillColor = System.Drawing.SystemColors.ControlLight,
					Location = new System.Drawing.Point(lbl_letter.Location.X + defaultSize + padding, 2 * padding),
					Name = "bar_base",
					Size = new System.Drawing.Size(lbl_percentage.Location.X - padding - (lbl_letter.Location.X + defaultSize + padding), defaultSize - 2 * padding),
					TabStop = false,
				};
				Guna2Panel bar_fill = new Guna2Panel()
				{
					BackColor = System.Drawing.ColorTranslator.FromHtml($"{colour.r}, {colour.g}, {colour.b}"),
					BorderRadius = 3,
					FillColor = System.Drawing.ColorTranslator.FromHtml($"{colour.r}, {colour.g}, {colour.b}"),
					Location = new System.Drawing.Point(bar_base.Location.X, bar_base.Location.Y),
					Name = "panel_fill",
					Size = new System.Drawing.Size(((int)(accuracy * bar_base.Size.Width)), bar_base.Size.Height),
					TabStop = false,
				};

				Guna2Panel panel_char = new Guna2Panel()
				{
					BackColor = System.Drawing.SystemColors.ControlDark,
					BorderRadius = 10,
					FillColor = System.Drawing.SystemColors.ControlDark,
					Location = new System.Drawing.Point(X, y),
					Name = "panel_char",
					Size = new System.Drawing.Size(panelX, panelY),
					TabStop = false,
				};

				panel_char.Controls.Add(bar_fill);
				panel_char.Controls.Add(bar_base);
				panel_char.Controls.Add(lbl_percentage);
				panel_char.Controls.Add(lbl_time);
				panel_char.Controls.Add(lbl_total);
				panel_char.Controls.Add(lbl_letter);

				bar_fill.BringToFront();
				
				panel_stats.Controls.Add(panel_char);

				y += panelY + 2 * padding;
			}

			panel_stats.ResumeLayout();
		}

		private Guna2TextBox lbl_username;
		private Guna2TextBox lbl_rank;
		private Guna2TextBox lbl_total;
		private Guna2TextBox lbl_accuracy;
		private Guna2GradientButton btn_addFriends;
		private Guna2GradientButton btn_removeFriends;
		private PictureBox pic_seperator;
		private Panel panel_stats;
	}
}