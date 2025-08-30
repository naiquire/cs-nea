using client_app.components;
using client_app.menus.games;
using client_app.Properties;
using Guna.UI2.WinForms;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus
{
	public abstract class interfaces : Form // can be made static in final build, maybe rename
	{
		public static int clientX = Screen.PrimaryScreen.WorkingArea.Width;
		public static int clientY = Screen.PrimaryScreen.WorkingArea.Height;

		public void tempInitializeComponent()
		{

			this.SuspendLayout();

			// 
			// panel_stat
			// 

			// 
			// interfaces
			// 
			this.BackColor = Color.FromArgb(35, 31, 32);
			this.ClientSize = new Size(500, 1050);

			this.FormBorderStyle = FormBorderStyle.None;
			this.Name = "interfaces";

			this.ResumeLayout(false);

		}

		public static void InitializeComponent(main main)
		{
			// OPENING DESIGNER WILL BREAK THIS MODULE

			main.Controls.Clear();

			main.panel_topBorder = new Panel();
			main.lbl_appName = new Label();
			main.btn_close = new Button();
			main.btn_home = new Button();
			main.panel_left = new Panel();
			main.panel_topLeft = new Panel();
			main.panel_main = new Panel();
			main.panel_right = new Panel();
			main.panel_topBorder.SuspendLayout();
			main.panel_left.SuspendLayout();
			main.panel_main.SuspendLayout();
			main.SuspendLayout();
			// 
			// panel_topBorder
			// 
			main.panel_topBorder.BackColor = Color.FromArgb(26, 23, 24);
			main.panel_topBorder.Controls.Add(main.lbl_appName);
			main.panel_topBorder.Controls.Add(main.btn_close);
			main.panel_topBorder.Location = new Point(0, 0);
			main.panel_topBorder.Name = "panel_topBorder";
			main.panel_topBorder.Size = new Size(1920, 30);
			main.panel_topBorder.TabIndex = 0;
			// 
			// lbl_appName
			// 
			main.lbl_appName.BackColor = main.panel_topBorder.BackColor;
			main.lbl_appName.Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			main.lbl_appName.Location = new Point(10, 7);
			main.lbl_appName.Name = "lbl_appName";
			main.lbl_appName.Size = new Size(100, 16);
			main.lbl_appName.TabIndex = 0;
			main.lbl_appName.Text = "Glyph";
			// 
			// btn_close
			// 
			main.btn_close.Location = new Point(1890, 0);
			main.btn_close.Name = "btn_close";
			main.btn_close.Size = new Size(30, 30);
			main.btn_close.TabIndex = 0;
			main.btn_close.Text = "X";
			main.btn_close.UseVisualStyleBackColor = true;
			main.btn_close.Click += new EventHandler(main.btn_close_Click);
			// 
			// btn_home
			// 
			main.btn_home.Location = new Point(50, 880);
			main.btn_home.Name = "btn_home";
			main.btn_home.Size = new Size(200, 30);
			main.btn_home.TabIndex = 0;
			main.btn_home.Text = "HOME";
			main.btn_home.UseVisualStyleBackColor = true;
			// 
			// panel_left
			// 
			main.panel_left.AutoScroll = true;
			main.panel_left.BackColor = Color.FromArgb(35, 31, 32);
			main.panel_left.Controls.Add(main.btn_home);
			main.panel_left.Location = new Point(0, 130);
			main.panel_left.Name = "panel_left";
			main.panel_left.Size = new Size(300, 950);
			main.panel_left.TabIndex = 2;
			// 
			// panel_topLeft
			// 
			main.panel_topLeft.BackColor = Color.FromArgb(46, 46, 46);
			main.panel_topLeft.Location = new Point(0, 30);
			main.panel_topLeft.Name = "panel_topLeft";
			main.panel_topLeft.Size = new Size(300, 100);
			main.panel_topLeft.TabIndex = 1;
			// 
			// panel_main
			// 
			main.panel_main.BackColor = Color.FromArgb(104, 104, 104); ;
			main.panel_main.Location = new Point(300, 30);
			main.panel_main.Name = "panel_main";
			main.panel_main.Size = new Size(1120, clientY - 30);
			main.panel_main.TabIndex = 4;
			// 
			// panel_right
			// 
			main.panel_right.BackColor = Color.FromArgb(35, 31, 32);
			main.panel_right.Location = new Point(1420, 30);
			main.panel_right.Name = "panel_right";
			main.panel_right.Size = new Size(500, 1050);
			main.panel_right.TabIndex = 3;
			// 
			// abstractMenu
			// 
			main.AutoScroll = false;
			main.BackColor = Color.White;
			main.ClientSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			main.Controls.Add(main.panel_topLeft);
			main.Controls.Add(main.panel_topBorder);
			main.Controls.Add(main.panel_left);
			main.Controls.Add(main.panel_main);
			main.Controls.Add(main.panel_right);
			main.StartPosition = FormStartPosition.Manual;
			main.Location = new Point(0, 0);
			main.FormBorderStyle = FormBorderStyle.None;
			main.Name = "abstractMenu";
			main.panel_topBorder.ResumeLayout(false);
			main.panel_left.ResumeLayout(false);
			main.panel_main.ResumeLayout(false);
			main.ResumeLayout(false);

		}

		public static void resetLayout(main main)
		{
			main.panel_main?.Controls.Clear();
			main.panel_left?.Controls.Clear();
			main.panel_right?.Controls.Clear();
			main.btn_home.Click -= main.btn_home_Click;

			main.panel_left.Controls.Add(main.btn_home);
			main.btn_home.Click += main.btn_home_Click;
		}

		public static input configGamePanel(abstractGame game)
		{
			game.main.panel_main.Controls.Clear();

			game.panel_outline = new Guna2Shapes()
			{
				BorderColor = Color.FromArgb(52, 52, 52),
				BorderThickness = 10,
				FillColor = Color.White,
				Location = new Point(260, 250),
				Name = "panel_outline",
				PolygonSkip = 1,
				Rotate = 0F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Rounded,
				Size = new Size(600, 600),
				TabIndex = 1,
				Text = "panel_outline",
				Zoom = 100,
			};
			game.lbl_letter = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Calibri", 144F),
				ForeColor = Color.Black,
				Location = new Point(380, 50),
				Margin = new Padding(42, 47, 42, 47),
				Name = "lbl_letter",
				Size = new Size(360, 150),
				TabIndex = 2,
				TextAlignment = ContentAlignment.MiddleCenter,
			};
			game.btn_submit = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 49,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift SemiBold", 31.75F, FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(460, 900),
				Name = "btn_submit",
				Size = new Size(440, 100),
				TabIndex = 3,
				Text = "Submit",
			};
			game.btn_clear = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 49,
				FillColor = Color.FromArgb(156, 156, 156),
				FillColor2 = Color.FromArgb(156, 156, 156),
				Font = new Font("Bahnschrift SemiBold", 31.75F, FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(220, 900),
				Name = "btn_clearDrawing",
				Size = new Size(225, 100),
				TabIndex = 4,
				Text = "Clear",
			};

			var input = new input(game.main.panel_main, (260, 250), (600, 600));

			game.main.panel_main.Controls.Add(game.panel_outline);
			game.main.panel_main.Controls.Add(game.lbl_letter);
			game.main.panel_main.Controls.Add(game.btn_submit);
			game.main.panel_main.Controls.Add(game.btn_clear);
			return input;
		}

		public static void configRightGamePanel(abstractGame game)
		{
			game.lbl_rounds = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(35, 31, 32),
				Cursor = Cursors.Arrow,
				Font = new Font("Bahnschrift SemiBold", 48F, FontStyle.Bold),
				Location = new Point(40, 40),
				Margin = new Padding(15, 15, 15, 15),
				Name = "lbl_rounds",
				Size = new Size(420, 80),
				TabStop = false,
				Text = $"Round {game.getRounds()}",
				TextAlignment = ContentAlignment.MiddleCenter,
			};
			PictureBox seperator = new PictureBox()
			{
				Image = Resources.seperator,
				Location = new Point(30, 120),
				Name = "seperator",
				Size = new Size(440, 7),
				SizeMode = PictureBoxSizeMode.StretchImage,
				TabIndex = 1,
				TabStop = false,
			};
			game.panel_stats = new Guna2Panel()
			{
				BorderRadius = 20,
				FillColor = Color.FromArgb(104, 104, 104),
				Location = new Point(40, 160),
				Name = "panel_stats",
				Size = new Size(420, 720),
				TabIndex = 2,
			};

			game.main.panel_right.Controls.Add(game.lbl_rounds);
			game.main.panel_right.Controls.Add(seperator);
			game.main.panel_right.Controls.Add(game.panel_stats);
		}
		public static void configRightGamePanelStats(Panel panel, List<char> letters, List<double> accuracies)
		{
			panel.Controls.Clear();

			// only display last 10 rounds
			int start = 0;
			if (letters.Count > 10)
			{
				start = letters.Count - 10;
			}

			const int panelY = 50;
			const int padding = 20;

			int y = 20;
			for (int i = start; i < letters.Count; i++, y += panelY + padding)
			{
				panel.Controls.Add(configStatPanel(letters[i], accuracies[i]));
			}

			Guna2Panel configStatPanel(char letter, double accuracy)
			{
				(int r, int g, int b) = ((int)(255 * (1 - accuracy)), (int)(255 * (accuracy)), 0);

				Guna2Panel panel_stat = new Guna2Panel()
				{
					BackColor = Color.FromArgb(104, 104, 104),
					BorderRadius = 10,
					FillColor = Color.FromArgb(156, 156, 156),
					Location = new Point(padding, y),
					Name = "panel_stat",
					Size = new Size(420 - 2 * padding, panelY),
					TabIndex = 3,
				};

				Label lbl_letter = new Label()
				{
					AutoSize = true,
					BackColor = Color.FromArgb(156, 156, 156),
					Font = new Font("Bahnschrift SemiBold", 27.75F, FontStyle.Bold),
					Location = new Point(10, 2),
					Name = "lbl_letter",
					Size = new Size(43, 45),
					TabIndex = 0,
					Text = letter.ToString(),
				};
				Panel bar_base = new Panel()
				{
					BackColor = Color.White,
					Location = new Point(60, 10),
					Name = "bar_base",
					Size = new Size(230, 30),
					TabIndex = 1,
				};
				Guna2TextBox lbl_accuracy = new Guna2TextBox()
				{
					BorderThickness = 0,
					Cursor = Cursors.Arrow,
					DefaultText = $"{Math.Round(accuracy * 100)}%",
					FillColor = Color.FromArgb(156, 156, 156),
					Font = new Font("Bahnschrift", 19.75F),
					ForeColor = Color.FromArgb(52, 52, 52),
					Location = new Point(290, 10),
					Name = "lbl_accuracy",
					Size = new Size(80, 30),
					TabIndex = 2,
					TextAlign = HorizontalAlignment.Right,
				};
				Guna2Panel bar_fill = new Guna2Panel()
				{
					BackColor = ColorTranslator.FromHtml($"{r}, {g}, {b}"),
					Location = new Point(60, 10),
					Name = "bar_fill",
					Size = new Size((int)(accuracy * bar_base.Size.Width), bar_base.Size.Height),
					TabIndex = 3,
				};

				panel_stat.Controls.Add(bar_fill);
				panel_stat.Controls.Add(lbl_accuracy);
				panel_stat.Controls.Add(bar_base);
				panel_stat.Controls.Add(lbl_letter);

				return panel_stat;
			}

		}

		public static void configLeftGamePanel(abstractGame game, List<friendData> users)
		{
			game.main.panel_left.Controls.Clear();

			Panel panel_players = new Panel()
			{
				AutoScroll = true,
				BackColor = Color.FromArgb(46, 46, 46),
				Location = new Point(20, 90),
				Name = "panel_friendList",
				Size = new Size(260, 384),
				TabIndex = 2,
			};
			PictureBox seperator = new PictureBox()
			{
				Image = Resources.seperator,
				InitialImage = null,
				Location = new Point(50, 60),
				Name = "seperator",
				Size = new Size(200, 5),
				SizeMode = PictureBoxSizeMode.StretchImage,
				TabIndex = 1,
				TabStop = false,
			};
			Label lbl_players = new Label()
			{
				BackColor = Color.FromArgb(35, 31, 32),
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(247, 113, 163),
				Location = new Point(0, 20),
				Name = "txt_friendsLabel",
				Size = new Size(300, 33),
				TabIndex = 0,
				Text = "Players",
				TextAlign = (ContentAlignment)HorizontalAlignment.Center,
			};

			int y_offset = 10;

			for (int i = 0; i < users.Count; i++, y_offset += 35)
			{
				Label user = new Label()
				{
					BackColor = panel_players.BackColor,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(10, y_offset),
					Size = new Size(200, 30),
					TabStop = false,
					Text = users[i].userID,
				};
				Label rank = new Label()
				{
					BackColor = panel_players.BackColor,
					BorderStyle = BorderStyle.None,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(210, y_offset),
					Name = "txt_onlineCount",
					Size = new Size(50, 20),
					TabIndex = 0,
					Text = users[i].rank.ToString(),
					TextAlign = ContentAlignment.MiddleRight,
				};

				panel_players.Controls.Add(user);
				panel_players.Controls.Add(rank);
			}

			game.main.panel_left.Controls.Add(panel_players);
			game.main.panel_left.Controls.Add(seperator);
			game.main.panel_left.Controls.Add(lbl_players);

			configCountdown(game);
		}
		public static void configLeftGamePanel(abstractGame game, List<friendData> alive, List<friendData> dead)
		{
			game.main.panel_left.Controls.Clear();

			Panel panel_players = new Panel()
			{
				AutoScroll = true,
				BackColor = Color.FromArgb(46, 46, 46),
				Location = new Point(20, 90),
				Name = "panel_friendList",
				Size = new Size(260, 384),
				TabIndex = 2,
			};
			PictureBox seperator = new PictureBox()
			{
				Image = Resources.seperator,
				InitialImage = null,
				Location = new Point(50, 60),
				Name = "seperator",
				Size = new Size(200, 5),
				SizeMode = PictureBoxSizeMode.StretchImage,
				TabIndex = 1,
				TabStop = false,
			};
			Label lbl_players = new Label()
			{
				BackColor = Color.FromArgb(35, 31, 32),
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(247, 113, 163),
				Location = new Point(0, 20),
				Name = "txt_friendsLabel",
				Size = new Size(300, 33),
				TabIndex = 0,
				Text = "Players",
				TextAlign = (ContentAlignment)HorizontalAlignment.Center,
			};

			const int labelX = 200;
			const int labelY = 30;
			const int padding = 5;

			int y_offset = 10;

			Label lbl_alive = new Label()
			{
				BackColor = panel_players.BackColor,
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				Location = new Point(10, y_offset),
				Size = new Size(200, labelY),
				TabIndex = 0,
				Text = "Alive"
			};
			panel_players.Controls.Add(lbl_alive);

			Label aliveCount = new Label()
			{
				BackColor = panel_players.BackColor,
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				Location = new Point(230, 10),
				Size = new Size(30, 20),
				TabIndex = 0,
				Text = alive.Count.ToString(),
				TextAlign = ContentAlignment.MiddleRight,
			};
			panel_players.Controls.Add(aliveCount);
			y_offset += 30;

			for (int i = 0; i < alive.Count; i++, y_offset += labelY + padding)
			{
				Label user = new Label()
				{
					BackColor = panel_players.BackColor,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(10, y_offset),
					Size = new Size(labelX, labelY),
					TabIndex = 0,
					Text = alive[i].userID,
					FlatStyle = FlatStyle.Flat,
				};
				panel_players.Controls.Add(user);
			}

			y_offset += 30;
			Label lbl_dead = new Label()
			{
				BackColor = panel_players.BackColor,
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				Location = new Point(10, y_offset),
				Size = new Size(200, labelY),
				TabIndex = 0,
				Text = "Eliminated",
			};
			panel_players.Controls.Add(lbl_dead);

			Label offlineCount = new Label()
			{
				BackColor = panel_players.BackColor,
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				Location = new Point(230, y_offset),
				Size = new Size(30, 20),
				TabIndex = 0,
				Text = dead.Count.ToString(),
				TextAlign = ContentAlignment.MiddleRight,
			};
			panel_players.Controls.Add(offlineCount);
			y_offset += 30;

			for (int i = 0; i < dead.Count; i++, y_offset += labelY + padding)
			{
				Label user = new Label()
				{
					BackColor = panel_players.BackColor,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(10, y_offset),
					Size = new Size(labelX, labelY),
					TabIndex = 0,
					Text = dead[i].userID,
					FlatStyle = FlatStyle.Flat,
				};
				panel_players.Controls.Add(user);
			}

			game.main.panel_left.Controls.Add(panel_players);
			game.main.panel_left.Controls.Add(seperator);
			game.main.panel_left.Controls.Add(lbl_players);

			configCountdown(game);
		}

		public static void configResultsPanel(abstractGame game, char c, stats stats)
		{
			game.main.panel_main.Controls.Clear();

			const int X = 100;
			int y = 500;

			const int panelX = 900;
			const int panelY = 50;
			const int padding = 5;
			const int defaultSize = panelY - 2 * padding;

			string letter = c.ToString();
			bool correct = stats.correct.Last();
			double accuracy = stats.accuracy.Last();
			TimeSpan time = stats.time.Last();

			(int r, int g, int b) = ((int)(255 * (1 - accuracy)), (int)(255 * (accuracy)), 0);

			Label lbl_letter = new Label()
			{
				Location = new Point(0 + padding, 0 + padding),
				Name = "lbl_letter",
				Size = new Size(defaultSize, defaultSize),
				TabIndex = 0,
				Text = letter,
				TextAlign = ContentAlignment.MiddleCenter,
				BorderStyle = BorderStyle.FixedSingle,
			};
			Label lbl_total = new Label()
			{
				Location = new Point(panelX - 2 * defaultSize - padding, padding),
				Name = "lbl_total",
				Size = new Size(2 * defaultSize, defaultSize),
				TabIndex = 1,
				Text = correct.ToString(),
				TextAlign = ContentAlignment.MiddleCenter,
				BorderStyle = BorderStyle.FixedSingle,
			};
			Label lbl_time = new Label()
			{
				Location = new Point(lbl_total.Location.X - 2 * defaultSize - padding, padding),
				Name = "lbl_time",
				Size = new Size(2 * defaultSize, defaultSize),
				TabIndex = 2,
				Text = $"{time.TotalSeconds}",
				TextAlign = ContentAlignment.MiddleCenter,
				BorderStyle = BorderStyle.FixedSingle,
			};
			Label lbl_percentage = new Label()
			{
				Location = new Point(lbl_time.Location.X - defaultSize - padding, padding),
				Name = "lbl_percentage",
				Size = new Size(defaultSize, defaultSize),
				TabIndex = 3,
				Text = $"{Math.Round(100 * accuracy, 2)}%",
				TextAlign = ContentAlignment.MiddleCenter,
				BorderStyle = BorderStyle.FixedSingle,
			};
			Panel bar_base = new Panel()
			{
				BackColor = SystemColors.ControlLight,
				Location = new Point(lbl_letter.Location.X + defaultSize + padding, 2 * padding),
				Name = "bar_base",
				Size = new Size(lbl_percentage.Location.X - padding - (lbl_letter.Location.X + defaultSize + padding), defaultSize - 2 * padding),
				TabIndex = 4,
				BorderStyle = BorderStyle.FixedSingle,
			};
			Panel bar_fill = new Panel()
			{
				BackColor = ColorTranslator.FromHtml($"{r}, {g}, {b}"),
				Location = new Point(bar_base.Location.X, bar_base.Location.Y),
				Name = "panel_fill",
				Size = new Size((int)(accuracy * bar_base.Size.Width), bar_base.Size.Height),
				TabIndex = 5,
				BorderStyle = BorderStyle.FixedSingle,
			};

			Panel panel_char = new Panel()
			{
				BackColor = SystemColors.ControlDark,
				Location = new Point(X, y),
				Name = "panel_char",
				Size = new Size(panelX, panelY),
				TabIndex = 0,
				BorderStyle = BorderStyle.FixedSingle,
			};
			game.btn_continue = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 49,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift SemiBold", 31.75F, FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(220, 900),
				Name = "btn_continue",
				Size = new Size(680, 100),
				TabIndex = 3,
				Text = "Continue",
			};

			panel_char.Controls.Add(bar_fill);
			panel_char.Controls.Add(bar_base);
			panel_char.Controls.Add(lbl_percentage);
			panel_char.Controls.Add(lbl_time);
			panel_char.Controls.Add(lbl_total);
			panel_char.Controls.Add(lbl_letter);

			bar_fill.BringToFront();

			game.btn_continue.Click += async (sender, e) =>
			{
				game.btn_continue.Enabled = false;
				await main.connection.InvokeAsync("requestRound", game.getGameID(), main.userData.userID);
			};

			game.main.panel_main.Controls.Add(panel_char);
			game.main.panel_main.Controls.Add(game.btn_continue);
		}

		public static void configUserDataPanel(main main, userData userData)
		{
			main.panel_right.Controls.Clear();

			Guna2PictureBox pic_account = new Guna2PictureBox()
			{
				Image = Resources.account,
				ImageRotate = 0F,
				Location = new Point(150, 50),
				Name = "pic_account",
				Size = new Size(200, 200),
				SizeMode = PictureBoxSizeMode.Zoom,
				TabIndex = 0,
				TabStop = false
			};
			Guna2GradientButton btn_profile = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				Location = new Point(140, 930),
				Name = "btn_profile",
				Size = new Size(220, 50),
				TabIndex = 3,
				Text = "Profile",
			};
			Guna2TextBox lbl_userID = new Guna2TextBox()
			{
				BackColor = Color.FromArgb(44, 39, 41),
				BorderThickness = 0,
				Cursor = Cursors.IBeam,
				DefaultText = userData.userID,
				BorderColor = Color.FromArgb(208, 208, 208),
				FillColor = Color.FromArgb(44, 39, 41),
				Font = new Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(40, 265),
				Margin = new Padding(6),
				Name = "lbl_userID",
				PlaceholderForeColor = Color.Transparent,
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new Size(420, 70),
				TabIndex = 13,
				TextAlign = HorizontalAlignment.Center,
			};
			Guna2TextBox lbl_aboutMe = new Guna2TextBox()
			{
				BorderColor = Color.FromArgb(156, 156, 156),
				BorderRadius = 10,
				BorderThickness = 4,
				Cursor = Cursors.Arrow,
				DefaultText = userData.aboutMe,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift SemiBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(40, 350),
				Margin = new Padding(5, 5, 5, 5),
				Multiline = true,
				Name = "header",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new Size(420, 180),
				TabIndex = 0,
				TabStop = false,
			};

			btn_profile.Click += (sender, e) =>
			{
				menu.profile = new profile(main, userData);
			};

			main.panel_right.Controls.Add(lbl_userID);
			main.panel_right.Controls.Add(lbl_aboutMe);
			main.panel_right.Controls.Add(btn_profile);
			main.panel_right.Controls.Add(pic_account);

			configStatsPanel(main.panel_right, (40, 570), userData);
		}
		public static void configStatsPanel(Panel panel, (int X, int Y) pos, userData user)
		{
			(string rank, string total, string accuracy) = main.calculateStatsOverview(user);

			Guna2Panel panel_statsOverview = new Guna2Panel()
			{
				BorderRadius = 20,
				BorderThickness = 5,
				BorderColor = Color.White,
				FillColor = Color.FromArgb(156, 156, 156),
				Location = new Point(pos.X, pos.Y),
				Name = "panel_statsOverview",
				Size = new Size(420, 230),
				TabIndex = 0,
			};
			Guna2Panel panel_rank = new Guna2Panel()
			{

				BackColor = Color.Transparent,
				BorderRadius = 20,
				FillColor = Color.FromArgb(208, 208, 208),
				Location = new Point(20, 20),
				Name = "panel_rank",
				Size = new Size(380, 50),
				TabIndex = 0,
			};
			Guna2Shapes circle_total = new Guna2Shapes()
			{
				BackColor = Color.Transparent,
				BorderColor = Color.White,
				BorderThickness = 5,
				FillColor = Color.Transparent,
				Location = new Point(31, 80),
				Name = "circle_total",
				PolygonSides = 3,
				PolygonSkip = 1,
				Rotate = 9F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
				Size = new Size(70, 70),
				TabIndex = 1,
				UseTransparentBackground = true,
				Zoom = 80,
			};
			Guna2Shapes line_stats = new Guna2Shapes()
			{
				BorderThickness = 0,
				FillColor = Color.White,
				LineThickness = 1,
				Location = new Point(63, 1),
				Name = "line_stats",
				PolygonSkip = 1,
				Rotate = 0F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle,
				Size = new Size(5, 228),
				TabIndex = 2,
				Text = "guna2Shapes2",
				Zoom = 100,
			};
			Guna2Shapes circle_rank = new Guna2Shapes()
			{
				BackColor = Color.Transparent,
				BorderColor = Color.White,
				BorderThickness = 5,
				FillColor = Color.Transparent,
				Location = new Point(31, 10),
				Name = "circle_rank",
				PolygonSides = 3,
				PolygonSkip = 1,
				Rotate = 9F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
				Size = new Size(70, 70),
				TabIndex = 3,
				Text = "guna2Shapes3",
				UseTransparentBackground = true,
				Zoom = 80,
			};
			Guna2Panel panel_total = new Guna2Panel()
			{
				BackColor = Color.Transparent,
				BorderRadius = 20,
				FillColor = Color.FromArgb(208, 208, 208),
				Location = new Point(20, 90),
				Name = "panel_total",
				Size = new Size(380, 50),
				TabIndex = 2,
			};
			Guna2PictureBox pic_rank = new Guna2PictureBox()
			{
				BackColor = Color.Transparent,
				FillColor = Color.Transparent,
				Image = Resources.rank,
				ImageRotate = 0F,
				Location = new Point(30, 10),
				Name = "pic_rank",
				Size = new Size(30, 30),
				SizeMode = PictureBoxSizeMode.Zoom,
				TabIndex = 4,
				TabStop = false,
				UseTransparentBackground = true,
			};
			Guna2PictureBox pic_total = new Guna2PictureBox()
			{
				BackColor = Color.Transparent,
				FillColor = Color.Transparent,
				Image = Resources.total,
				ImageRotate = 0F,
				Location = new Point(30, 10),
				Name = "pic_total",
				Size = new Size(30, 30),
				SizeMode = PictureBoxSizeMode.Zoom,
				TabIndex = 5,
				TabStop = false,
				UseTransparentBackground = true,
			};
			Guna2TextBox lbl_rank = new Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = "ELO",
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift SemiBold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(52, 52, 52),
				Location = new Point(90, 10),
				Margin = new Padding(3, 4, 3, 4),
				Name = "lbl_rank",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new Size(48, 30),
				TabIndex = 5,
			};
			Guna2TextBox lbl_total = new Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = "TOTAL",
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift SemiBold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(52, 52, 52),
				Location = new Point(90, 10),
				Margin = new Padding(3, 4, 3, 4),
				Name = "lbl_total",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new Size(62, 30),
				TabIndex = 6,
			};
			Guna2TextBox txt_rank = new Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = rank,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(104, 104, 104),
				Location = new Point(300, 10),
				Margin = new Padding(3, 4, 3, 4),
				Name = "txt_rank",
				PlaceholderText = "",
				ReadOnly = true,
				RightToLeft = RightToLeft.Yes,
				SelectedText = "",
				Size = new Size(60, 30),
				TabIndex = 6,
				TextOffset = new Point(0, -1),
			};
			Guna2TextBox txt_total = new Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = total,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(104, 104, 104),
				Location = new Point(300, 10),
				Margin = new Padding(3, 4, 3, 4),
				Name = "txt_total",
				PlaceholderText = "",
				ReadOnly = true,
				RightToLeft = RightToLeft.Yes,
				SelectedText = "",
				Size = new Size(60, 30),
				TabIndex = 7,
				TextOffset = new Point(0, -1),
			};
			Guna2Separator seperator_rank = new Guna2Separator()
			{

				FillColor = Color.FromArgb(247, 113, 163),
				FillThickness = 2,
				Location = new Point(144, 20),
				Name = "seperator_rank",
				Size = new Size(150, 10),
				TabIndex = 7,
			};
			Guna2Separator seperator_total = new Guna2Separator()
			{
				FillColor = Color.FromArgb(247, 113, 163),
				FillThickness = 2,
				Location = new Point(158, 20),
				Name = "seperator_total",
				Size = new Size(136, 10),
				TabIndex = 8,
			};
			Guna2Panel panel_accuracy = new Guna2Panel()
			{
				BackColor = Color.Transparent,
				BorderRadius = 20,
				FillColor = Color.FromArgb(208, 208, 208),
				Location = new Point(20, 160),
				Name = "panel_accuracy",
				Size = new Size(380, 50),
				TabIndex = 9,
			};
			Guna2Separator seperator_accuracy = new Guna2Separator()
			{
				FillColor = Color.FromArgb(247, 113, 163),
				FillThickness = 2,
				Location = new Point(189, 20),
				Name = "seperator_accuracy",
				Size = new Size(86, 10),
				TabIndex = 8,
			};
			Guna2TextBox txt_accuracy = new Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = $"{accuracy}%",
				FillColor = Color.FromArgb(208, 208, 208),
				BorderColor = Color.FromArgb(94, 148, 255),
				Font = new Font("Bahnschrift", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(104, 104, 104),
				Location = new Point(281, 10),
				Margin = new Padding(3, 4, 3, 4),
				Name = "txt_accuracy",
				PlaceholderText = "",
				ReadOnly = true,
				RightToLeft = RightToLeft.Yes,
				SelectedText = "",
				Size = new Size(79, 30),
				TabIndex = 7,
				TextOffset = new Point(0, -1),
			};
			Guna2TextBox lbl_accuracy = new Guna2TextBox()
			{

				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = "ACCURACY",
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift SemiBold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(52, 52, 52),
				Location = new Point(90, 10),
				Margin = new Padding(3, 4, 3, 4),
				Name = "lbl_accuracy",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new Size(93, 30),
				TabIndex = 6,
			};
			Guna2PictureBox pic_accuracy = new Guna2PictureBox()
			{
				BackColor = Color.Transparent,
				FillColor = Color.Transparent,
				Image = Resources.accuracy,
				ImageRotate = 0F,
				Location = new Point(27, 7),
				Name = "pic_accuracy",
				Size = new Size(36, 36),
				SizeMode = PictureBoxSizeMode.Zoom,
				TabIndex = 5,
				TabStop = false,
				UseTransparentBackground = true,
			};
			Guna2Shapes circle_accuracy = new Guna2Shapes()
			{
				BackColor = Color.Transparent,
				BorderColor = Color.White,
				BorderThickness = 5,
				FillColor = Color.Transparent,
				Location = new Point(30, 150),
				Name = "circle_accuracy",
				PolygonSides = 3,
				PolygonSkip = 1,
				Rotate = 9F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
				Size = new Size(70, 70),
				TabIndex = 10,
				UseTransparentBackground = true,
				Zoom = 80,
			};

			panel_rank.Controls.Add(seperator_rank);
			panel_rank.Controls.Add(txt_rank);
			panel_rank.Controls.Add(lbl_rank);
			panel_rank.Controls.Add(pic_rank);

			panel_total.Controls.Add(seperator_total);
			panel_total.Controls.Add(txt_total);
			panel_total.Controls.Add(lbl_total);
			panel_total.Controls.Add(pic_total);

			panel_accuracy.Controls.Add(seperator_accuracy);
			panel_accuracy.Controls.Add(txt_accuracy);
			panel_accuracy.Controls.Add(lbl_accuracy);
			panel_accuracy.Controls.Add(pic_accuracy);

			panel_statsOverview.Controls.Add(circle_accuracy);
			panel_statsOverview.Controls.Add(panel_accuracy);
			panel_statsOverview.Controls.Add(circle_rank);
			panel_statsOverview.Controls.Add(panel_rank);
			panel_statsOverview.Controls.Add(circle_total);
			panel_statsOverview.Controls.Add(panel_total);
			panel_statsOverview.Controls.Add(line_stats);

			panel.Controls.Add(panel_statsOverview);
		}

		public static void initialiseLobby(main main)
		{
			Label lbl_header = new Label
			{
				BackColor = main.panel_main.BackColor,
				Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				Location = new Point(10, 10),
				Name = "lbl_header",
				Size = new Size(300, 30),
				TabIndex = 0,
				Text = "Lobby",
			};

			main.panel_main.Controls.Add(lbl_header);
		}
		public static void configLobbyPanel(abstractGame game, List<friendData> users)
		{
			game.main.panel_main.Controls.Clear();
			initialiseLobby(game.main);

			Panel panel_users = new Panel()
			{
				Name = "panel_users",
				BackColor = game.main.panel_main.BackColor,
				BorderStyle = BorderStyle.FixedSingle,
				Location = new Point(50, 150),
				Size = new Size(game.main.panel_main.Width - 100, 500)
			};
			Guna2TextBox lbl_remainingPlayers = new Guna2TextBox()
			{
				BorderThickness = 0,
				BorderRadius = 10,
				Cursor = Cursors.Arrow,
				FillColor = Color.FromArgb(156, 156, 156),
				Font = new Font("Bahnschrift SemiBold", 32F, FontStyle.Bold),
				Location = new Point(220, 700),
				Size = new Size(680, 100),
				TabStop = false,
				ForeColor = Color.FromArgb(52, 52, 52),
				Text = $"{users.Count}/{game.getMaxPlayers()} players",
				TextAlign = HorizontalAlignment.Center,
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
					Location = new Point(X, Y),
					Size = new Size(userX, userY),
				};

				Label userID = new Label()
				{
					BackColor = game.main.panel_main.BackColor,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(padding, padding),
					Name = user.userID,
					Size = new Size(userX - userY - 2 * padding, userY - 2 * padding),
					TabIndex = 0,
					Text = user.userID,
					BorderStyle = BorderStyle.FixedSingle,
					TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
				};
				Label rank = new Label()
				{
					BackColor = game.main.panel_main.BackColor,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(userID.Width + 2 * padding, padding),
					Name = "rank",
					Size = new Size(userY - padding, userY - 2 * padding),
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

			game.main.panel_main.Controls.Add(panel_users);
			game.main.panel_main.Controls.Add(lbl_remainingPlayers);
		}

		private static void configCountdown(abstractGame game)
		{
			game.lbl_countdown = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(35, 31, 32),
				Font = new Font("Bahnschrift SemiBold", 64F, FontStyle.Bold),
				Location = new Point(20, 700),
				Margin = new Padding(15, 15, 15, 15),
				Size = new Size(260, 100),
				TabStop = false,
				TextAlignment = ContentAlignment.MiddleCenter,
			};
			game.lbl_status = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(35, 31, 32),
				Font = new Font("Bahnschrift SemiBold", 32F, FontStyle.Bold),
				Location = new Point(20, 650),
				Margin = new Padding(15, 15, 15, 15),
				Size = new Size(260, 50),
				TabStop = false,
				TextAlignment = ContentAlignment.MiddleCenter,
			};

			game.main.panel_left.Controls.Add(game.lbl_countdown);
			game.main.panel_left.Controls.Add(game.lbl_status);
		}
		public static async Task countdown(Guna2HtmlLabel lbl_countdown, int num, Guna2HtmlLabel lbl_status, string text)
		{
			lbl_status.Text = text;
			for (int i = num; i > 0; i--)
			{
				lbl_countdown.Text = i.ToString();
				await Task.Delay(1000);
			}
			lbl_countdown.ResetText();
			lbl_status.ResetText();
		}

		public static void configEndGamePanel(abstractGame game, List<char> letters, stats statistics)
		{
			game.main.panel_main.Controls.Clear();

			const int X = 10;
			int y = 10;

			const int panelX = 900;
			const int panelY = 50;
			const int padding = 5;
			const int defaultSize = panelY - 2 * padding;

			Guna2Panel panel_stats = new Guna2Panel()
			{
				AutoScroll = true,
				BorderRadius = 0,
				FillColor = Color.White,
				Location = new Point(40, 175),
				Name = "panel_stats",
				Size = new Size(1040, 500),
				TabIndex = 5,
			};
			panel_stats.VerticalScroll.Enabled = true;

			for (int i = 0; i < statistics.accuracy.Count; i++)
			{
				string letter = letters[i].ToString();
				bool correct = statistics.correct[i];
				double accuracy = statistics.accuracy[i];
				TimeSpan time = statistics.time[i];

				(int r, int g, int b) colour = ((int)(255 * (1 - accuracy)), (int)(255 * (accuracy)), 0);

				Label lbl_letter = new Label()
				{
					Location = new Point(0 + padding, 0 + padding),
					Name = "lbl_letter",
					Size = new Size(defaultSize, defaultSize),
					TabIndex = 0,
					Text = letter,
					TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_correct = new Label()
				{
					Location = new Point(panelX - 2 * defaultSize - padding, padding),
					Name = "lbl_correct",
					Size = new Size(2 * defaultSize, defaultSize),
					TabIndex = 1,
					Text = correct.ToString(),
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_time = new Label()
				{
					Location = new Point(lbl_correct.Location.X - 2 * defaultSize - padding, padding),
					Name = "lbl_time",
					Size = new Size(2 * defaultSize, defaultSize),
					TabIndex = 2,
					Text = $"{time.TotalSeconds}",
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_percentage = new Label()
				{
					Location = new Point(lbl_time.Location.X - defaultSize - padding, padding),
					Name = "lbl_percentage",
					Size = new Size(defaultSize, defaultSize),
					TabIndex = 3,
					Text = $"{100 * accuracy}%",
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Panel bar_base = new Panel()
				{
					BackColor = SystemColors.ControlLight,
					Location = new Point(lbl_letter.Location.X + defaultSize + padding, 2 * padding),
					Name = "bar_base",
					Size = new Size(lbl_percentage.Location.X - padding - (lbl_letter.Location.X + defaultSize + padding), defaultSize - 2 * padding),
					TabIndex = 4,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Panel bar_fill = new Panel()
				{
					BackColor = ColorTranslator.FromHtml($"{colour.r}, {colour.g}, {colour.b}"),
					Location = new Point(bar_base.Location.X, bar_base.Location.Y),
					Name = "panel_fill",
					Size = new Size(((int)(accuracy * bar_base.Size.Width)), bar_base.Size.Height),
					TabIndex = 5,
					BorderStyle = BorderStyle.FixedSingle,
				};

				Panel panel_char = new Panel()
				{
					BackColor = SystemColors.ControlDark,
					Location = new Point(X, y),
					Name = "panel_char",
					Size = new Size(panelX, panelY),
					TabIndex = 0,
					BorderStyle = BorderStyle.FixedSingle,
				};

				panel_char.Controls.Add(bar_fill);
				panel_char.Controls.Add(bar_base);
				panel_char.Controls.Add(lbl_percentage);
				panel_char.Controls.Add(lbl_time);
				panel_char.Controls.Add(lbl_correct);
				panel_char.Controls.Add(lbl_letter);

				bar_fill.BringToFront();

				panel_stats.Controls.Add(panel_char);

				y += panelY + 2 * padding;
			}

			game.main.panel_main.Controls.Add(panel_stats);
		}

	}
}
