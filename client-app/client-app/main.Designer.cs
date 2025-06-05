using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace client_app
{
    partial class main
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

        /// <summary>
        /// hex codes for graphics
        /// 
        /// #f771a3
        /// c571f7
        /// 
        /// </summary>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel_topBorder = new System.Windows.Forms.Panel();
            this.txt_appName = new System.Windows.Forms.TextBox();
            this.panel_appLogo = new System.Windows.Forms.Panel();
            this.panel_friends = new System.Windows.Forms.Panel();
            this.panel_friendList = new System.Windows.Forms.Panel();
            this.txt_friendsLabel = new System.Windows.Forms.TextBox();
            this.panel_user = new System.Windows.Forms.Panel();
            this.panel_main = new System.Windows.Forms.Panel();
            this.seperator = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel_topBorder.SuspendLayout();
            this.panel_friends.SuspendLayout();
            this.panel_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seperator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_topBorder
            // 
            this.panel_topBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(23)))), ((int)(((byte)(24)))));
            this.panel_topBorder.Controls.Add(this.txt_appName);
            this.panel_topBorder.Location = new System.Drawing.Point(0, 0);
            this.panel_topBorder.Name = "panel_topBorder";
            this.panel_topBorder.Size = new System.Drawing.Size(1920, 30);
            this.panel_topBorder.TabIndex = 0;
            // 
            // txt_appName
            // 
            this.txt_appName.BackColor = this.panel_topBorder.BackColor;
            this.txt_appName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_appName.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_appName.Location = new System.Drawing.Point(10, 7);
            this.txt_appName.Name = "txt_appName";
            this.txt_appName.Size = new System.Drawing.Size(100, 16);
            this.txt_appName.TabIndex = 0;
            this.txt_appName.Text = "appName";
            // 
            // panel_appLogo
            // 
            this.panel_appLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panel_appLogo.Location = new System.Drawing.Point(0, 30);
            this.panel_appLogo.Name = "panel_appLogo";
            this.panel_appLogo.Size = new System.Drawing.Size(300, 100);
            this.panel_appLogo.TabIndex = 1;
            // 
            // panel_friends
            // 
            this.panel_friends.AutoScroll = true;
            this.panel_friends.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
            this.panel_friends.Controls.Add(this.panel_friendList);
            this.panel_friends.Controls.Add(this.seperator);
            this.panel_friends.Controls.Add(this.txt_friendsLabel);
            this.panel_friends.Location = new System.Drawing.Point(0, 130);
            this.panel_friends.Name = "panel_friends";
            this.panel_friends.Size = new System.Drawing.Size(300, 950);
            this.panel_friends.TabIndex = 2;
            // 
            // panel_friendList
            // 
            this.panel_friendList.AutoScroll = true;
            this.panel_friendList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panel_friendList.Location = new System.Drawing.Point(20, 90);
            this.panel_friendList.Name = "panel_friendList";
            this.panel_friendList.Size = new System.Drawing.Size(260, 384);
            this.panel_friendList.TabIndex = 2;
            // 
            // txt_friendsLabel
            // 
            this.txt_friendsLabel.BackColor = this.panel_friends.BackColor;
            this.txt_friendsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_friendsLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_friendsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
            this.txt_friendsLabel.Location = new System.Drawing.Point(100, 20);
            this.txt_friendsLabel.Name = "txt_friendsLabel";
            this.txt_friendsLabel.Size = new System.Drawing.Size(100, 33);
            this.txt_friendsLabel.TabIndex = 0;
            this.txt_friendsLabel.Text = "Friends";
            this.txt_friendsLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel_user
            // 
            this.panel_user.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(31)))), ((int)(((byte)(32)))));
            this.panel_user.Location = new System.Drawing.Point(1420, 30);
            this.panel_user.Name = "panel_user";
            this.panel_user.Size = new System.Drawing.Size(500, 1050);
            this.panel_user.TabIndex = 3;
            // 
            // panel_main
            // 
            this.panel_main.BackColor = System.Drawing.Color.Transparent;
            this.panel_main.Controls.Add(this.pictureBox1);
            this.panel_main.Location = new System.Drawing.Point(300, 30);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1120, 1050);
            this.panel_main.TabIndex = 4;
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
            // pictureBox1
            // 
            this.pictureBox1.Image = global::client_app.Properties.Resources.play;
            this.pictureBox1.Location = new System.Drawing.Point(360, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // main
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.panel_appLogo);
            this.Controls.Add(this.panel_topBorder);
            this.Controls.Add(this.panel_friends);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_user);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "main";
            this.panel_topBorder.ResumeLayout(false);
            this.panel_topBorder.PerformLayout();
            this.panel_friends.ResumeLayout(false);
            this.panel_friends.PerformLayout();
            this.panel_main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seperator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        public void closeApp(object sender, EventArgs e)
        {
            Close();
        }


        private void configFriendsPanel()
        {
            // seperate friends into online/offline
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

            // online text
            TextBox online = new TextBox()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(10, 10),
                Name = "txt_online",
                Size = new System.Drawing.Size(100, 16),
                TabIndex = 0,
                Text = "ONLINE",
            };
            panel_friendList.Controls.Add(online);

            // online count
            TextBox onlineCount = new TextBox()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(230, 10),
                Name = "txt_onlineCount",
                Size = new System.Drawing.Size(30, 16),
                TabIndex = 0,
                Text = onlineList.Count.ToString(),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Right,
            };

            // add online friends
            int y_offset = 20;
            for (int i = 0; i < onlineList.Count; i++, y_offset += 20)
            {
                Button user = new Button()
                {
                    BackColor = this.panel_friendList.BackColor,
                    Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(10, y_offset),
                    Name = onlineList[i].userID,
                    Size = new System.Drawing.Size(100, 20),
                    TabIndex = 0,
                    Text = onlineList[i].userID,
                };
                user.Click += async (sender, e) =>
                {
                    string userID = ((Button)sender).Name;
                    await requestProfile(userID);
                };
                panel_friendList.Controls.Add(user);
            }

            // offline text
            y_offset += 30;
            TextBox offline = new TextBox()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(10, y_offset),
                Name = "txt_appName",
                Size = new System.Drawing.Size(100, 16),
                TabIndex = 0,
                Text = "OFFLINE",
            };
            panel_friendList.Controls.Add(offline);

            // offline count
            TextBox offlineCount = new TextBox()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(230, y_offset),
                Name = "txt_offlineCount",
                Size = new System.Drawing.Size(30, 16),
                TabIndex = 0,
                Text = onlineList.Count.ToString(),
                TextAlign = System.Windows.Forms.HorizontalAlignment.Right,
            };

            // add offline friends
            y_offset += 10;
            for (int i = 0; i < offlineList.Count; i++, y_offset += 20)
            {
                Button user = new Button()
                {
                    BackColor = this.panel_friendList.BackColor,
                    Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(10, y_offset),
                    Name = offlineList[i].userID,
                    Size = new System.Drawing.Size(100, 20),
                    TabIndex = 0,
                    Text = offlineList[i].userID,
                };
                user.Click += async (sender, e) =>
                {
                    string userID = ((Button)sender).Name;
                    await requestProfile(userID);
                };
                panel_friendList.Controls.Add(user);
            }
        }



        


        


        #endregion

        private System.Windows.Forms.Panel panel_topBorder;
        private System.Windows.Forms.Panel panel_appLogo;
        private System.Windows.Forms.Panel panel_friends;
        private TextBox txt_appName;
        private TextBox txt_friendsLabel;
        private PictureBox seperator;
        private Panel panel_friendList;
        private Panel panel_user;
        private Panel panel_main;
        private PictureBox pictureBox1;
        private Panel panel_input;
    }
}

