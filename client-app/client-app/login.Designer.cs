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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
			this.btn_login = new Guna.UI2.WinForms.Guna2GradientButton();
			this.btn_createAccount = new Guna.UI2.WinForms.Guna2Button();
			this.lbl_header = new Guna.UI2.WinForms.Guna2TextBox();
			this.txt_password = new Guna.UI2.WinForms.Guna2TextBox();
			this.txt_userID = new Guna.UI2.WinForms.Guna2TextBox();
			this.btn_language = new Guna.UI2.WinForms.Guna2Button();
			this.pic_language = new Guna.UI2.WinForms.Guna2PictureBox();
			this.btn_requestAccount = new Guna.UI2.WinForms.Guna2GradientButton();
			this.txt_passwordconfirm = new Guna.UI2.WinForms.Guna2TextBox();
			this.pic_connecting = new Guna.UI2.WinForms.Guna2ProgressIndicator();
			this.lbl_connection = new Guna.UI2.WinForms.Guna2Button();
			this.lbl_information = new Guna.UI2.WinForms.Guna2Button();
			((System.ComponentModel.ISupportInitialize)(this.pic_language)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_login
			// 
			this.btn_login.AutoRoundedCorners = true;
			this.btn_login.BorderRadius = 14;
			this.btn_login.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
			this.btn_login.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247)))));
			resources.ApplyResources(this.btn_login, "btn_login");
			this.btn_login.ForeColor = System.Drawing.Color.White;
			this.btn_login.Name = "btn_login";
			this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
			// 
			// btn_createAccount
			// 
			this.btn_createAccount.BackColor = System.Drawing.Color.Transparent;
			this.btn_createAccount.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.btn_createAccount, "btn_createAccount");
			this.btn_createAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.btn_createAccount.Name = "btn_createAccount";
			this.btn_createAccount.Click += new System.EventHandler(this.btn_createAccount_Click);
			// 
			// lbl_header
			// 
			this.lbl_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
			this.lbl_header.BorderThickness = 0;
			this.lbl_header.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.lbl_header.DefaultText = "Account";
			this.lbl_header.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.lbl_header.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.lbl_header.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_header.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.lbl_header.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
			this.lbl_header.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.lbl_header, "lbl_header");
			this.lbl_header.ForeColor = System.Drawing.Color.White;
			this.lbl_header.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.lbl_header.Name = "lbl_header";
			this.lbl_header.PlaceholderForeColor = System.Drawing.Color.Transparent;
			this.lbl_header.PlaceholderText = "";
			this.lbl_header.ReadOnly = true;
			this.lbl_header.SelectedText = "";
			this.lbl_header.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txt_password
			// 
			this.txt_password.BorderRadius = 8;
			this.txt_password.BorderThickness = 0;
			this.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txt_password.DefaultText = "";
			this.txt_password.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_password.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txt_password.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_password.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_password.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_password.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.txt_password, "txt_password");
			this.txt_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
			this.txt_password.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_password.IconLeft = global::client_app.Properties.Resources.password;
			this.txt_password.IconLeftOffset = new System.Drawing.Point(5, 0);
			this.txt_password.IconLeftSize = new System.Drawing.Size(17, 20);
			this.txt_password.MaxLength = 32;
			this.txt_password.Name = "txt_password";
			this.txt_password.PasswordChar = '*';
			this.txt_password.PlaceholderForeColor = System.Drawing.Color.Gray;
			this.txt_password.PlaceholderText = "Password";
			this.txt_password.SelectedText = "";
			this.txt_password.TextOffset = new System.Drawing.Point(5, 0);
			// 
			// txt_userID
			// 
			this.txt_userID.BorderRadius = 8;
			this.txt_userID.BorderThickness = 0;
			this.txt_userID.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txt_userID.DefaultText = "";
			this.txt_userID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_userID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txt_userID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_userID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_userID.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_userID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.txt_userID, "txt_userID");
			this.txt_userID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
			this.txt_userID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_userID.IconLeft = global::client_app.Properties.Resources.account;
			this.txt_userID.IconLeftOffset = new System.Drawing.Point(5, 0);
			this.txt_userID.IconLeftSize = new System.Drawing.Size(17, 20);
			this.txt_userID.MaxLength = 32;
			this.txt_userID.Name = "txt_userID";
			this.txt_userID.PlaceholderForeColor = System.Drawing.Color.Gray;
			this.txt_userID.PlaceholderText = "Username";
			this.txt_userID.SelectedText = "";
			this.txt_userID.TextOffset = new System.Drawing.Point(5, 0);
			// 
			// btn_language
			// 
			this.btn_language.AutoRoundedCorners = true;
			this.btn_language.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btn_language.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btn_language.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btn_language.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btn_language.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
			resources.ApplyResources(this.btn_language, "btn_language");
			this.btn_language.ForeColor = System.Drawing.Color.Gainsboro;
			this.btn_language.Name = "btn_language";
			this.btn_language.Click += new System.EventHandler(this.btn_language_Click);
			// 
			// pic_language
			// 
			this.pic_language.FillColor = System.Drawing.Color.Transparent;
			this.pic_language.Image = global::client_app.Properties.Resources.language;
			this.pic_language.ImageRotate = 0F;
			resources.ApplyResources(this.pic_language, "pic_language");
			this.pic_language.Name = "pic_language";
			this.pic_language.TabStop = false;
			// 
			// btn_requestAccount
			// 
			this.btn_requestAccount.AutoRoundedCorners = true;
			this.btn_requestAccount.BorderRadius = 14;
			this.btn_requestAccount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
			this.btn_requestAccount.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247)))));
			resources.ApplyResources(this.btn_requestAccount, "btn_requestAccount");
			this.btn_requestAccount.ForeColor = System.Drawing.Color.White;
			this.btn_requestAccount.Name = "btn_requestAccount";
			this.btn_requestAccount.Click += new System.EventHandler(this.btn_requestAccount_Click);
			// 
			// txt_passwordconfirm
			// 
			this.txt_passwordconfirm.BorderRadius = 8;
			this.txt_passwordconfirm.BorderThickness = 0;
			this.txt_passwordconfirm.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txt_passwordconfirm.DefaultText = "";
			this.txt_passwordconfirm.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_passwordconfirm.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txt_passwordconfirm.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_passwordconfirm.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_passwordconfirm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_passwordconfirm.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			resources.ApplyResources(this.txt_passwordconfirm, "txt_passwordconfirm");
			this.txt_passwordconfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
			this.txt_passwordconfirm.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_passwordconfirm.IconLeft = global::client_app.Properties.Resources.password;
			this.txt_passwordconfirm.IconLeftOffset = new System.Drawing.Point(5, 0);
			this.txt_passwordconfirm.IconLeftSize = new System.Drawing.Size(17, 20);
			this.txt_passwordconfirm.MaxLength = 32;
			this.txt_passwordconfirm.Name = "txt_passwordconfirm";
			this.txt_passwordconfirm.PasswordChar = '*';
			this.txt_passwordconfirm.PlaceholderForeColor = System.Drawing.Color.Gray;
			this.txt_passwordconfirm.PlaceholderText = "Confirm Password";
			this.txt_passwordconfirm.SelectedText = "";
			this.txt_passwordconfirm.TextOffset = new System.Drawing.Point(5, 0);
			// 
			// pic_connecting
			// 
			this.pic_connecting.AutoStart = true;
			this.pic_connecting.BackColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.pic_connecting, "pic_connecting");
			this.pic_connecting.Name = "pic_connecting";
			this.pic_connecting.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
			// 
			// lbl_connection
			// 
			this.lbl_connection.BackColor = System.Drawing.Color.Transparent;
			this.lbl_connection.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.lbl_connection, "lbl_connection");
			this.lbl_connection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.lbl_connection.Name = "lbl_connection";
			this.lbl_connection.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// lbl_information
			// 
			this.lbl_information.BackColor = System.Drawing.Color.Transparent;
			this.lbl_information.FillColor = System.Drawing.Color.Transparent;
			resources.ApplyResources(this.lbl_information, "lbl_information");
			this.lbl_information.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.lbl_information.Name = "lbl_information";
			this.lbl_information.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			// 
			// login
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
			this.Controls.Add(this.lbl_information);
			this.Controls.Add(this.pic_connecting);
			this.Controls.Add(this.lbl_connection);
			this.Controls.Add(this.pic_language);
			this.Controls.Add(this.btn_language);
			this.Controls.Add(this.lbl_header);
			this.Controls.Add(this.txt_userID);
			this.Controls.Add(this.txt_password);
			this.Controls.Add(this.btn_login);
			this.Controls.Add(this.btn_createAccount);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "login";
			((System.ComponentModel.ISupportInitialize)(this.pic_language)).EndInit();
			this.ResumeLayout(false);

        }

		private void controlEventConfigs()
		{
			this.btn_createAccount.MouseEnter += (sender, e) =>
			{
				this.btn_createAccount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
				this.btn_createAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
			};
			this.btn_createAccount.MouseLeave += (sender, e) =>
			{
				this.btn_createAccount.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(39)))), ((int)(((byte)(41)))));
				this.btn_createAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
				
			};
		}

        #endregion
        private Guna.UI2.WinForms.Guna2GradientButton btn_login;
        private Guna.UI2.WinForms.Guna2Button btn_createAccount;
		private Guna.UI2.WinForms.Guna2TextBox txt_userID;
		private Guna.UI2.WinForms.Guna2TextBox txt_password;
		private Guna.UI2.WinForms.Guna2TextBox lbl_header;
		private Guna.UI2.WinForms.Guna2Button btn_language;
		private Guna.UI2.WinForms.Guna2PictureBox pic_language;
		private Guna.UI2.WinForms.Guna2GradientButton btn_requestAccount;
		private Guna.UI2.WinForms.Guna2TextBox txt_passwordconfirm;
		private Guna.UI2.WinForms.Guna2ProgressIndicator pic_connecting;
		private Guna.UI2.WinForms.Guna2Button lbl_connection;
		public Guna.UI2.WinForms.Guna2Button lbl_information;
	}
}