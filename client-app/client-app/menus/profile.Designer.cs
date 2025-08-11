using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace client_app.menus
{
    partial class profile
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public void InitializeComponent()
        {
            interfaces.resetLayout(main);

			this.lbl_username = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbl_rank = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbl_total = new Guna.UI2.WinForms.Guna2TextBox();
			this.lbl_accuracy = new Guna.UI2.WinForms.Guna2TextBox();
			this.pic_seperator = new System.Windows.Forms.PictureBox();
			this.panel_stats = new Guna.UI2.WinForms.Guna2Panel();
			((System.ComponentModel.ISupportInitialize)(this.pic_seperator)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_username
			// 
			this.lbl_username.BorderRadius = 20;
			this.lbl_username.BorderThickness = 0;
			this.lbl_username.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_username.DefaultText = "username";
			this.lbl_username.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.lbl_username.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.lbl_username.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_username.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_username.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.lbl_username.Font = new System.Drawing.Font("Bahnschrift SemiBold", 31.75F, System.Drawing.FontStyle.Bold);
			this.lbl_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
			this.lbl_username.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
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
			this.lbl_rank.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.lbl_rank.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.lbl_rank.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_rank.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_rank.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.lbl_rank.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
			this.lbl_rank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
			this.lbl_rank.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
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
			this.lbl_total.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.lbl_total.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.lbl_total.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_total.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_total.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.lbl_total.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
			this.lbl_total.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
			this.lbl_total.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
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
			this.lbl_accuracy.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.lbl_accuracy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.lbl_accuracy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_accuracy.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_accuracy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.lbl_accuracy.Font = new System.Drawing.Font("Bahnschrift", 15.75F);
			this.lbl_accuracy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
			this.lbl_accuracy.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
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
			this.pic_seperator.TabIndex = 4;
			this.pic_seperator.TabStop = false;
			// 
			// panel_stats
			// 
			this.panel_stats.BorderRadius = 0;
			this.panel_stats.FillColor = System.Drawing.Color.White;
			this.panel_stats.Location = new System.Drawing.Point(40, 175);
			this.panel_stats.Name = "panel_stats";
			this.panel_stats.Size = new System.Drawing.Size(1040, 835);
			this.panel_stats.TabIndex = 5;
			this.panel_stats.AutoScroll = true;

			(string rank, string total, string accuracy) = interfaces.calculateStatsOverview(userData);

			this.lbl_username.Text = userData.userID;
			this.lbl_rank.Text = rank;
			this.lbl_total.Text = total;
			this.lbl_accuracy.Text = accuracy + "%";

			configStats();

			main.panel_main.Controls.Add(this.panel_stats);
			main.panel_main.Controls.Add(this.pic_seperator);
			main.panel_main.Controls.Add(this.lbl_accuracy);
			main.panel_main.Controls.Add(this.lbl_total);
			main.panel_main.Controls.Add(this.lbl_rank);
			main.panel_main.Controls.Add(this.lbl_username);

			((System.ComponentModel.ISupportInitialize)(this.pic_seperator)).EndInit();

            main.configFriendsPanel();
            interfaces.configUserDataPanel(main, userData);
        }

        private void configStats()
        {
            const int X = 10;
            int y = 10;

            const int panelX = 900;
            const int panelY = 50;
            const int padding = 5;
            const int defaultSize = panelY - 2 * padding;

			panel_stats.SuspendLayout();

            foreach (var stat in userData.statistics)
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
                Panel bar_base = new System.Windows.Forms.Panel()
                {
					BackColor = System.Drawing.SystemColors.ControlLight,
					Location = new System.Drawing.Point(lbl_letter.Location.X + defaultSize + padding, 2 * padding),
					Name = "bar_base",
					Size = new System.Drawing.Size(lbl_percentage.Location.X - padding - (lbl_letter.Location.X + defaultSize + padding), defaultSize - 2 * padding),
					TabIndex = 4,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Panel bar_fill = new System.Windows.Forms.Panel()
                {
					BackColor = System.Drawing.ColorTranslator.FromHtml($"{colour.r}, {colour.g}, {colour.b}"),
					Location = new System.Drawing.Point(bar_base.Location.X, bar_base.Location.Y),
					Name = "panel_fill",
					Size = new System.Drawing.Size(((int)(accuracy * bar_base.Size.Width)), bar_base.Size.Height),
					TabIndex = 5,
					BorderStyle = BorderStyle.FixedSingle,
				};
                
				Panel panel_char = new System.Windows.Forms.Panel()
                {
					BackColor = System.Drawing.SystemColors.ControlDark,
					Location = new System.Drawing.Point(X, y),
					Name = "panel_char",
					Size = new System.Drawing.Size(panelX, panelY),
					TabIndex = 0,
					BorderStyle = BorderStyle.FixedSingle,
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
        }

		private Guna.UI2.WinForms.Guna2TextBox lbl_username;
		private Guna.UI2.WinForms.Guna2TextBox lbl_rank;
		private Guna.UI2.WinForms.Guna2TextBox lbl_total;
		private Guna.UI2.WinForms.Guna2TextBox lbl_accuracy;
		private PictureBox pic_seperator;
		private Guna.UI2.WinForms.Guna2Panel panel_stats;

		#endregion
	}
}