using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace client_app.menus
{
	public class abstractMenu : Form
	{
		protected Panel panel_topBorder;
		protected Button btn_close;
		protected Label lbl_appName;
		public Panel panel_left;
		protected Panel panel_topLeft;
		public Panel panel_main;
		public Panel panel_right;
		private Panel panel_char;
		private TextBox lbl_letter;
		private TextBox lbl_total;
		private TextBox lbl_time;
		private TextBox lbl_percentage;
		private Panel bar_base;
		private Panel panel_fill;
		protected Button btn_home;

		public void tempInitializeComponent()
		{
			this.panel_char = new System.Windows.Forms.Panel();
			this.panel_fill = new System.Windows.Forms.Panel();
			this.bar_base = new System.Windows.Forms.Panel();
			this.lbl_percentage = new System.Windows.Forms.TextBox();
			this.lbl_time = new System.Windows.Forms.TextBox();
			this.lbl_total = new System.Windows.Forms.TextBox();
			this.lbl_letter = new System.Windows.Forms.TextBox();
			this.panel_char.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_char
			// 
			this.panel_char.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panel_char.Controls.Add(this.panel_fill);
			this.panel_char.Controls.Add(this.bar_base);
			this.panel_char.Controls.Add(this.lbl_percentage);
			this.panel_char.Controls.Add(this.lbl_time);
			this.panel_char.Controls.Add(this.lbl_total);
			this.panel_char.Controls.Add(this.lbl_letter);
			this.panel_char.Location = new System.Drawing.Point(159, 138);
			this.panel_char.Name = "panel_char";
			this.panel_char.Size = new System.Drawing.Size(800, 50);
			this.panel_char.TabIndex = 0;
			// 
			// panel_fill
			// 
			this.panel_fill.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.panel_fill.Location = new System.Drawing.Point(50, 10);
			this.panel_fill.Name = "panel_fill";
			this.panel_fill.Size = new System.Drawing.Size(283, 30);
			this.panel_fill.TabIndex = 5;
			// 
			// bar_base
			// 
			this.bar_base.BackColor = System.Drawing.SystemColors.ControlLight;
			this.bar_base.Location = new System.Drawing.Point(50, 10);
			this.bar_base.Name = "bar_base";
			this.bar_base.Size = new System.Drawing.Size(530, 30);
			this.bar_base.TabIndex = 4;
			// 
			// lbl_percentage
			// 
			this.lbl_percentage.Location = new System.Drawing.Point(585, 5);
			this.lbl_percentage.Multiline = true;
			this.lbl_percentage.Name = "lbl_percentage";
			this.lbl_percentage.ReadOnly = true;
			this.lbl_percentage.Size = new System.Drawing.Size(40, 40);
			this.lbl_percentage.TabIndex = 3;
			this.lbl_percentage.Text = "97%";
			// 
			// lbl_time
			// 
			this.lbl_time.Location = new System.Drawing.Point(630, 5);
			this.lbl_time.Multiline = true;
			this.lbl_time.Name = "lbl_time";
			this.lbl_time.ReadOnly = true;
			this.lbl_time.Size = new System.Drawing.Size(80, 40);
			this.lbl_time.TabIndex = 2;
			this.lbl_time.Text = "00:00";
			// 
			// lbl_total
			// 
			this.lbl_total.Location = new System.Drawing.Point(715, 5);
			this.lbl_total.Multiline = true;
			this.lbl_total.Name = "lbl_total";
			this.lbl_total.ReadOnly = true;
			this.lbl_total.Size = new System.Drawing.Size(80, 40);
			this.lbl_total.TabIndex = 1;
			this.lbl_total.Text = "total";
			// 
			// lbl_letter
			// 
			this.lbl_letter.Location = new System.Drawing.Point(5, 5);
			this.lbl_letter.Multiline = true;
			this.lbl_letter.Name = "lbl_letter";
			this.lbl_letter.ReadOnly = true;
			this.lbl_letter.Size = new System.Drawing.Size(40, 40);
			this.lbl_letter.TabIndex = 0;
			this.lbl_letter.Text = "char";
			// 
			// abstractMenu
			// 
			this.ClientSize = new System.Drawing.Size(1119, 504);
			this.Controls.Add(this.panel_char);
			this.Name = "abstractMenu";
			this.panel_char.ResumeLayout(false);
			this.panel_char.PerformLayout();
			this.ResumeLayout(false);

		}

		public virtual void InitializeComponent()
		{
			// OPENING DESIGNER WILL BREAK THIS MODULE

			this.Controls.Clear();

			this.panel_topBorder = new System.Windows.Forms.Panel();
			this.lbl_appName = new System.Windows.Forms.Label();
			this.btn_close = new System.Windows.Forms.Button();
			this.btn_home = new System.Windows.Forms.Button();
			this.panel_left = new System.Windows.Forms.Panel();
			this.panel_topLeft = new System.Windows.Forms.Panel();
			this.panel_main = new System.Windows.Forms.Panel();
			this.panel_right = new System.Windows.Forms.Panel();
			this.panel_topBorder.SuspendLayout();
			this.panel_left.SuspendLayout();
			this.panel_main.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_topBorder
			// 
			this.panel_topBorder.BackColor = Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
			this.panel_topBorder.Controls.Add(this.lbl_appName);
			this.panel_topBorder.Controls.Add(this.btn_close);
			this.panel_topBorder.Location = new Point(0, 0);
			this.panel_topBorder.Name = "panel_topBorder";
			this.panel_topBorder.Size = new Size(1920, 30);
			this.panel_topBorder.TabIndex = 0;
			// 
			// lbl_appName
			// 
			this.lbl_appName.BackColor = this.panel_topBorder.BackColor;
			this.lbl_appName.Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
			this.lbl_appName.Location = new Point(10, 7);
			this.lbl_appName.Name = "lbl_appName";
			this.lbl_appName.Size = new Size(100, 16);
			this.lbl_appName.TabIndex = 0;
			this.lbl_appName.Text = "appName";
			// 
			// btn_close
			// 
			this.btn_close.Location = new Point(1890, 0);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new Size(30, 30);
			this.btn_close.TabIndex = 0;
			this.btn_close.Text = "X";
			this.btn_close.UseVisualStyleBackColor = true;
			this.btn_close.Click += new EventHandler(this.btn_close_Click);
			// 
			// btn_home
			// 
			this.btn_home.Location = new Point(50, 910);
			this.btn_home.Name = "btn_home";
			this.btn_home.Size = new Size(200, 30);
			this.btn_home.TabIndex = 0;
			this.btn_home.Text = "HOME";
			this.btn_home.UseVisualStyleBackColor = true;
			// 
			// panel_left
			// 
			this.panel_left.AutoScroll = true;
			this.panel_left.BackColor = Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			this.panel_left.Controls.Add(this.btn_home);
			this.panel_left.Location = new Point(0, 130);
			this.panel_left.Name = "panel_left";
			this.panel_left.Size = new Size(300, 950);
			this.panel_left.TabIndex = 2;
			// 
			// panel_topLeft
			// 
			this.panel_topLeft.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
			this.panel_topLeft.Location = new Point(0, 30);
			this.panel_topLeft.Name = "panel_topLeft";
			this.panel_topLeft.Size = new Size(300, 100);
			this.panel_topLeft.TabIndex = 1;
			// 
			// panel_main
			// 
			this.panel_main.BackColor = Color.Transparent;
			this.panel_main.Location = new Point(300, 30);
			this.panel_main.Name = "panel_main";
			this.panel_main.Size = new Size(1120, 1050);
			this.panel_main.TabIndex = 4;
			// 
			// panel_right
			// 
			this.panel_right.BackColor = Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			this.panel_right.Location = new Point(1420, 30);
			this.panel_right.Name = "panel_right";
			this.panel_right.Size = new Size(500, 1050);
			this.panel_right.TabIndex = 3;
			// 
			// abstractMenu
			// 
			this.BackColor = Color.White;
			this.ClientSize = new Size(1920, 1080);
			this.Controls.Add(this.panel_topLeft);
			this.Controls.Add(this.panel_topBorder);
			this.Controls.Add(this.panel_left);
			this.Controls.Add(this.panel_main);
			this.Controls.Add(this.panel_right);
			this.FormBorderStyle = FormBorderStyle.None;
			this.Name = "abstractMenu";
			this.panel_topBorder.ResumeLayout(false);
			this.panel_left.ResumeLayout(false);
			this.panel_main.ResumeLayout(false);
			this.ResumeLayout(false);
		}

		/// <summary>
		/// Resets the current window to the base UI.
		/// </summary>
		/// <param name="main"></param>
		protected void resetLayout(main main)
		{
			main.panel_main?.Controls.Clear();
			main.panel_left?.Controls.Clear();
			main.panel_right?.Controls.Clear();
			main.btn_home.Click -= main.btn_home_Click;

			main.panel_left.Controls.Add(main.btn_home);
			main.btn_home.Click += main.btn_home_Click;
		}
		protected virtual void aInitializeComponent(main main)
		{
			resetLayout(main);

			main.panel_topBorder = new System.Windows.Forms.Panel();
			main.lbl_appName = new System.Windows.Forms.Label();
			main.btn_close = new System.Windows.Forms.Button();
			main.btn_home = new System.Windows.Forms.Button();
			main.panel_left = new System.Windows.Forms.Panel();
			main.panel_topLeft = new System.Windows.Forms.Panel();
			main.panel_main = new System.Windows.Forms.Panel();
			main.panel_right = new System.Windows.Forms.Panel();
			main.panel_topBorder.SuspendLayout();
			main.panel_left.SuspendLayout();
			main.SuspendLayout();
			// 
			// panel_topBorder
			// 
			main.panel_topBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
			main.panel_topBorder.Controls.Add(main.lbl_appName);
			main.panel_topBorder.Controls.Add(main.btn_close);
			main.panel_topBorder.Location = new System.Drawing.Point(0, 0);
			main.panel_topBorder.Name = "panel_topBorder";
			main.panel_topBorder.Size = new System.Drawing.Size(1920, 30);
			main.panel_topBorder.TabIndex = 0;
			// 
			// lbl_appName
			// 
			main.lbl_appName.BackColor = main.panel_topBorder.BackColor;
			main.lbl_appName.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			main.lbl_appName.Location = new System.Drawing.Point(10, 7);
			main.lbl_appName.Name = "lbl_appName";
			main.lbl_appName.Size = new System.Drawing.Size(100, 16);
			main.lbl_appName.TabIndex = 0;
			main.lbl_appName.Text = "appName";
			// 
			// btn_close
			// 
			main.btn_close.Location = new System.Drawing.Point(1890, 0);
			main.btn_close.Name = "btn_close";
			main.btn_close.Size = new System.Drawing.Size(30, 30);
			main.btn_close.TabIndex = 0;
			main.btn_close.Text = "X";
			main.btn_close.UseVisualStyleBackColor = true;
			main.btn_close.Click += new System.EventHandler(main.btn_close_Click);
			// 
			// btn_home
			// 
			main.btn_home.Location = new System.Drawing.Point(50, 910);
			main.btn_home.Name = "btn_home";
			main.btn_home.Size = new System.Drawing.Size(200, 30);
			main.btn_home.TabIndex = 0;
			main.btn_home.Text = "HOME";
			main.btn_home.UseMnemonic = false;
			main.btn_home.UseVisualStyleBackColor = true;
			//main.btn_home.Click += (sender, e) => { btn_home_Click(sender, e, main); };
			// 
			// panel_left
			// 
			main.panel_left.AutoScroll = true;
			main.panel_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			main.panel_left.Controls.Add(main.btn_home);
			main.panel_left.Location = new System.Drawing.Point(0, 130);
			main.panel_left.Name = "panel_left";
			main.panel_left.Size = new System.Drawing.Size(300, 950);
			main.panel_left.TabIndex = 2;
			// 
			// panel_topLeft
			// 
			main.panel_topLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
			main.panel_topLeft.Location = new System.Drawing.Point(0, 30);
			main.panel_topLeft.Name = "panel_topLeft";
			main.panel_topLeft.Size = new System.Drawing.Size(300, 100);
			main.panel_topLeft.TabIndex = 1;
			// 
			// panel_main
			// 
			main.panel_main.BackColor = System.Drawing.Color.Transparent;
			main.panel_main.Location = new System.Drawing.Point(300, 30);
			main.panel_main.Name = "panel_main";
			main.panel_main.Size = new System.Drawing.Size(1120, 1050);
			main.panel_main.TabIndex = 4;
			// 
			// panel_right
			// 
			main.panel_right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			main.panel_right.Location = new System.Drawing.Point(1420, 30);
			main.panel_right.Name = "panel_right";
			main.panel_right.Size = new System.Drawing.Size(500, 1050);
			main.panel_right.TabIndex = 3;
			// 
			// abstractMenu
			// 
			main.BackColor = System.Drawing.Color.White;
			main.ClientSize = new System.Drawing.Size(1920, 1080);
			main.Controls.Add(main.panel_topLeft);
			main.Controls.Add(main.panel_topBorder);
			main.Controls.Add(main.panel_left);
			main.Controls.Add(main.panel_main);
			main.Controls.Add(main.panel_right);
			main.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			main.Name = "abstractMenu";
			main.panel_topBorder.ResumeLayout(false);
			main.panel_left.ResumeLayout(false);
			main.ResumeLayout(false);

		} // no references, unlikely needed in final

		/// <summary>
		/// Configures the right panel with userData.
		/// </summary>
		protected void configPanel_right_userData(main main, userData userData)
		{
			//throw new NotImplementedException();
		}

		/// <summary>
		/// Loads the Lobby menu.
		/// </summary>
		/// <param name="main"></param>
		/// <param name="users"></param>
		protected void initialiseLobby(main main, List<friendData> users)
		{
			resetLayout(main);

			Label lbl_header = new Label
			{
				BackColor = main.panel_main.BackColor,
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				Location = new System.Drawing.Point(10, 10),
				Name = "lbl_header",
				Size = new System.Drawing.Size(300, 30),
				TabIndex = 0,
				Text = $"Lobby <{game.type}>"
			};

			main.panel_main.Controls.Add(lbl_header);

			configPanel_main_lobby(main, users);
		}

		/// <summary>
		/// Configures a Panel to display the current users in the queued game.
		/// </summary>
		/// <param name="main"></param>
		/// <param name="users"></param>
		protected void configPanel_main_lobby(main main, List<friendData> users)
		{
			#region temp
			users = main.userData.friends;
			users.Add(new friendData()
			{
				userID = main.userData.userID,
				rank = main.userData.rank,
			});
			#endregion

			Panel panel_users = new Panel()
			{
				Name = "panel_users",
				BackColor = main.panel_main.BackColor,
				BorderStyle = BorderStyle.FixedSingle,
				Location = new System.Drawing.Point(50, 150),
				Size = new System.Drawing.Size(main.panel_main.Width - 100, main.panel_main.Height - 100 - 100)
			};

			int X = 10;
			int Y = 10;
			const int userX = 400;
			const int userY = 50;
			const int padding = 5;

			foreach (friendData user in users)
			{
				Panel panel_user = new Panel()
				{
					Name = user.userID,
					BackColor = panel_users.BackColor,
					BorderStyle = BorderStyle.FixedSingle,
					Location = new System.Drawing.Point(X, Y),
					Size = new System.Drawing.Size(userX, userY),
				};

				Label userID = new Label()
				{
					BackColor = main.panel_main.BackColor,
					Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
					Location = new System.Drawing.Point(padding, padding),
					Name = user.userID,
					Size = new System.Drawing.Size(userX - userY - 2 * padding, userY - 2 * padding),
					TabIndex = 0,
					Text = user.userID,
					BorderStyle = BorderStyle.FixedSingle,
					TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
				};
				Label rank = new Label()
				{
					BackColor = main.panel_main.BackColor,
					Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
					Location = new System.Drawing.Point(userID.Width + 2 * padding, padding),
					Name = "rank",
					Size = new System.Drawing.Size(userY - padding, userY - 2 * padding),
					TabIndex = 0,
					Text = user.rank.ToString(),
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};

				panel_user.Controls.Add(userID);
				panel_user.Controls.Add(rank);

				panel_users.Controls.Add(panel_user);

				Y = Y + userY + 10;
			}

			main.panel_main.Controls.Add(panel_users);
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async void btn_close_Click(object sender, EventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			Hide();
			//await main.connection.InvokeAsync("clientDisconnected", main.userData.userID);
			Close();
		}

	}
}
