using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace client_app.components
{
	public class update : Form
	{
		private string userID;
		private string aboutMe;
		private string localisation;

		private Guna2HtmlLabel lbl_header;
		private Guna2GradientButton btn_accept;
		private Guna2GradientButton btn_cancel;
		private Guna2TextBox txt_username;
		private Guna2TextBox txt_aboutMe;
		private PictureBox pic_logo;
		private Guna2PictureBox pic_language;
		private Guna2Button btn_language;
		private Guna2HtmlLabel lbl_textLength;
		
		private int languageIndex;

		public update(userData user)
		{
			userID = user.userID;
			aboutMe = user.aboutMe;
			localisation = user.localisation;

			languageIndex = languages.languageCodes.IndexOf(localisation);

			InitializeComponent();

			txt_username.Text = userID;
			txt_aboutMe.Text = aboutMe;
			lbl_header.Text = languages.localisation["Account"][localisation];

			btn_language.Text = languages.supportedLanguages[languageIndex];
			lbl_textLength.Text = $"{txt_aboutMe.Text.Length} / 500";

			btn_accept.Click += (sender, e) =>
			{
				localisation = languages.languageCodes[languageIndex];
				aboutMe = txt_aboutMe.Text;

				DialogResult = DialogResult.OK;
				Close();
			};
			btn_cancel.Click += (sender, e) => { DialogResult = DialogResult.Cancel; Close(); };

			this.ShowDialog();
		}
		private void InitializeComponent()
		{
			this.lbl_header = new Guna.UI2.WinForms.Guna2HtmlLabel();
			this.btn_accept = new Guna.UI2.WinForms.Guna2GradientButton();
			this.btn_cancel = new Guna.UI2.WinForms.Guna2GradientButton();
			this.txt_aboutMe = new Guna.UI2.WinForms.Guna2TextBox();
			this.txt_username = new Guna.UI2.WinForms.Guna2TextBox();
			this.pic_logo = new System.Windows.Forms.PictureBox();
			this.pic_language = new Guna.UI2.WinForms.Guna2PictureBox();
			this.btn_language = new Guna.UI2.WinForms.Guna2Button();
			this.lbl_textLength = new Guna.UI2.WinForms.Guna2HtmlLabel();
			((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_language)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_header
			// 
			this.lbl_header.AutoSize = false;
			this.lbl_header.BackColor = System.Drawing.Color.Transparent;
			this.lbl_header.Font = new System.Drawing.Font("Bahnschrift SemiBold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_header.ForeColor = System.Drawing.Color.White;
			this.lbl_header.Location = new System.Drawing.Point(20, 15);
			this.lbl_header.Name = "lbl_header";
			this.lbl_header.Size = new System.Drawing.Size(760, 50);
			this.lbl_header.TabIndex = 0;
			this.lbl_header.Text = "Account";
			// 
			// btn_accept
			// 
			this.btn_accept.AutoRoundedCorners = true;
			this.btn_accept.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163)))));
			this.btn_accept.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247)))));
			this.btn_accept.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btn_accept.ForeColor = System.Drawing.Color.White;
			this.btn_accept.Location = new System.Drawing.Point(580, 330);
			this.btn_accept.Name = "btn_accept";
			this.btn_accept.Size = new System.Drawing.Size(200, 50);
			this.btn_accept.TabIndex = 1;
			this.btn_accept.Text = "Confirm";
			// 
			// btn_cancel
			// 
			this.btn_cancel.AutoRoundedCorners = true;
			this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_cancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.btn_cancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.btn_cancel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btn_cancel.ForeColor = System.Drawing.Color.White;
			this.btn_cancel.Location = new System.Drawing.Point(360, 330);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new System.Drawing.Size(200, 50);
			this.btn_cancel.TabIndex = 2;
			this.btn_cancel.Text = "Cancel";
			// 
			// txt_aboutMe
			// 
			this.txt_aboutMe.BorderRadius = 10;
			this.txt_aboutMe.BorderThickness = 0;
			this.txt_aboutMe.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txt_aboutMe.DefaultText = "";
			this.txt_aboutMe.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_aboutMe.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txt_aboutMe.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_aboutMe.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_aboutMe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_aboutMe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_aboutMe.Font = new System.Drawing.Font("Bahnschrift", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_aboutMe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
			this.txt_aboutMe.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_aboutMe.IconLeftOffset = new System.Drawing.Point(5, 0);
			this.txt_aboutMe.IconLeftSize = new System.Drawing.Size(17, 20);
			this.txt_aboutMe.Location = new System.Drawing.Point(20, 116);
			this.txt_aboutMe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_aboutMe.MaxLength = 500;
			this.txt_aboutMe.Multiline = true;
			this.txt_aboutMe.Name = "txt_aboutMe";
			this.txt_aboutMe.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156)))));
			this.txt_aboutMe.PlaceholderText = "About me";
			this.txt_aboutMe.ReadOnly = false;
			this.txt_aboutMe.SelectedText = "";
			this.txt_aboutMe.Size = new System.Drawing.Size(760, 100);
			this.txt_aboutMe.TabIndex = 4;
			this.txt_aboutMe.TabStop = false;
			this.txt_aboutMe.TextChanged += new System.EventHandler(this.txt_aboutMe_TextChanged);
			// 
			// txt_username
			// 
			this.txt_username.BorderRadius = 10;
			this.txt_username.BorderThickness = 0;
			this.txt_username.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txt_username.DefaultText = "username";
			this.txt_username.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_username.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
			this.txt_username.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_username.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
			this.txt_username.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			this.txt_username.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_username.Font = new System.Drawing.Font("Bahnschrift", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
			this.txt_username.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
			this.txt_username.IconLeft = global::client_app.Properties.Resources.account;
			this.txt_username.IconLeftOffset = new System.Drawing.Point(5, 0);
			this.txt_username.IconLeftSize = new System.Drawing.Size(17, 20);
			this.txt_username.Location = new System.Drawing.Point(20, 80);
			this.txt_username.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txt_username.Name = "txt_username";
			this.txt_username.PlaceholderText = "";
			this.txt_username.ReadOnly = true;
			this.txt_username.SelectedText = "";
			this.txt_username.Size = new System.Drawing.Size(540, 30);
			this.txt_username.TabIndex = 3;
			this.txt_username.TabStop = false;
			this.txt_username.TextOffset = new System.Drawing.Point(10, -1);
			// 
			// pic_logo
			// 
			this.pic_logo.Image = global::client_app.Properties.Resources.app_logo;
			this.pic_logo.Location = new System.Drawing.Point(601, 15);
			this.pic_logo.Name = "pic_logo";
			this.pic_logo.Size = new System.Drawing.Size(200, 50);
			this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pic_logo.TabIndex = 5;
			this.pic_logo.TabStop = false;
			// 
			// pic_language
			// 
			this.pic_language.Image = global::client_app.Properties.Resources.language;
			this.pic_language.ImageRotate = 0F;
			this.pic_language.Location = new System.Drawing.Point(20, 223);
			this.pic_language.Name = "pic_language";
			this.pic_language.Size = new System.Drawing.Size(40, 40);
			this.pic_language.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pic_language.TabIndex = 6;
			this.pic_language.TabStop = false;
			// 
			// btn_language
			// 
			this.btn_language.AutoRoundedCorners = true;
			this.btn_language.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btn_language.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btn_language.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btn_language.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btn_language.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(104)))), ((int)(((byte)(104)))));
			this.btn_language.Font = new System.Drawing.Font("Bahnschrift", 9.75F);
			this.btn_language.ForeColor = System.Drawing.Color.White;
			this.btn_language.Location = new System.Drawing.Point(67, 223);
			this.btn_language.Name = "btn_language";
			this.btn_language.Size = new System.Drawing.Size(180, 40);
			this.btn_language.TabIndex = 7;
			this.btn_language.Text = "language";
			this.btn_language.Click += new System.EventHandler(this.btn_language_Click);
			// 
			// lbl_textLength
			// 
			this.lbl_textLength.AutoSize = false;
			this.lbl_textLength.BackColor = System.Drawing.Color.Transparent;
			this.lbl_textLength.ForeColor = System.Drawing.Color.White;
			this.lbl_textLength.Location = new System.Drawing.Point(700, 223);
			this.lbl_textLength.Name = "lbl_textLength";
			this.lbl_textLength.Size = new System.Drawing.Size(80, 15);
			this.lbl_textLength.TabIndex = 8;
			this.lbl_textLength.Text = "0 / 500";
			this.lbl_textLength.TextAlignment = System.Drawing.ContentAlignment.TopRight;
			// 
			// update
			// 
			this.AcceptButton = this.btn_accept;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
			this.CancelButton = this.btn_cancel;
			this.ClientSize = new System.Drawing.Size(800, 400);
			this.Controls.Add(this.lbl_textLength);
			this.Controls.Add(this.btn_language);
			this.Controls.Add(this.pic_language);
			this.Controls.Add(this.pic_logo);
			this.Controls.Add(this.txt_aboutMe);
			this.Controls.Add(this.txt_username);
			this.Controls.Add(this.lbl_header);
			this.Controls.Add(this.btn_accept);
			this.Controls.Add(this.btn_cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "update";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pic_language)).EndInit();
			this.ResumeLayout(false);
		}

		private void btn_language_Click(object sender, EventArgs e)
		{
			languageIndex++;
			if (languageIndex == languages.supportedLanguages.Count)
			{
				languageIndex = 0;
			}
			btn_language.Text = languages.supportedLanguages[languageIndex];

			lbl_header.Text = languages.localisation["Account"][languages.languageCodes[languageIndex]];
			
		}

		private void txt_aboutMe_TextChanged(object sender, EventArgs e)
		{
			lbl_textLength.Text = $"{txt_aboutMe.Text.Length} / 500";
		}

		public string getAboutMe() => aboutMe;
		public string getLocalisation() => localisation;
	}
}
