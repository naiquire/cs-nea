using client_app.Properties;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace client_app.components
{
	public class update : Form
	{
		private readonly string userID;
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

			ShowDialog();
		}
		private void InitializeComponent()
		{
			SuspendLayout();

			lbl_header = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				Font = new Font("Bahnschrift SemiBold", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(20, 15),
				Size = new Size(760, 50),
				TabStop = false,
			};
			btn_accept = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Segoe UI", 9F),
				ForeColor = Color.White,
				Location = new Point(580, 330),
				Size = new Size(200, 50),
				TabStop = false,
				Text = "Confirm",
			};
			btn_cancel = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				FillColor = Color.FromArgb(208, 208, 208),
				FillColor2 = Color.FromArgb(208, 208, 208),
				Font = new Font("Segoe UI", 9F),
				ForeColor = Color.White,
				Location = new Point(360, 330),
				Size = new Size(200, 50),
				TabStop = false,
				Text = "Cancel",
			};
			txt_aboutMe = new Guna2TextBox()
			{
				BorderRadius = 10,
				BorderThickness = 0,
				Cursor = Cursors.IBeam,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(104, 104, 104),
				IconLeftOffset = new Point(5, 0),
				IconLeftSize = new Size(17, 20),
				Location = new Point(20, 116),
				MaxLength = 500,
				Multiline = true,
				PlaceholderForeColor = Color.FromArgb(156, 156, 156),
				PlaceholderText = languages.localisation["About me"][main.userData.localisation],
				Size = new Size(760, 100),
			};
			
			txt_username = new Guna2TextBox()
			{
				BorderRadius = 10,
				BorderThickness = 0,
				Cursor = Cursors.Arrow,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.FromArgb(104, 104, 104),
				IconLeft = Resources.account,
				IconLeftOffset = new Point(5, 0),
				IconLeftSize = new Size(17, 20),
				Location = new Point(20, 80),
				ReadOnly = true,
				Size = new Size(540, 30),
				TabStop = false,
				TextOffset = new Point(10, -1),
			};
			pic_logo = new PictureBox()
			{
				Image = Resources.app_logo,
				Location = new Point(601, 15),
				Size = new Size(200, 50),
				SizeMode = PictureBoxSizeMode.Zoom,
				TabStop = false,
			};
			pic_language = new Guna2PictureBox()
			{
				Image = Resources.language,
				ImageRotate = 0F,
				Location = new Point(20, 223),
				Size = new Size(40, 40),
				SizeMode = PictureBoxSizeMode.Zoom,
				TabStop = false,
			};
			btn_language = new Guna2Button()
			{
				AutoRoundedCorners = true,
				FillColor = Color.FromArgb(104, 104, 104),
				Font = new Font("Bahnschrift", 9.75F),
				ForeColor = Color.White,
				Location = new Point(67, 223),
				Size = new Size(180, 40),
			};
			lbl_textLength = new Guna2HtmlLabel()
			{
				AutoSize = false,
				BackColor = Color.Transparent,
				ForeColor = Color.White,
				Location = new Point(700, 223),
				Size = new Size(80, 15),
				TabStop = false,
				TextAlignment = ContentAlignment.TopRight,
			};

			txt_aboutMe.TextChanged += new EventHandler(txt_aboutMe_TextChanged);
			btn_language.Click += new EventHandler(btn_language_Click);

			Controls.Add(lbl_textLength);
			Controls.Add(btn_language);
			Controls.Add(pic_language);
			Controls.Add(pic_logo);
			Controls.Add(txt_aboutMe);
			Controls.Add(txt_username);
			Controls.Add(lbl_header);
			Controls.Add(btn_accept);
			Controls.Add(btn_cancel);

			AcceptButton = btn_accept;
			BackColor = Color.FromArgb(58, 51, 54);
			CancelButton = btn_cancel;
			ClientSize = new Size(800, 400);
			FormBorderStyle = FormBorderStyle.None;
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			TopMost = true;
			ResumeLayout(false);
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
