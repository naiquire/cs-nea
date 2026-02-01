using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace client_app.components
{
	public class ConfirmForm : Form
	{
		private readonly Guna2TextBox header;
		private readonly Guna2GradientButton btn_accept;
		private readonly Guna2GradientButton btn_cancel;

		public ConfirmForm(string text)
		{
			SuspendLayout();

			header = new Guna2TextBox()
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
				Size = new Size(360, 100),
				TabIndex = 0,
				TabStop = false,
			};
			btn_accept = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 14,
				FillColor = Color.FromArgb(247, 113, 163),
				FillColor2 = Color.FromArgb(197, 113, 247),
				Font = new Font("Bahnschrift", 9.75F),
				ForeColor = Color.White,
				Location = new Point(215, 145),
				Size = new Size(160, 30),
				TabIndex = 1,
				Text = Languages.localisation["Accept"][Main.userData.localisation],
			};
			btn_cancel = new Guna2GradientButton()
			{
				AutoRoundedCorners = true,
				BorderRadius = 14,
				FillColor = Color.FromArgb(156, 156, 156),
				FillColor2 = Color.FromArgb(156, 156, 156),
				Font = new Font("Bahnschrift", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0),
				ForeColor = Color.White,
				Location = new Point(25, 145),
				Size = new Size(160, 30),
				TabIndex = 1,
				Text = "Ignore",
			};

			btn_accept.Click += (sender, e) =>
			{
				DialogResult = DialogResult.OK;
				Close();
			};
			btn_cancel.Click += (sender, e) =>
			{
				DialogResult = DialogResult.Cancel;
				Close();
			};

			AcceptButton = btn_accept;
			BackColor = Color.FromArgb(208, 208, 208);
			CancelButton = btn_cancel;
			ClientSize = new Size(400, 200);
			Controls.Add(header);
			Controls.Add(btn_accept);
			Controls.Add(btn_cancel);
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
