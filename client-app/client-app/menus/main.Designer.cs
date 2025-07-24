using client_app.Properties;
using Microsoft.AspNetCore.SignalR.Client;
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
        /// f771a3
        /// c571f7
        /// 
        /// </summary>

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        public override void InitializeComponent()
        {
            resetLayout(this);

			this.panel_friendList = new System.Windows.Forms.Panel();
            this.seperator = new System.Windows.Forms.PictureBox();
            this.lbl_friendsLabel = new System.Windows.Forms.Label();
            this.btn_queueAccuracy = new System.Windows.Forms.Button();
            this.pic_play = new System.Windows.Forms.PictureBox();
			this.accuracy_seperator = new System.Windows.Forms.PictureBox();
			this.btn_queueAccuracy = new System.Windows.Forms.Button();
			this.panel_accuracy = new System.Windows.Forms.Panel();
			this.lbl_accuracy = new System.Windows.Forms.Label();

			this.panel_topBorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seperator)).BeginInit();
            this.panel_main.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_play)).BeginInit();
			this.panel_accuracy.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.accuracy_seperator)).BeginInit();
            this.SuspendLayout();
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
            // txt_friendsLabel
            // 
            this.lbl_friendsLabel.BackColor = this.panel_left.BackColor;
            this.lbl_friendsLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_friendsLabel.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_friendsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
            this.lbl_friendsLabel.Location = new System.Drawing.Point(0, 20);
            this.lbl_friendsLabel.Name = "txt_friendsLabel";
            this.lbl_friendsLabel.Size = new System.Drawing.Size(300, 33);
            this.lbl_friendsLabel.TabIndex = 0;
            this.lbl_friendsLabel.Text = localisation["Friends"][userData.localisation];
            this.lbl_friendsLabel.TextAlign = (ContentAlignment)System.Windows.Forms.HorizontalAlignment.Center;            
			// 
			// pic_play
			// 
			this.pic_play.Image = global::client_app.Properties.Resources.play;
            this.pic_play.Location = new System.Drawing.Point(360, 20);
            this.pic_play.Name = "pictureBox1";
            this.pic_play.Size = new System.Drawing.Size(400, 131);
            this.pic_play.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_play.TabIndex = 0;
            this.pic_play.TabStop = false;
			// 
			// accuracy_seperator
			// 
			this.accuracy_seperator.Image = global::client_app.Properties.Resources.seperator;
			this.accuracy_seperator.Location = new System.Drawing.Point(10, 40);
			this.accuracy_seperator.Name = "accuracy_seperator";
			this.accuracy_seperator.Size = new System.Drawing.Size(180, 10);
			this.accuracy_seperator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.accuracy_seperator.TabIndex = 1;
			this.accuracy_seperator.TabStop = false;
			// 
			// btn_queueAccuracy
			// 
			this.btn_queueAccuracy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.btn_queueAccuracy.ForeColor = System.Drawing.Color.Transparent;
			this.btn_queueAccuracy.Location = new System.Drawing.Point(55, 80);
			this.btn_queueAccuracy.Name = "btn_queueAccuracy";
			this.btn_queueAccuracy.Size = new System.Drawing.Size(90, 90);
			this.btn_queueAccuracy.TabIndex = 2;
			this.btn_queueAccuracy.UseVisualStyleBackColor = true;
			this.btn_queueAccuracy.Click += new System.EventHandler(this.btn_queueAccuracy_Click);
			// 
			// lbl_accuracy
			// 
			this.lbl_accuracy.AutoSize = true;
			this.lbl_accuracy.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_accuracy.ForeColor = System.Drawing.Color.White;
			this.lbl_accuracy.Location = new System.Drawing.Point(51, 10);
			this.lbl_accuracy.Name = "lbl_accuracy";
			this.lbl_accuracy.Size = new System.Drawing.Size(98, 25);
			this.lbl_accuracy.TabIndex = 0;
			this.lbl_accuracy.Text = "Accuracy";
			// 
			// panel_accuracy
			// 
			this.panel_accuracy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
			this.panel_accuracy.Controls.Add(this.btn_queueAccuracy);
			this.panel_accuracy.Controls.Add(this.accuracy_seperator);
			this.panel_accuracy.Controls.Add(this.lbl_accuracy);
			this.panel_accuracy.Location = new System.Drawing.Point(230, 177);
			this.panel_accuracy.Name = "panel_accuracy";
			this.panel_accuracy.Size = new System.Drawing.Size(200, 400);
			this.panel_accuracy.TabIndex = 0;
			//
			// main
			//
			this.panel_main.Controls.Add(pic_play);
			this.panel_main.Controls.Add(this.panel_accuracy);
			this.panel_left.Controls.Add(panel_friendList);
            this.panel_left.Controls.Add(seperator);
            this.panel_left.Controls.Add(lbl_friendsLabel);

			((System.ComponentModel.ISupportInitialize)(this.seperator)).EndInit();
            this.panel_main.ResumeLayout(false);
			this.panel_accuracy.ResumeLayout(false);
			this.panel_accuracy.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pic_play)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.accuracy_seperator)).EndInit();
			this.ResumeLayout(false);


            configFriendsPanel();
            configPanel_right_userData(this, userData);
        }
		public void btn_home_Click(object sender, EventArgs e)
		{
			InitializeComponent();
		}

		private void configFriendsPanel()
        {
            // seperate friends into online/offline
            List<friendData> onlineList = new List<friendData>();
            List<friendData> offlineList = new List<friendData>();

            const int buttonX = 200;
            const int buttonY = 30;
            const int padding = 5;

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

			int y_offset = 10;
			// online text
			Label online = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(10, y_offset),
                Name = "txt_online",
                Size = new System.Drawing.Size(200, buttonY),
                TabIndex = 0,
                Text = localisation["ONLINE"][userData.localisation],
            };
            panel_friendList.Controls.Add(online);

            // online count
            Label onlineCount = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(230, 10),
                Name = "txt_onlineCount",
                Size = new System.Drawing.Size(30, 16),
                TabIndex = 0,
                Text = onlineList.Count.ToString(),
                TextAlign = ContentAlignment.MiddleRight,
            };
			panel_friendList.Controls.Add(onlineCount);
			y_offset += 30;

			// add online friends
			for (int i = 0; i < onlineList.Count; i++, y_offset += buttonY + padding)
            {
                Button user = new Button()
                {
                    BackColor = this.panel_friendList.BackColor,
                    Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(10, y_offset),
                    Name = onlineList[i].userID,
                    Size = new System.Drawing.Size(buttonX, buttonY),
                    TabIndex = 0,
                    Text = onlineList[i].userID,
					FlatStyle = FlatStyle.Flat,
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
            Label offline = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(10, y_offset),
                Name = "txt_appName",
                Size = new System.Drawing.Size(200, buttonY),
                TabIndex = 0,
                Text = localisation["OFFLINE"][userData.localisation],
            };
            panel_friendList.Controls.Add(offline);

            // offline count
            Label offlineCount = new Label()
            {
                BackColor = this.panel_friendList.BackColor,
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(230, y_offset),
                Name = "txt_offlineCount",
                Size = new System.Drawing.Size(30, 16),
                TabIndex = 0,
                Text = offlineList.Count.ToString(),
				TextAlign = ContentAlignment.MiddleRight,
			};
			panel_friendList.Controls.Add(offlineCount);
			y_offset += 30;

			// add offline friends
			for (int i = 0; i < offlineList.Count; i++, y_offset += buttonY + padding)
            {
                Button user = new Button()
                {
                    BackColor = this.panel_friendList.BackColor,
                    Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                    Location = new System.Drawing.Point(10, y_offset),
                    Name = offlineList[i].userID,
                    Size = new System.Drawing.Size(buttonX, buttonY),
                    TabIndex = 0,
                    Text = offlineList[i].userID,
                    FlatStyle = FlatStyle.Flat,
                };
                user.Click += async (sender, e) =>
                {
                    string userID = ((Button)sender).Name;
                    await requestProfile(userID);
                };
                panel_friendList.Controls.Add(user);
            }
        }

        private Panel panel_friendList;
        private PictureBox seperator;
        private PictureBox pic_play;
        private Label lbl_friendsLabel;
		private Panel panel_accuracy;
		private Label lbl_accuracy;
		private PictureBox accuracy_seperator;
		private Button btn_queueAccuracy;

		#endregion

	}
}

