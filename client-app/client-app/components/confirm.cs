using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace client_app.components
{
	public class confirm : Form
	{
		private readonly Guna.UI2.WinForms.Guna2TextBox header;
		private readonly Guna.UI2.WinForms.Guna2GradientButton btn_accept;
		private readonly Guna.UI2.WinForms.Guna2GradientButton btn_cancel;

		public confirm(string text)
		{
			SuspendLayout();

			header = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				BorderRadius = 10,
				BorderThickness = 2,
				Cursor = System.Windows.Forms.Cursors.Arrow,
				DefaultText = text,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208))))),
				Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(20, 20),
				Margin = new System.Windows.Forms.Padding(5, 5, 5, 5),
				Multiline = true,
				Name = "header",
				PlaceholderText = "",
				ReadOnly = true,
				SelectedText = "",
				Size = new System.Drawing.Size(360, 100),
				TabIndex = 0,
				TabStop = false,
			};
			btn_accept = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 14,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(113)))), ((int)(((byte)(163))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(113)))), ((int)(((byte)(247))))),
				Font = new System.Drawing.Font("Bahnschrift", 9.75F),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(215, 145),
				Name = "btn_accept",
				Size = new System.Drawing.Size(160, 30),
				TabIndex = 1,
				Text = "Accept",
			};
			btn_cancel = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 14,
				FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(156)))), ((int)(((byte)(156))))),
				Font = new System.Drawing.Font("Bahnschrift", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
				ForeColor = System.Drawing.Color.White,
				Location = new System.Drawing.Point(25, 145),
				Name = "btn_cancel",
				Size = new System.Drawing.Size(160, 30),
				TabIndex = 1,
				Text = "Ignore",
			};

			btn_accept.Click += (sender, e) => { DialogResult = DialogResult.OK; Close(); };
			btn_cancel.Click += (sender, e) => { DialogResult = DialogResult.Cancel; Close(); };

			AcceptButton = btn_accept;
			BackColor = Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
			CancelButton = btn_cancel;
			ClientSize = new Size(400, 200);
			Controls.Add(header);
			Controls.Add(btn_accept);
			Controls.Add(btn_cancel);
			FormBorderStyle = FormBorderStyle.None;
			Name = "abstractMenu";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			TopMost = true;
			ResumeLayout(false);

			ShowDialog();
		}
	}
}
