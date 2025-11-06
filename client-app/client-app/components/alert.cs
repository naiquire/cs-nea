using System.Drawing;
using System.Windows.Forms;

namespace client_app.components
{
	public class alert : Form
	{
		private readonly Guna.UI2.WinForms.Guna2TextBox header;
		private readonly Guna.UI2.WinForms.Guna2GradientButton btn_close;

		public alert(string text, bool closeButton)
		{
			SuspendLayout();

			header = new Guna.UI2.WinForms.Guna2TextBox()
			{
				BorderColor = Color.FromArgb(156, 156, 156),
				BorderRadius = 10,
				BorderThickness = 2,
				Cursor = Cursors.Arrow,
				DefaultText = text,
				FillColor = Color.FromArgb(208, 208, 208),
				Font = new Font("Bahnschrift SemiBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(20, 20),
				Margin = new Padding(5, 5, 5, 5),
				Multiline = true,
				ReadOnly = true,
				Size = new Size(720, 300),
				TabIndex = 0,
				TabStop = false,
			};
			btn_close = new Guna.UI2.WinForms.Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 14,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift", 9.75F),
				ForeColor = Color.White,
				Location = new Point(615, 345),
				Size = new Size(160, 30),
				TabIndex = 1,
				Text = "Close",
			};

			btn_close.Click += (sender, e) => Close();

			AcceptButton = btn_close;
			BackColor = Color.FromArgb(208, 208, 208);
			ClientSize = new Size(800, 400);
			Controls.Add(header);
			if (closeButton)
			{
				Controls.Add(btn_close);
			}
			FormBorderStyle = FormBorderStyle.None;
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterScreen;
			TopMost = true;
			ResumeLayout(false);

			ShowDialog();
		}
	}
}
