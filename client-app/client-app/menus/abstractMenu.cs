using client_app.components;
using client_app.menus.games;
using client_app.Properties;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace client_app.menus
{
	public abstract class abstractMenu : Form // can be made static in final build, maybe rename
	{
		
		private Guna.UI2.WinForms.Guna2TextBox lbl_timer;
		

		/// <summary>
		/// Used to create control elements with the visual designer.
		/// </summary>
		public void tempInitializeComponent()
		{
			this.lbl_timer = new Guna.UI2.WinForms.Guna2TextBox();
			this.SuspendLayout();
			// 
			// lbl_timer
			// 
			this.lbl_timer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
			this.lbl_timer.BorderRadius = 10;
			this.lbl_timer.BorderThickness = 4;
			this.lbl_timer.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.lbl_timer.DefaultText = "00:00.0";
			this.lbl_timer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.lbl_timer.Font = new System.Drawing.Font("Bahnschrift SemiBold", 39.75F, System.Drawing.FontStyle.Bold);
			this.lbl_timer.ForeColor = System.Drawing.Color.White;
			this.lbl_timer.Location = new System.Drawing.Point(310, 900);
			this.lbl_timer.Margin = new System.Windows.Forms.Padding(5);
			this.lbl_timer.Multiline = true;
			this.lbl_timer.Name = "lbl_timer";
			this.lbl_timer.PlaceholderText = "";
			this.lbl_timer.ReadOnly = true;
			this.lbl_timer.SelectedText = "";
			this.lbl_timer.Size = new System.Drawing.Size(500, 100);
			this.lbl_timer.TabIndex = 0;
			this.lbl_timer.TabStop = false;
			this.lbl_timer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lbl_timer.TextOffset = new System.Drawing.Point(0, 4);
			// 
			// panel_outline
			// 
			
			// 
			// lbl_letter
			// 
			
			// 
			// btn_submit
			// 
			
			// 
			// btn_clearDrawing
			// 
			
			// 
			// abstractMenu
			// 


		}

		/// <summary>
		/// Initialises the base UI when the client application is launched.
		/// </summary>
		public static void InitializeComponent(main main)
		{
			// OPENING DESIGNER WILL BREAK THIS MODULE

			main.Controls.Clear();

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
			main.panel_main.SuspendLayout();
			main.SuspendLayout();
			// 
			// panel_topBorder
			// 
			main.panel_topBorder.BackColor = Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
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
			main.lbl_appName.Font = new Font("Bahnschrift SemiBold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
			main.lbl_appName.Location = new Point(10, 7);
			main.lbl_appName.Name = "lbl_appName";
			main.lbl_appName.Size = new Size(100, 16);
			main.lbl_appName.TabIndex = 0;
			main.lbl_appName.Text = "appName";
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
			main.btn_home.Location = new Point(50, 910);
			main.btn_home.Name = "btn_home";
			main.btn_home.Size = new Size(200, 30);
			main.btn_home.TabIndex = 0;
			main.btn_home.Text = "HOME";
			main.btn_home.UseVisualStyleBackColor = true;
			// 
			// panel_left
			// 
			main.panel_left.AutoScroll = true;
			main.panel_left.BackColor = Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			main.panel_left.Controls.Add(main.btn_home);
			main.panel_left.Location = new Point(0, 130);
			main.panel_left.Name = "panel_left";
			main.panel_left.Size = new Size(300, 950);
			main.panel_left.TabIndex = 2;
			// 
			// panel_topLeft
			// 
			main.panel_topLeft.BackColor = Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
			main.panel_topLeft.Location = new Point(0, 30);
			main.panel_topLeft.Name = "panel_topLeft";
			main.panel_topLeft.Size = new Size(300, 100);
			main.panel_topLeft.TabIndex = 1;
			// 
			// panel_main
			// 
			main.panel_main.BackColor = Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104))))); ;
			main.panel_main.Location = new Point(300, 30);
			main.panel_main.Name = "panel_main";
			main.panel_main.Size = new Size(1120, 1050);
			main.panel_main.TabIndex = 4;
			// 
			// panel_right
			// 
			main.panel_right.BackColor = Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			main.panel_right.Location = new Point(1420, 30);
			main.panel_right.Name = "panel_right";
			main.panel_right.Size = new Size(500, 1050);
			main.panel_right.TabIndex = 3;
			// 
			// abstractMenu
			// 
			main.BackColor = Color.White;
			main.ClientSize = new Size(1920, 1080);
			main.Controls.Add(main.panel_topLeft);
			main.Controls.Add(main.panel_topBorder);
			main.Controls.Add(main.panel_left);
			main.Controls.Add(main.panel_main);
			main.Controls.Add(main.panel_right);
			main.FormBorderStyle = FormBorderStyle.None;
			main.Name = "abstractMenu";
			main.panel_topBorder.ResumeLayout(false);
			main.panel_left.ResumeLayout(false);
			main.panel_main.ResumeLayout(false);
			main.ResumeLayout(false);

		}

		/// <summary>
		/// Resets the current window to the base UI.
		/// </summary>
		/// <param name="main"></param>
		public static void resetLayout(main main)
		{
			main.panel_main?.Controls.Clear();
			main.panel_left?.Controls.Clear();
			main.panel_right?.Controls.Clear();
			main.btn_home.Click -= main.btn_home_Click;

			main.panel_left.Controls.Add(main.btn_home);
			main.btn_home.Click += main.btn_home_Click;
		}

		/// <summary>
		/// Configures the specified game panel by adding visual elements such as shapes, text, and buttons.
		/// </summary>
		/// <param name="panel"></param>
		/// <returns>An <see cref="input"/> object containing the created drawing panel.</returns>
		public static input configGamePanel(abstractGame game)
		{
			game.panel_outline = new Guna.UI2.WinForms.Guna2Shapes()
			{
				BorderColor = Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52))))),
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
			game.lbl_letter = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				DefaultText = "K",
				FillColor = Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new Font("Calibri", 144F),
				ForeColor = Color.Black,
				Location = new Point(380, 50),
				Margin = new Padding(42, 47, 42, 47),
				Name = "lbl_letter",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new Size(360, 150),
				TabIndex = 2,
				TextAlign = HorizontalAlignment.Center,
			};
			game.btn_submit = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 49,
				FillColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillColor2 = Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247))))),
				Font = new Font("Bahnschrift SemiBold", 31.75F, FontStyle.Bold),
				ForeColor = Color.White,
				Location = new Point(460, 900),
				Name = "btn_submit",
				Size = new Size(440, 100),
				TabIndex = 3,
				Text = "Submit",
			};
			game.btn_clear = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 49,
				FillColor = Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				FillColor2 = Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
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

		/// <summary>
		/// Configures the right panel with userData.
		/// </summary>
		public static void configUserDataPanel(main main, userData userData)
		{
			Guna.UI2.WinForms.Guna2PictureBox pic_account = new Guna.UI2.WinForms.Guna2PictureBox()
			{
				Image = global::client_app.Properties.Resources.account,
				ImageRotate = 0F,
				Location = new System.Drawing.Point(150, 50),
				Name = "pic_account",
				Size = new System.Drawing.Size(200, 200),
				SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
				TabIndex = 0,
				TabStop = false
			};
			Guna.UI2.WinForms.Guna2GradientButton btn_profile = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 24,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold),
				Location = new System.Drawing.Point(140, 960),
				Name = "btn_profile",
				Size = new System.Drawing.Size(220, 50),
				TabIndex = 3,
				Text = "Profile",
			};
			Guna.UI2.WinForms.Guna2TextBox lbl_userID = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41))))),
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.IBeam,
				DefaultText = userData.userID,
				BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41))))),
				Font = new System.Drawing.Font("Bahnschrift", 27.75F, System.Drawing.FontStyle.Bold),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(40, 265),
				Margin = new System.Windows.Forms.Padding(6),
				Name = "lbl_userID",
				PlaceholderForeColor = System.Drawing.Color.Transparent,
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(420, 50),
				TabIndex = 13,
				TextAlign = System.Windows.Forms.HorizontalAlignment.Center,
			};
			Guna.UI2.WinForms.Guna2TextBox lbl_aboutMe = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				BorderRadius = 10,
				BorderThickness = 4,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = userData.aboutMe,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(40, 330),
				Margin = new System.Windows.Forms.Padding(5, 5, 5, 5),
				Multiline = true,
				Name = "header",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(420, 200),
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

		/// <summary>
		/// Configures the panel containing summary statistics for a user at a given position in a Panel.
		/// </summary>
		/// <param name="panel"></param>
		/// <param name="pos"></param>
		/// <param name="user"></param>
		public static void configStatsPanel(Panel panel, (int X, int Y) pos, userData user)
		{
			string rank = user.rank.ToString();

			int sum = 0;
			foreach (var letter in user.statistics.Keys)
			{
				sum += user.statistics[letter].total;
			}
			string total = sum.ToString();

			double mean = 0;
			foreach (var letter in user.statistics.Keys)
			{
				mean += user.statistics[letter].accuracy;
			}
			mean /= user.statistics.Count;
			string accuracy = ((int)(100 * mean)).ToString();

			Guna.UI2.WinForms.Guna2Panel panel_statsOverview = new Guna.UI2.WinForms.Guna2Panel()
			{
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				Location = new System.Drawing.Point(pos.X, pos.Y),
				Name = "panel_statsOverview",
				Size = new System.Drawing.Size(420, 230),
				TabIndex = 0,
			};
			Guna.UI2.WinForms.Guna2Panel panel_rank = new Guna.UI2.WinForms.Guna2Panel()
			{

				BackColor = System.Drawing.Color.Transparent,
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Location = new System.Drawing.Point(20, 20),
				Name = "panel_rank",
				Size = new System.Drawing.Size(380, 50),
				TabIndex = 0,
			};
			Guna.UI2.WinForms.Guna2Shapes circle_total = new Guna.UI2.WinForms.Guna2Shapes()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderColor = System.Drawing.Color.White,
				BorderThickness = 5,
				FillColor = System.Drawing.Color.Transparent,
				Location = new System.Drawing.Point(31, 80),
				Name = "circle_total",
				PolygonSides = 3,
				PolygonSkip = 1,
				Rotate = 9F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
				Size = new System.Drawing.Size(70, 70),
				TabIndex = 1,
				UseTransparentBackground = true,
				Zoom = 80,
			};
			Guna.UI2.WinForms.Guna2Shapes line_stats = new Guna.UI2.WinForms.Guna2Shapes()
			{
				BorderThickness = 0,
				FillColor = System.Drawing.Color.White,
				LineThickness = 1,
				Location = new System.Drawing.Point(63, 0),
				Name = "line_stats",
				PolygonSkip = 1,
				Rotate = 0F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle,
				Size = new System.Drawing.Size(5, 230),
				TabIndex = 2,
				Text = "guna2Shapes2",
				Zoom = 100,
			};
			Guna.UI2.WinForms.Guna2Shapes circle_rank = new Guna.UI2.WinForms.Guna2Shapes()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderColor = System.Drawing.Color.White,
				BorderThickness = 5,
				FillColor = System.Drawing.Color.Transparent,
				Location = new System.Drawing.Point(31, 10),
				Name = "circle_rank",
				PolygonSides = 3,
				PolygonSkip = 1,
				Rotate = 9F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
				Size = new System.Drawing.Size(70, 70),
				TabIndex = 3,
				Text = "guna2Shapes3",
				UseTransparentBackground = true,
				Zoom = 80,
			};
			Guna.UI2.WinForms.Guna2Panel panel_total = new Guna.UI2.WinForms.Guna2Panel()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Location = new System.Drawing.Point(20, 90),
				Name = "panel_total",
				Size = new System.Drawing.Size(380, 50),
				TabIndex = 2,
			};
			Guna.UI2.WinForms.Guna2PictureBox pic_rank = new Guna.UI2.WinForms.Guna2PictureBox()
			{
				BackColor = System.Drawing.Color.Transparent,
				FillColor = System.Drawing.Color.Transparent,
				Image = global::client_app.Properties.Resources.rank,
				ImageRotate = 0F,
				Location = new System.Drawing.Point(30, 10),
				Name = "pic_rank",
				Size = new System.Drawing.Size(30, 30),
				SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
				TabIndex = 4,
				TabStop = false,
				UseTransparentBackground = true,
			};
			Guna.UI2.WinForms.Guna2PictureBox pic_total = new Guna.UI2.WinForms.Guna2PictureBox()
			{
				BackColor = System.Drawing.Color.Transparent,
				FillColor = System.Drawing.Color.Transparent,
				Image = global::client_app.Properties.Resources.total,
				ImageRotate = 0F,
				Location = new System.Drawing.Point(30, 10),
				Name = "pic_total",
				Size = new System.Drawing.Size(30, 30),
				SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
				TabIndex = 5,
				TabStop = false,
				UseTransparentBackground = true,
			};
			Guna.UI2.WinForms.Guna2TextBox lbl_rank = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "ELO",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52))))),
				Location = new System.Drawing.Point(90, 10),
				Margin = new System.Windows.Forms.Padding(3, 4, 3, 4),
				Name = "lbl_rank",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(48, 30),
				TabIndex = 5,
			};
			Guna.UI2.WinForms.Guna2TextBox lbl_total = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "TOTAL",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52))))),
				Location = new System.Drawing.Point(90, 10),
				Margin = new System.Windows.Forms.Padding(3, 4, 3, 4),
				Name = "lbl_total",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(62, 30),
				TabIndex = 6,
			};
			Guna.UI2.WinForms.Guna2TextBox txt_rank = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = rank,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104))))),
				Location = new System.Drawing.Point(300, 10),
				Margin = new System.Windows.Forms.Padding(3, 4, 3, 4),
				Name = "txt_rank",
				PlaceholderText = "",
				ReadOnly = true,
				RightToLeft = System.Windows.Forms.RightToLeft.Yes,
				SelectedText = "",
				Size = new System.Drawing.Size(60, 30),
				TabIndex = 6,
				TextOffset = new System.Drawing.Point(0, -1),
			};
			Guna.UI2.WinForms.Guna2TextBox txt_total = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = total,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104))))),
				Location = new System.Drawing.Point(300, 10),
				Margin = new System.Windows.Forms.Padding(3, 4, 3, 4),
				Name = "txt_total",
				PlaceholderText = "",
				ReadOnly = true,
				RightToLeft = System.Windows.Forms.RightToLeft.Yes,
				SelectedText = "",
				Size = new System.Drawing.Size(60, 30),
				TabIndex = 7,
				TextOffset = new System.Drawing.Point(0, -1),
			};
			Guna.UI2.WinForms.Guna2Separator seperator_rank = new Guna.UI2.WinForms.Guna2Separator()
			{

				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillThickness = 2,
				Location = new System.Drawing.Point(144, 20),
				Name = "seperator_rank",
				Size = new System.Drawing.Size(150, 10),
				TabIndex = 7,
			};
			Guna.UI2.WinForms.Guna2Separator seperator_total = new Guna.UI2.WinForms.Guna2Separator()
			{
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillThickness = 2,
				Location = new System.Drawing.Point(158, 20),
				Name = "seperator_total",
				Size = new System.Drawing.Size(136, 10),
				TabIndex = 8,
			};
			Guna.UI2.WinForms.Guna2Panel panel_accuracy = new Guna.UI2.WinForms.Guna2Panel()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderRadius = 20,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Location = new System.Drawing.Point(20, 160),
				Name = "panel_accuracy",
				Size = new System.Drawing.Size(380, 50),
				TabIndex = 9,
			};
			Guna.UI2.WinForms.Guna2Separator seperator_accuracy = new Guna.UI2.WinForms.Guna2Separator()
			{
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillThickness = 2,
				Location = new System.Drawing.Point(189, 20),
				Name = "seperator_accuracy",
				Size = new System.Drawing.Size(96, 10),
				TabIndex = 8,
			};
			Guna.UI2.WinForms.Guna2TextBox txt_accuracy = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = $"{accuracy}%",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255))))),
				Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104))))),
				Location = new System.Drawing.Point(291, 10),
				Margin = new System.Windows.Forms.Padding(3, 4, 3, 4),
				Name = "txt_accuracy",
				PlaceholderText = "",
				ReadOnly = true,
				RightToLeft = System.Windows.Forms.RightToLeft.Yes,
				SelectedText = "",
				Size = new System.Drawing.Size(69, 30),
				TabIndex = 7,
				TextOffset = new System.Drawing.Point(0, -1),
			};
			Guna.UI2.WinForms.Guna2TextBox lbl_accuracy = new Guna.UI2.WinForms.Guna2TextBox()
			{

				BorderThickness = 0,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = "ACCURACY",
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52))))),
				Location = new System.Drawing.Point(90, 10),
				Margin = new System.Windows.Forms.Padding(3, 4, 3, 4),
				Name = "lbl_accuracy",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(93, 30),
				TabIndex = 6,
			};
			Guna.UI2.WinForms.Guna2PictureBox pic_accuracy = new Guna.UI2.WinForms.Guna2PictureBox()
			{
				BackColor = System.Drawing.Color.Transparent,
				FillColor = System.Drawing.Color.Transparent,
				Image = global::client_app.Properties.Resources.accuracy,
				ImageRotate = 0F,
				Location = new System.Drawing.Point(27, 7),
				Name = "pic_accuracy",
				Size = new System.Drawing.Size(36, 36),
				SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
				TabIndex = 5,
				TabStop = false,
				UseTransparentBackground = true,
			};
			Guna.UI2.WinForms.Guna2Shapes circle_accuracy = new Guna.UI2.WinForms.Guna2Shapes()
			{
				BackColor = System.Drawing.Color.Transparent,
				BorderColor = System.Drawing.Color.White,
				BorderThickness = 5,
				FillColor = System.Drawing.Color.Transparent,
				Location = new System.Drawing.Point(30, 150),
				Name = "circle_accuracy",
				PolygonSides = 3,
				PolygonSkip = 1,
				Rotate = 9F,
				Shape = Guna.UI2.WinForms.Enums.ShapeType.Ellipse,
				Size = new System.Drawing.Size(70, 70),
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

		/// <summary>
		/// Loads the Lobby menu.
		/// </summary>
		/// <param name="main"></param>
		/// <param name="users"></param>
		public static void initialiseLobby(main main, List<friendData> users)
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
		public static void configPanel_main_lobby(main main, List<friendData> users)
		{
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

		/// <summary>
		/// Handles the click event for the close button, disconnecting the client and closing the application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

	}
}
