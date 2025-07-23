using client_app.games;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.components
{
	public class queueing : Form
	{
		private Button okButton;
		private Button cancelButton;
		private string text;
		public queueing(string text)
		{
			ClientSize = new Size(600, 300);
			StartPosition = FormStartPosition.CenterScreen;
			BackColor = ColorTranslator.FromHtml("#2e2e2e");
			FormBorderStyle = FormBorderStyle.None;
			ShowInTaskbar = false;
			BringToFront();

			lbl_header = new Label
			{
				Text = text,
				Font = new Font("Bahnschrift", 12, FontStyle.Bold),
				AutoSize = false,
				Size = new Size(380, 30),
				Location = new Point(10, 20),
				TextAlign = ContentAlignment.MiddleCenter
			};

			pic_loading = new PictureBox()
			{
				Image = global::client_app.Properties.Resources.loading,
				Location = new System.Drawing.Point(200, 100),
				Name = "pic_loading",
				Size = new System.Drawing.Size(100, 100),
				SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
				TabIndex = 0,
				TabStop = false,
			};

			btn_confirm = new Button()
			{
				BackColor = this.BackColor,
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				Location = new System.Drawing.Point(100, 100),
				Name = "btn_confirm",
				Size = new System.Drawing.Size(50, 30),
				TabIndex = 0,
				Text = "join",
				FlatStyle = FlatStyle.Flat,
			};

			btn_confirm.Click += (sender, e) =>
			{
				this.Close();
			};

			Controls.Add(lbl_header);
			Controls.Add(pic_loading);

			

			ShowDialog();
		}

		private Label lbl_header;
		private PictureBox pic_loading;
		public Button btn_confirm;
	}
}
