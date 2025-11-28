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

		public static void InitializeComponent(Main main)
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
			main.panel_topBorder.Size = new Size(1920, 30);
			// 
			// lbl_appName
			// 
			main.lbl_appName.BackColor = main.panel_topBorder.BackColor;
			main.lbl_appName.Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
			main.lbl_appName.Location = new Point(10, 7);
			main.lbl_appName.Name = "lbl_appName";
			main.lbl_appName.Size = new Size(100, 16);
			main.lbl_appName.Text = "v1.0.0a";
			// 
			// btn_close
			// 
			main.btn_close.Location = new Point(1890, 0);
			main.btn_close.Size = new Size(30, 30);
			main.btn_close.TabStop = false;
			main.btn_close.Text = "X";
			main.btn_close.UseVisualStyleBackColor = true;
			main.btn_close.Click += (sender, e) => main.btn_close_Click(sender, e);
			// 
			// btn_home
			// 
			main.btn_home.Location = new Point(50, 880);
			main.btn_home.Size = new Size(200, 30);
			main.btn_home.TabStop = false;
			main.btn_home.Text = languages.localisation["Home"][Main.userData.localisation];
			main.btn_home.UseVisualStyleBackColor = true;
			// 
			// panel_left
			// 
			main.panel_left.AutoScroll = true;
			main.panel_left.BackColor = Color.FromArgb(35, 31, 32);
			main.panel_left.Controls.Add(main.btn_home);
			main.panel_left.Location = new Point(0, 130);
			main.panel_left.Size = new Size(300, 950);
			// 
			// panel_topLeft
			// 
			main.panel_topLeft.BackColor = Color.FromArgb(46, 46, 46);
			main.panel_topLeft.Location = new Point(0, 30);
			main.panel_topLeft.Size = new Size(300, 100);
			// 
			// panel_main
			// 
			main.panel_main.BackColor = Color.FromArgb(104, 104, 104);
			main.panel_main.Location = new Point(300, 30);
			main.panel_main.Size = new Size(1120, clientY - 30);
			// 
			// panel_right
			// 
			main.panel_right.BackColor = Color.FromArgb(35, 31, 32);
			main.panel_right.Location = new Point(1420, 30);
			main.panel_right.Size = new Size(500, 1050);
			// 
			// abstractMenu
			// 
			main.AutoScroll = false;
			main.BackColor = Color.White;
			main.ClientSize = new Size(clientX, clientY);
			main.Controls.Add(main.panel_topLeft);
			main.Controls.Add(main.panel_topBorder);
			main.Controls.Add(main.panel_left);
			main.Controls.Add(main.panel_main);
			main.Controls.Add(main.panel_right);
			main.StartPosition = FormStartPosition.Manual;
			main.Location = new Point(0, 0);
			main.FormBorderStyle = FormBorderStyle.None;
			main.panel_topBorder.ResumeLayout(false);
			main.panel_left.ResumeLayout(false);
			main.panel_main.ResumeLayout(false);
			main.ResumeLayout(false);

		}

		public static void ResetLayout(Main main)
		{
			main.panel_main?.Controls.Clear();
			main.panel_left?.Controls.Clear();
			main.panel_right?.Controls.Clear();
			main.btn_home.Click -= main.btn_home_Click;

			main.panel_left.Controls.Add(main.btn_home);
			main.btn_home.Click += main.btn_home_Click;
		}

		public static input ConfigGamePanel(Game game)
		{
			game.main.panel_main.Controls.Clear();

			game.panel_outline = new Guna2Shapes()
			{
				BorderColor = Color.FromArgb(52, 52, 52),
				BorderThickness = 10,
				FillColor = Color.White,
				Location = new Point(260, 250),
				PolygonSkip = 1,
				Rotate = 0F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Rounded,
				Size = new Size(600, 600),
				TabStop = false,
				Zoom = 100,
			};
			game.lbl_letter = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Calibri", 144F),
				ForeColor = Color.Black,
				Location = new Point(380, 50),
				Size = new Size(360, 150),
				TabStop = false,
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
				Size = new Size(440, 100),
				TabStop = false,
				Text = languages.localisation["Submit"][Main.userData.localisation],
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
				Size = new Size(225, 100),
				TabStop = false,
				Text = languages.localisation["Clear"][Main.userData.localisation],
			};

			var input = new input(game.main.panel_main, (260, 250), (600, 600));

			game.main.panel_main.Controls.Add(game.panel_outline);
			game.main.panel_main.Controls.Add(game.lbl_letter);
			game.main.panel_main.Controls.Add(game.btn_submit);
			game.main.panel_main.Controls.Add(game.btn_clear);
			return input;
		}

		public static void configRightGamePanel(Game game)
		{
			game.lbl_rounds = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(35, 31, 32),
				Cursor = Cursors.Arrow,
				Font = new Font("Bahnschrift SemiBold", 48F, FontStyle.Bold),
				Location = new Point(40, 40),
				Size = new Size(420, 80),
				TabStop = false,
				Text = $"{languages.localisation["Round"][Main.userData.localisation]} {game.GetRounds()}",
				TextAlignment = ContentAlignment.MiddleCenter,
			};
			PictureBox seperator = new PictureBox()
			{
				Image = Resources.seperator,
				Location = new Point(30, 120),
				Size = new Size(440, 7),
				SizeMode = PictureBoxSizeMode.StretchImage,
				TabStop = false,
			};
			game.panel_stats = new Guna2Panel()
			{
				BorderRadius = 20,
				FillColor = Color.FromArgb(104, 104, 104),
				Location = new Point(40, 160),
				Size = new Size(420, 720),
			};

			game.main.panel_right.Controls.Add(game.lbl_rounds);
			game.main.panel_right.Controls.Add(seperator);
			game.main.panel_right.Controls.Add(game.panel_stats);
		}
		public static void ConfigRightGamePanelStats(Panel panel, List<char> letters, List<double> accuracies)
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
					Size = new Size(420 - 2 * padding, panelY),
				};

				Label lbl_letter = new Label()
				{
					BackColor = Color.FromArgb(156, 156, 156),
					Font = new Font("Bahnschrift SemiBold", 27.75F, FontStyle.Bold),
					Location = new Point(10, 2),
					Size = new Size(43, 45),
					Text = letter.ToString(),
				};
				Panel bar_base = new Panel()
				{
					BackColor = Color.White,
					Location = new Point(60, 10),
					Size = new Size(230, 30),
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
					Size = new Size(80, 30),
					TabStop = false,
					TextAlign = HorizontalAlignment.Right,
				};
				Guna2Panel bar_fill = new Guna2Panel()
				{
					BackColor = ColorTranslator.FromHtml($"{r}, {g}, {b}"),
					Location = new Point(60, 10),
					Size = new Size((int)(accuracy * bar_base.Size.Width), bar_base.Size.Height),
				};

				panel_stat.Controls.Add(bar_fill);
				panel_stat.Controls.Add(lbl_accuracy);
				panel_stat.Controls.Add(bar_base);
				panel_stat.Controls.Add(lbl_letter);

				return panel_stat;
			}

		}

		public static void configLeftGamePanel(Game game, List<friendData> users)
		{
			game.main.panel_left.Controls.Clear(); // will clear countdown of its happening

			Panel panel_players = new Panel()
			{
				AutoScroll = true,
				BackColor = Color.FromArgb(46, 46, 46),
				Location = new Point(20, 90),
				Name = "panel_friendList",
				Size = new Size(260, 384),
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
		public static void configLeftGamePanel(Game game, List<friendData> alive, List<friendData> dead)
		{
			game.main.panel_left.Controls.Clear();

			Panel panel_players = new Panel()
			{
				AutoScroll = true,
				BackColor = Color.FromArgb(46, 46, 46),
				Location = new Point(20, 90),
				Name = "panel_friendList",
				Size = new Size(260, 384),
			};
			PictureBox seperator = new PictureBox()
			{
				Image = Resources.seperator,
				InitialImage = null,
				Location = new Point(50, 60),
				Name = "seperator",
				Size = new Size(200, 5),
				SizeMode = PictureBoxSizeMode.StretchImage,
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
				TabStop = false,
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
				TabStop = false,
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
					TabStop = false,
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
				TabStop = false,
				Text = languages.localisation["Eliminated"][Main.userData.localisation],
			};
			panel_players.Controls.Add(lbl_dead);

			Label offlineCount = new Label()
			{
				BackColor = panel_players.BackColor,
				BorderStyle = BorderStyle.None,
				Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				Location = new Point(230, y_offset),
				Size = new Size(30, 20),
				TabStop = false,
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
					TabStop = false,
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

		public static Guna2Panel ConfigResultsPanel(Game game, char c, gameStats stats)
		{
			game.main.panel_main.Controls.Clear();

			string letter = c.ToString();
			bool correct = stats.correct.Last();
			double accuracy = stats.accuracy.Last();
			TimeSpan time = stats.time.Last();

			double accuracyDelta = accuracy - Main.userData.statistics[c].accuracy;
			string accuracyDeltaText = $"{(accuracyDelta < 0 ? "" : "+")}{Math.Round(accuracyDelta * 100, 2)}";
			Color accuracyColour = accuracyDelta < 0 ? Color.Red : Color.Lime;

			double timeDelta = time.TotalSeconds - Main.userData.statistics[c].time.TotalSeconds;
			string timeDeltaText = $"{(timeDelta < 0 ? "" : "+")}{Math.Round(timeDelta, 2)}";
			Color timeColour = timeDelta < 0 ? Color.Lime : Color.Red;

			Guna2GradientButton btn_continue = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 49,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift SemiBold", 31.75F, FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(220, 900),
				Size = new Size(680, 100),
				TabStop = false,
				Text = languages.localisation["Continue"][Main.userData.localisation],
			};
			Guna2Panel panel_results = new Guna2Panel()
			{
				BorderRadius = 30,
				FillColor = Color.FromArgb(62, 55, 55),
				Location = new Point(110, 200),
				Size = new Size(900, 500),
				TabStop = false,
			};
			Guna2TextBox txt_timeDiff = new Guna2TextBox()
			{
				BackColor = Color.Transparent,
				BorderRadius = 20,
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				FillColor = Color.FromArgb(82, 65, 65),
				Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = timeColour,
				Location = new Point(740, 270),
				Text = timeDeltaText,
				ReadOnly = true,
				Size = new Size(140, 40),
				TabStop = false,
				TextAlign = HorizontalAlignment.Center,
				TextOffset = new Point(0, -1),
			};
			Guna2HtmlLabel lbl_time = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(40, 270),
				Size = new Size(160, 40),
				TabStop = false,
				Text = languages.localisation["Time"][Main.userData.localisation],
				TextAlignment = ContentAlignment.TopRight,
			};
			Guna2ProgressBar bar_time = new Guna2ProgressBar()
			{
				BackColor = Color.Transparent,
				BorderRadius = 15,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 14.75F),
				ForeColor = Color.Black,
				Location = new Point(220, 275),
				ProgressColor = Color.White,
				ProgressColor2 = Color.FromArgb(208, 208, 208),
				RightToLeft = RightToLeft.No,
				ShowText = true,
				Size = new Size(500, 30),
				Style = ProgressBarStyle.Continuous,
				TabStop = false,
				Text = time.ToString(@"mm\:ss\:ff"),
				TextMode = Guna.UI2.WinForms.Enums.ProgressBarTextMode.Custom,
				TextOffset = new Point(0, 2),
				TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault,
				UseTransparentBackground = true,
				Value = 100,
			};
			Guna2TextBox txt_accuracyDiff = new Guna2TextBox()
			{
				BackColor = Color.Transparent,
				BorderRadius = 20,
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				FillColor = Color.FromArgb(82, 65, 65),
				Font = new Font("Bahnschrift", 18F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = accuracyColour,
				Location = new Point(740, 220),
				Text = accuracyDeltaText,
				ReadOnly = true,
				Size = new Size(140, 40),
				TabStop = false,
				TextAlign = HorizontalAlignment.Center,
				TextOffset = new Point(0, -1),
			};
			Guna2HtmlLabel lbl_accuracy = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(40, 220),
				Size = new Size(160, 40),
				TabStop = false,
				Text = languages.localisation["Accuracy"][Main.userData.localisation],
				TextAlignment = ContentAlignment.TopRight,
			};
			Guna2ProgressBar bar_accuracy = new Guna2ProgressBar()
			{
				BackColor = Color.Transparent,
				BorderRadius = 15,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 14.75F),
				ForeColor = Color.Black,
				Location = new Point(220, 225),
				ProgressColor = Color.PaleGreen,
				ProgressColor2 = Color.SpringGreen,
				RightToLeft = RightToLeft.No,
				ShowText = true,
				Size = new Size(500, 30),
				Style = ProgressBarStyle.Continuous,
				TabStop = false,
				TextAlign = HorizontalAlignment.Right,
				TextOffset = new Point(0, 2),
				TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault,
				UseTransparentBackground = true,
				Value = (int)(accuracy * 100),
			};
			Guna2HtmlLabel lbl_letter = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift SemiBold", 96F, FontStyle.Bold),
				ForeColor = Color.Black,
				Location = new Point(60, 63),
				Size = new Size(120, 120),
				TabStop = false,
				Text = letter,
				TextAlignment = ContentAlignment.MiddleCenter,
			};
			Guna2Shapes shape_letterOutline = new Guna2Shapes()
			{
				BackColor = Color.Transparent,
				BorderColor = Color.FromArgb(247, 113, 163),
				BorderThickness = 5,
				FillColor = Color.FromArgb(208, 208, 208),
				Location = new Point(40, 40),
				PolygonSides = 4,
				PolygonSkip = 1,
				Rotate = 0F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Rounded,
				Size = new Size(160, 160),
				TabStop = false,
				UseTransparentBackground = true,
				Zoom = 100,
			};
			Guna2PictureBox seperator = new Guna2PictureBox()
			{
				Image = Resources.seperator,
				ImageRotate = 0F,
				Location = new Point(50, 160),
				Margin = new Padding(0),
				Size = new Size(1020, 10),
				SizeMode = PictureBoxSizeMode.StretchImage,
				TabStop = false,
			};
			Guna2HtmlLabel lbl_results = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 72F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(50, 20),
				Size = new Size(1020, 120),
				TabStop = false,
				Text = languages.localisation["Results"][Main.userData.localisation],
				TextAlignment = ContentAlignment.MiddleCenter,
			};
			Guna2HtmlLabel lbl_diff = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 14.25F),
				ForeColor = Color.White,
				Location = new Point(740, 170),
				Size = new Size(140, 40),
				TabStop = false,
				Text = languages.localisation["Delta"][Main.userData.localisation],
				TextAlignment = ContentAlignment.BottomCenter,
			};
			Guna2HtmlLabel lbl_correct = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 40.25F),
				ForeColor = Color.White,
				Location = new Point(220, 40),
				Size = new Size(330, 70),
				TabStop = false,
				Text = correct ? languages.localisation["Correct"][Main.userData.localisation] : languages.localisation["Incorrect"][Main.userData.localisation],
			};

			panel_results.Controls.Add(lbl_correct);
			panel_results.Controls.Add(lbl_diff);
			panel_results.Controls.Add(txt_timeDiff);
			panel_results.Controls.Add(lbl_time);
			panel_results.Controls.Add(bar_time);
			panel_results.Controls.Add(txt_accuracyDiff);
			panel_results.Controls.Add(lbl_accuracy);
			panel_results.Controls.Add(bar_accuracy);
			panel_results.Controls.Add(lbl_letter);
			panel_results.Controls.Add(shape_letterOutline);

			btn_continue.Click += async (sender, e) =>
			{
				if (Main.connection.State != HubConnectionState.Connected)
				{
					game.main.btn_home.PerformClick();
				}

				btn_continue.Enabled = false;

				await Main.connection.InvokeAsync("requestRound", game.GetGameID(), Main.userData.userID);
			};

			game.main.panel_main.Controls.Add(lbl_results);
			game.main.panel_main.Controls.Add(seperator);
			game.main.panel_main.Controls.Add(panel_results);
			game.main.panel_main.Controls.Add(btn_continue);

			return panel_results;
		}

		public static void configUserDataPanel(Main main, userData userData)
		{
			main.panel_right.Controls.Clear();

			Guna2PictureBox pic_account = new Guna2PictureBox()
			{
				Image = Resources.account,
				ImageRotate = 0F,
				Location = new Point(150, 50),
				Size = new Size(200, 200),
				SizeMode = PictureBoxSizeMode.Zoom,
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
				Size = new Size(220, 50),
				TabStop = false,
				Text = languages.localisation["Profile"][Main.userData.localisation],
			};
			Guna2GradientButton btn_edit = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				Location = new Point(140, 850),
				Size = new Size(220, 50),
				TabStop = false,
				Text = languages.localisation["Edit"][Main.userData.localisation],
			};
			Guna2TextBox lbl_userID = new Guna2TextBox()
			{
				BackColor = Color.FromArgb(44, 39, 41),
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = userData.userID,
				BorderColor = Color.FromArgb(208, 208, 208),
				FillColor = Color.FromArgb(44, 39, 41),
				Font = new Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(40, 265),
				ReadOnly = true,
				Size = new Size(420, 70),
				TabStop = false,
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
				ReadOnly = true,
				Size = new Size(420, 180),
				TabStop = false,
			};

			btn_profile.Click += (sender, e) =>
			{
				menu.profile = new Profile(main, userData);
			};
			btn_edit.Click += async (sender, e) =>
			{
				var update = new update(Main.userData);
				if (update.DialogResult == DialogResult.OK)
				{
					if (Main.connection.State != HubConnectionState.Connected)
					{
						Main.LoadAlert(languages.localisation["An error occurred. Please wait and try again"][Main.userData.localisation]);
						return;
					}
					if (!await Main.connection.InvokeAsync<bool>("updateUserData", Main.userData.userID, update.getAboutMe(), update.getLocalisation()))
					{
						Main.LoadAlert(languages.localisation["An error occurred. Please wait and try again"][Main.userData.localisation]);
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
			(string rank, string total, string accuracy) = Main.CalculateStatsOverview(user);

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
				Size = new Size(420, 230),
				TabIndex = 0,
			};
			Guna2Shapes line_stats = new Guna2Shapes()
			{
				BorderThickness = 0,
				FillColor = Color.White,
				LineThickness = 1,
				Location = new Point(63, 1),
				PolygonSkip = 1,
				Rotate = 0F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle,
				Size = new Size(5, 228),
				TabIndex = 2,
				Zoom = 100,
			};

			Guna2Panel panel_rank = createPanel((20, 20));
			Guna2TextBox lbl_rank = createLabel((90, 10), (48, 30), "Elo");
			Guna2Shapes circle_rank = createCircle((31, 10));
			Guna2PictureBox pic_rank = createPicture((30, 10), (30, 30), Resources.rank);
			Guna2TextBox txt_rank = createTxt(rank);
			Guna2Separator seperator_rank = createSeperator((144, 20), (150, 10));

			Guna2Panel panel_accuracy = createPanel((20, 160));
			Guna2TextBox lbl_accuracy = createLabel((90, 10), (93, 30), languages.localisation["Accuracy"][Main.userData.localisation]);
			Guna2TextBox txt_accuracy = createTxt($"{accuracy}%");
			Guna2Shapes circle_accuracy = createCircle((31, 150));
			Guna2PictureBox pic_accuracy = createPicture((27, 7), (36, 36), Resources.accuracy);
			Guna2Separator seperator_accuracy = createSeperator((189, 20), (86, 10));

			Guna2Panel panel_total = createPanel((20, 90));
			Guna2TextBox lbl_total = createLabel((90, 10), (62, 30), languages.localisation["Total"][Main.userData.localisation]);
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

		public static void configLobbyPanel(Game game, List<friendData> users)
		{
			game.main.panel_main.Controls.Clear();

			Panel panel_users = new Panel()
			{
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
				Text = $"{users.Count}/{game.GetMaxPlayers()} {languages.localisation["players"][Main.userData.localisation]}",
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
					TabStop = false,
					Text = user.userID,
					BorderStyle = BorderStyle.FixedSingle,
					TextAlign = ContentAlignment.MiddleLeft,
				};
				Label rank = new Label()
				{
					BackColor = game.main.panel_main.BackColor,
					Font = new Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
					Location = new Point(userID.Width + 2 * padding, padding),
					Name = "rank",
					Size = new Size(userY - padding, userY - 2 * padding),
					TabStop = false,
					Text = user.rank.ToString(),
					TextAlign = ContentAlignment.MiddleCenter,
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

		private static void configCountdown(Game game)
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
		public static async Task Countdown(Guna2HtmlLabel lbl_countdown, int num, Guna2HtmlLabel lbl_status, string text)
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

		public static void configVersusResults(Panel panel_results, string winner)
		{
			Guna2HtmlLabel lbl_winner = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(40, 360),
				Size = new Size(160, 40),
				TabStop = false,
				Text = languages.localisation["Winner"][Main.userData.localisation],
				TextAlignment = ContentAlignment.TopRight,
			};
			Guna2ProgressBar bar_winner = new Guna2ProgressBar()
			{
				BackColor = Color.Transparent,
				BorderRadius = 15,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 14.75F),
				ForeColor = Color.Black,
				Location = new Point(220, 365),
				ProgressColor = Color.White,
				ProgressColor2 = Color.FromArgb(208, 208, 208),
				RightToLeft = RightToLeft.No,
				ShowText = true,
				Size = new Size(200, 30),
				Style = ProgressBarStyle.Continuous,
				TabStop = false,
				Text = winner,
				TextMode = Guna.UI2.WinForms.Enums.ProgressBarTextMode.Custom,
				TextOffset = new Point(0, 2),
				TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault,
				UseTransparentBackground = true,
				Value = 100,
			};

			panel_results.Controls.Add(lbl_winner);
			panel_results.Controls.Add(bar_winner);
		}
		public static void configKnockoutResults(Panel panel_results, bool eliminated, bool correct)
		{
			Guna2HtmlLabel lbl_eliminated = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 30.25F),
				ForeColor = Color.LightCoral,
				Location = new Point(550, 40),
				Size = new Size(330, 70),
				TabStop = false,
				Text = eliminated ? languages.localisation["Eliminated"][Main.userData.localisation] : languages.localisation["Passed"][Main.userData.localisation],
				TextAlignment = ContentAlignment.TopRight,
			};
			Guna2HtmlLabel lbl_eliminateReason = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift", 16.25F),
				ForeColor = Color.MistyRose,
				Location = new Point(550, 90),
				Size = new Size(330, 60),
				TabStop = false,
				Text = correct ? languages.localisation["by longest time elapsed"][Main.userData.localisation] : languages.localisation["by incorrect submission"][Main.userData.localisation],
				TextAlignment = ContentAlignment.TopRight,
			};


			panel_results.Controls.Add(lbl_eliminated);
			if (eliminated)
			{
				panel_results.Controls.Add(lbl_eliminateReason);
			}
			lbl_eliminateReason.BringToFront();
		}

		public static void ConfigEndGamePanel(Game game, List<char> letters, gameStats statistics)
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
				Size = new Size(1040, 500),
			};

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
					Size = new Size(defaultSize, defaultSize),
					Text = letter,
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_correct = new Label()
				{
					Location = new Point(panelX - 2 * defaultSize - padding, padding),
					Size = new Size(2 * defaultSize, defaultSize),
					Text = correct.ToString(),
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_time = new Label()
				{
					Location = new Point(lbl_correct.Location.X - 2 * defaultSize - padding, padding),
					Size = new Size(2 * defaultSize, defaultSize),
					Text = $"{time.TotalSeconds}",
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Label lbl_percentage = new Label()
				{
					Location = new Point(lbl_time.Location.X - defaultSize - padding, padding),
					Size = new Size(defaultSize, defaultSize),
					Text = $"{100 * accuracy}%",
					TextAlign = ContentAlignment.MiddleCenter,
					BorderStyle = BorderStyle.FixedSingle,
				};
				Panel bar_base = new Panel()
				{
					BackColor = SystemColors.ControlLight,
					Location = new Point(lbl_letter.Location.X + defaultSize + padding, 2 * padding),
					Size = new Size(lbl_percentage.Location.X - padding - (lbl_letter.Location.X + defaultSize + padding), defaultSize - 2 * padding),
					BorderStyle = BorderStyle.FixedSingle,
				};
				Panel bar_fill = new Panel()
				{
					BackColor = ColorTranslator.FromHtml($"{colour.r}, {colour.g}, {colour.b}"),
					Location = new Point(bar_base.Location.X, bar_base.Location.Y),
					Size = new Size(((int)(accuracy * bar_base.Size.Width)), bar_base.Size.Height),
					BorderStyle = BorderStyle.FixedSingle,
				};

				Panel panel_char = new Panel()
				{
					BackColor = SystemColors.ControlDark,
					Location = new Point(X, y),
					Size = new Size(panelX, panelY),
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

			panel_stats.VerticalScroll.Enabled = true;
			game.main.panel_main.Controls.Add(panel_stats);
		}

		public static void configVersusEndgame(Panel panel, int change)
		{
			Guna2HtmlLabel txt_rank = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.White,
				Font = new Font("Bahnschrift", 16.25F),
				ForeColor = Color.Black,
				Location = new Point(10, 10),
				Size = new Size(330, 60),
				TabStop = false,
				Text = $"{Main.userData.rank} | {(change < 0 ? "" : "+")}{change}",
				TextAlignment = ContentAlignment.MiddleLeft,
			};

			panel.Controls.Add(txt_rank);
		}
		public static void configKnockoutEndgame(Panel panel, bool win)
		{
			Guna2HtmlLabel txt_winner = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.White,
				Font = new Font("Bahnschrift", 16.25F),
				ForeColor = Color.Black,
				Location = new Point(100, 900),
				Size = new Size(330, 60),
				TabStop = false,
				Text = $"You {(win ? "won" : "did not win")}",
				TextAlignment = ContentAlignment.MiddleLeft,
			};

			panel.Controls.Add(txt_winner);
		}
	}
}
