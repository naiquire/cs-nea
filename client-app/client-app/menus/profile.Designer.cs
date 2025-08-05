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
            resetLayout(main);
            
            lbl_username = new Label();
            lbl_rank = new Label();
            pic_language = new PictureBox();
            panel_stats = new Panel();
            ///
            /// lbl_username
            ///
            lbl_username.BackColor = main.panel_main.BackColor;
            lbl_username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbl_username.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_username.Location = new System.Drawing.Point(20, 20);
            lbl_username.Name = "lbl_username";
            lbl_username.Size = new System.Drawing.Size(200, 40);
            lbl_username.TabIndex = 0;
            lbl_username.Text = userData.userID;
            ///
            /// lbl_rank
            ///
            lbl_rank.BackColor = main.panel_main.BackColor;
            lbl_rank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbl_rank.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_rank.Location = new System.Drawing.Point(20, 80);
            lbl_rank.Name = "lbl_rank";
            lbl_rank.Size = new System.Drawing.Size(200, 40);
            lbl_rank.TabIndex = 0;
            lbl_rank.Text = userData.rank.ToString();
            ///
            /// pic_language
            ///
            //pic_language.Image = (System.Drawing.Image) global::client_app.Properties.Resources.ResourceManager.GetObject(userData.localisation); // may not work
            pic_language.Location = new System.Drawing.Point(20, 150);
            pic_language.Name = "pic_language";
            pic_language.Size = new System.Drawing.Size(400, 200);
            pic_language.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pic_language.TabIndex = 0;
            pic_language.TabStop = false;
			pic_language.BackColor = main.panel_left.BackColor;
			///
			/// panel_stats
			/// 
			panel_stats.Name = "panel_stats";
			panel_stats.BackColor = main.panel_main.BackColor;
			panel_stats.BorderStyle = BorderStyle.FixedSingle;
			panel_stats.Location = new System.Drawing.Point(50, 150);
			panel_stats.Size = new System.Drawing.Size(main.panel_main.Width - 100, main.panel_main.Height - 100 - 100);


			main.panel_main.Controls.Add(lbl_username);
			main.panel_main.Controls.Add(lbl_rank);
			//main.panel_main.Controls.Add(pic_language);
			main.panel_main.Controls.Add(panel_stats);

            configStats();
        }

        private void configStats()
        {
            const int X = 10;
            int y = 10;

            const int panelX = 900;
            const int panelY = 50;
            const int padding = 5;
            const int defaultSize = panelY - 2 * padding;

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

        private Label lbl_username;
        private Label lbl_rank;
        private PictureBox pic_language;
        private Panel panel_stats;

        #endregion
    }
}