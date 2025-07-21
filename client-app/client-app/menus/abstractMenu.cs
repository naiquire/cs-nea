using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus
{
    public class abstractMenu : Form
    {
        protected Panel panel_topBorder;
        protected Button btn_close;
        protected Label lbl_appName;
        protected Panel panel_left;
        protected Panel panel_topLeft;
        public Panel panel_main;
        protected Panel panel_right;


		protected virtual void InitializeComponent()
        {
			this.Controls.Clear();

			this.panel_topBorder = new System.Windows.Forms.Panel();
			this.lbl_appName = new System.Windows.Forms.Label();
			this.btn_close = new System.Windows.Forms.Button();
			this.panel_left = new System.Windows.Forms.Panel();
			this.panel_topLeft = new System.Windows.Forms.Panel();
			this.panel_main = new System.Windows.Forms.Panel();
			this.panel_right = new System.Windows.Forms.Panel();
			this.panel_topBorder.SuspendLayout();
			this.panel_left.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_topBorder
			// 
			this.panel_topBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
			this.panel_topBorder.Controls.Add(this.lbl_appName);
			this.panel_topBorder.Controls.Add(this.btn_close);
			this.panel_topBorder.Location = new System.Drawing.Point(0, 0);
			this.panel_topBorder.Name = "panel_topBorder";
			this.panel_topBorder.Size = new System.Drawing.Size(1920, 30);
			this.panel_topBorder.TabIndex = 0;
			// 
			// lbl_appName
			// 
			this.lbl_appName.BackColor = this.panel_topBorder.BackColor;
			this.lbl_appName.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_appName.Location = new System.Drawing.Point(10, 7);
			this.lbl_appName.Name = "lbl_appName";
			this.lbl_appName.Size = new System.Drawing.Size(100, 16);
			this.lbl_appName.TabIndex = 0;
			this.lbl_appName.Text = "appName";
			// 
			// btn_close
			// 
			this.btn_close.Location = new System.Drawing.Point(1890, 0);
			this.btn_close.Name = "btn_close";
			this.btn_close.Size = new System.Drawing.Size(30, 30);
			this.btn_close.TabIndex = 0;
			this.btn_close.Text = "X";
			this.btn_close.UseVisualStyleBackColor = true;
			this.btn_close.Click += closeApp;
			// 
			// panel_left
			// 
			this.panel_left.AutoScroll = true;
			this.panel_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			this.panel_left.Location = new System.Drawing.Point(0, 130);
			this.panel_left.Name = "panel_left";
			this.panel_left.Size = new System.Drawing.Size(300, 950);
			this.panel_left.TabIndex = 2;
			// 
			// panel_topLeft
			// 
			this.panel_topLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
			this.panel_topLeft.Location = new System.Drawing.Point(0, 30);
			this.panel_topLeft.Name = "panel_topLeft";
			this.panel_topLeft.Size = new System.Drawing.Size(300, 100);
			this.panel_topLeft.TabIndex = 1;
			// 
			// panel_main
			// 
			this.panel_main.BackColor = System.Drawing.Color.Transparent;
			this.panel_main.Location = new System.Drawing.Point(300, 30);
			this.panel_main.Name = "panel_main";
			this.panel_main.Size = new System.Drawing.Size(1120, 1050);
			this.panel_main.TabIndex = 4;
			// 
			// panel_right
			// 
			this.panel_right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
			this.panel_right.Location = new System.Drawing.Point(1420, 30);
			this.panel_right.Name = "panel_right";
			this.panel_right.Size = new System.Drawing.Size(500, 1050);
			this.panel_right.TabIndex = 3;
			// 
			// abstractMenu
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1920, 1080);
			this.Controls.Add(this.panel_topLeft);
			this.Controls.Add(this.panel_topBorder);
			this.Controls.Add(this.panel_left);
			this.Controls.Add(this.panel_main);
			this.Controls.Add(this.panel_right);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "abstractMenu";
			this.panel_topBorder.ResumeLayout(false);
			this.panel_left.ResumeLayout(false);
			this.ResumeLayout(false);
        }
		protected virtual void InitializeComponent(main main)
		{
			main.Controls.Clear();

			main.panel_topBorder = new System.Windows.Forms.Panel();
			main.lbl_appName = new System.Windows.Forms.Label();
			main.btn_close = new System.Windows.Forms.Button();
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
			main.btn_close.Click += closeApp;
			// 
			// panel_left
			// 
			main.panel_left.AutoScroll = true;
			main.panel_left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
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
		}

		public async void closeApp(object sender, EventArgs e)
		{
			Hide();
			await main.connection.InvokeAsync("clientDisconnected", main.userData.userID);
			Close();
		}
	}
}
