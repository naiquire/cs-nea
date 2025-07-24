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
        public override void InitializeComponent()
        {
            base.InitializeComponent();

            lbl_username = new Label();
            lbl_rank = new Label();
            pic_language = new PictureBox();
            panel_stats = new Panel();
            ///
            ///
            /// 
            panel_stats.BackColor = panel_left.BackColor;
            
            ///
            /// lbl_username
            ///
            lbl_username.BackColor = panel_main.BackColor;
            lbl_username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbl_username.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_username.Location = new System.Drawing.Point(20, 20);
            lbl_username.Name = "lbl_username";
            lbl_username.Size = new System.Drawing.Size(100, 16);
            lbl_username.TabIndex = 0;
            lbl_username.Text = userData.userID;
            ///
            /// lbl_rank
            ///
            lbl_rank.BackColor = panel_main.BackColor;
            lbl_rank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbl_rank.Font = new System.Drawing.Font("Bahnschrift SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lbl_rank.Location = new System.Drawing.Point(20, 50);
            lbl_rank.Name = "lbl_rank";
            lbl_rank.Size = new System.Drawing.Size(100, 16);
            lbl_rank.TabIndex = 0;
            lbl_rank.Text = userData.rank.ToString();
            ///
            /// pic_language
            ///
            pic_language.Image = (System.Drawing.Image) global::client_app.Properties.Resources.ResourceManager.GetObject(userData.localisation); // may not work
            pic_language.Location = new System.Drawing.Point(360, 20);
            pic_language.Name = "pic_language";
            pic_language.Size = new System.Drawing.Size(400, 131);
            pic_language.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pic_language.TabIndex = 0;
            pic_language.TabStop = false;


            panel_main.Controls.Add(lbl_username);
            panel_main.Controls.Add(lbl_rank);
            panel_main.Controls.Add(pic_language);
            panel_main.Controls.Add(panel_stats);
        }

        private Label lbl_username;
        private Label lbl_rank;
        private PictureBox pic_language;

        private Panel panel_stats;

        #endregion
    }
}