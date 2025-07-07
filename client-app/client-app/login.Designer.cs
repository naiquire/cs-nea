namespace client_app
{
    partial class login
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
        private void InitializeComponent()
        {
            this.txt_userID = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            this.btn_createAccount = new System.Windows.Forms.Button();
            this.btn_requestAccount = new System.Windows.Forms.Button();
            this.list_languages = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_userID
            // 
            this.txt_userID.Location = new System.Drawing.Point(250, 250);
            this.txt_userID.Name = "txt_userID";
            this.txt_userID.Size = new System.Drawing.Size(300, 20);
            this.txt_userID.TabIndex = 0;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(250, 280);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(300, 20);
            this.txt_password.TabIndex = 1;
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(350, 310);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(100, 30);
            this.btn_login.TabIndex = 2;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // pic_logo
            // 
            this.pic_logo.Location = new System.Drawing.Point(300, 69);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(200, 126);
            this.pic_logo.TabIndex = 3;
            this.pic_logo.TabStop = false;
            // 
            // btn_createAccount
            // 
            this.btn_createAccount.Location = new System.Drawing.Point(350, 408);
            this.btn_createAccount.Name = "btn_createAccount";
            this.btn_createAccount.Size = new System.Drawing.Size(100, 30);
            this.btn_createAccount.TabIndex = 4;
            this.btn_createAccount.Text = "Create Account";
            this.btn_createAccount.UseVisualStyleBackColor = true;
            this.btn_createAccount.Click += new System.EventHandler(this.btn_createAccount_Click);
            // 
            // btn_requestAccount
            // 
            this.btn_requestAccount.Enabled = false;
            this.btn_requestAccount.Location = new System.Drawing.Point(350, 310);
            this.btn_requestAccount.Name = "btn_requestAccount";
            this.btn_requestAccount.Size = new System.Drawing.Size(100, 30);
            this.btn_requestAccount.TabIndex = 5;
            this.btn_requestAccount.Text = "Request Account";
            this.btn_requestAccount.UseVisualStyleBackColor = true;
            this.btn_requestAccount.Click += new System.EventHandler(this.btn_requestAccount_Click);
            // 
            // list_languages
            // 
            this.list_languages.Enabled = false;
            this.list_languages.FormattingEnabled = true;
            this.list_languages.Location = new System.Drawing.Point(587, 69);
            this.list_languages.Name = "list_languages";
            this.list_languages.Size = new System.Drawing.Size(201, 21);
            this.list_languages.TabIndex = 6;
            this.list_languages.Visible = false;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.list_languages);
            this.Controls.Add(this.btn_requestAccount);
            this.Controls.Add(this.btn_createAccount);
            this.Controls.Add(this.pic_logo);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.txt_userID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "login";
            this.Text = "login";
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_userID;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.PictureBox pic_logo;
        private System.Windows.Forms.Button btn_createAccount;
        private System.Windows.Forms.Button btn_requestAccount;
        private System.Windows.Forms.ComboBox list_languages;
    }
}