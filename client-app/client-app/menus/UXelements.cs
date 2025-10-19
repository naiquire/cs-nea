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
	public abstract class UXelements : Form
	{
		public static int clientX = Screen.PrimaryScreen.WorkingArea.Width;
		public static int clientY = Screen.PrimaryScreen.WorkingArea.Height;

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
			main.btn_close.Click += (sender, e) => main.Close();
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
				Enabled = false,
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
				Enabled = false,
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

		public static void configResultsPanel(abstractGame game, char c, gameStats stats)
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

			(int r, int g, int b) = ((int)(255 * (1 - accuracy)), (int)(255 * accuracy), 0);

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
			Guna2GradientButton btn_edit = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				Location = new Point(140, 850),
				Name = "btn_profile",
				Size = new Size(220, 50),
				TabIndex = 3,
				Text = "Edit",
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
			btn_edit.Click += async (sender, e) =>
			{
				var update = new update(main.userData);
				if (update.DialogResult == DialogResult.OK)
				{
					if (!await main.connection.InvokeAsync<bool>("updateUserData", main.userData.userID, update.getAboutMe(), update.getLocalisation()))
					{
						new alert("Failed to update profile. Please try again.");
					}
				}
			};

			main.panel_right.Controls.Add(lbl_userID);
			main.panel_right.Controls.Add(lbl_aboutMe);
			main.panel_right.Controls.Add(btn_profile);
			main.panel_right.Controls.Add(btn_edit);
			main.panel_right.Controls.Add(pic_account);

			configStatsPanel(main.panel_right, (40, 570), userData);
		}
		public static void configStatsPanel(Panel panel, (int X, int Y) pos, userData user)
		{
			(string rank, string total, string accuracy) = main.calculateStatsOverview(user);

			Guna2Panel createPanel((int X, int Y) p)
			{
				return new Guna2Panel()
				{
                    BackColor = Color.Transparent,
                    BorderRadius = 20,
                    FillColor = Color.FromArgb(208, 208, 208),
                    Location = new Point(p.X, p.Y),
                    Size = new Size(380, 50),
                    TabIndex = 0,
                };
			}
			Guna2Shapes createCircle((int X, int Y) p)
			{
				return new Guna2Shapes()
				{
                    BackColor = Color.Transparent,
                    BorderColor = Color.White,
                    BorderThickness = 5,
                    FillColor = Color.Transparent,
                    Location = new Point(31, 80),
                    PolygonSides = 3,
                    PolygonSkip = 1,
                    Rotate = 9F,
                    Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
                    Size = new Size(70, 70),
                    TabIndex = 1,
                    UseTransparentBackground = true,
                    Zoom = 80,
                };
			}
			Guna2PictureBox createPicture((int X, int Y) p, (int X, int Y) s, Bitmap image)
			{
				return new Guna2PictureBox()
				{
                    BackColor = Color.Transparent,
                    FillColor = Color.Transparent,
                    Image = image,
                    ImageRotate = 0F,
                    Location = new Point(p.X, p.Y),
                    Size = new Size(s.X, s.Y),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    TabStop = false,
                    UseTransparentBackground = true,
				};
			}
			Guna2TextBox createLabel((int X, int Y) p, (int X, int Y) s, string text)
			{
				return new Guna2TextBox()
				{
                    BorderThickness = 0,
                    Cursor = Cursors.Arrow,
                    DefaultText = text,
                    FillColor = Color.FromArgb(208, 208, 208),
                    Font = new Font("Bahnschrift SemiBold", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(52, 52, 52),
                    Location = new Point(p.X, p.Y),
                    //Margin = new Padding(3, 4, 3, 4),
                    ReadOnly = true,
                    Size = new Size(s.X, s.Y),
					TabStop = false,
                };
			}
			Guna2TextBox createTxt(string text)
			{
				return new Guna2TextBox()
				{
                    BorderThickness = 0,
                    Cursor = Cursors.Arrow,
                    DefaultText = text,
                    FillColor = Color.FromArgb(208, 208, 208),
                    Font = new Font("Bahnschrift", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(104, 104, 104),
                    Location = new Point(280, 10),
                    //Margin = new Padding(3, 4, 3, 4),
                    ReadOnly = true,
                    RightToLeft = RightToLeft.Yes,
                    Size = new Size(80, 30),
                    TabStop = false,
                    TextOffset = new Point(0, -1),
                };
			}
            Guna2Separator createSeperator((int X, int Y) p, (int X, int Y) s)
            {
                return new Guna2Separator()
                {
                    FillColor = Color.FromArgb(247, 113, 163),
                    FillThickness = 2,
                    Location = new Point(p.X, p.Y),
                    Name = "seperator_rank",
                    Size = new Size(s.X, s.Y),
                    TabStop = false,
                };
            }

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

            Guna2Panel panel_rank = createPanel((20, 20));
            Guna2TextBox lbl_rank = createLabel((90, 10), (48, 30), "ELO");
            Guna2Shapes circle_rank = createCircle((31, 10));
            Guna2PictureBox pic_rank = createPicture((30, 10), (30, 30), Resources.rank);
            Guna2TextBox txt_rank = createTxt(rank);
            Guna2Separator seperator_rank = createSeperator((144, 20), (150, 10));

            Guna2Panel panel_accuracy = createPanel((20, 160));
            Guna2TextBox lbl_accuracy = createLabel((90, 10), (93, 30), "ACCURACY");
            Guna2TextBox txt_accuracy = createTxt($"{accuracy}%");
            Guna2Shapes circle_accuracy = createCircle((31, 150));
            Guna2PictureBox pic_accuracy = createPicture((27, 7), (36, 36), Resources.accuracy);
            Guna2Separator seperator_accuracy = createSeperator((189, 20), (86, 10));

            Guna2Panel panel_total = createPanel((20, 90));
            Guna2TextBox lbl_total = createLabel((90, 10), (62, 30), "TOTAL");
            Guna2TextBox txt_total = createTxt(total);
            Guna2Shapes circle_total = createCircle((31, 80));
            Guna2PictureBox pic_total = createPicture((30, 10), (30, 30), Resources.total);
            Guna2Separator seperator_total = createSeperator((158, 20), (136, 10));

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
			//Label lbl_header = new Label
			//{
			//	BackColor = main.panel_main.BackColor,
			//	Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
			//	Location = new Point(10, 10),
			//	Name = "lbl_header",
			//	Size = new Size(300, 30),
			//	TabIndex = 0,
			//	Text = "Lobby",
			//};

			//main.panel_main.Controls.Add(lbl_header);
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
				Size = new Size(260, 70),
				TabStop = false,
				TextAlignment = ContentAlignment.TopCenter,
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

		public static void configVersusResults(Panel panel_main, string winner)
		{

		}
		public static void configKnockoutResults(Panel panel_main)
		{

		}

		public static void configEndGamePanel(abstractGame game, List<char> letters, gameStats statistics)
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

				// update with better UI or something idk

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
