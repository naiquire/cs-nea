using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace client_app.components
{
	public class input : Form
	{
		private readonly Panel panel_base;

		private Panel panel_input;
		private Bitmap drawing;

		private bool draw = false;
		private (int x, int y) pos;

		const int PEN_THICKNESS = 40;

		public Bitmap getDrawing() => drawing;

		public input(Panel panel, (int, int) pos, (int, int) size)
		{
			this.panel_base = panel;
			loadPanel(pos, size);
			clearPanel();
		}

		public void enablePanel()
		{
			clearPanel();

			panel_input.MouseDown += panel_MouseDown;
			panel_input.MouseUp += panel_MouseUp;
			panel_input.MouseMove += panel_MouseMove;
		}

		public void disablePanel()
		{
			panel_input.MouseDown -= panel_MouseDown;
			panel_input.MouseUp -= panel_MouseUp;
			panel_input.MouseMove -= panel_MouseMove;
		}

		public void clearPanel()
		{
			Graphics.FromImage(drawing).Clear(Color.White);
			panel_input.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
		}

		public void loadPanel((int x, int y) coords, (int x, int y) size)
		{
			panel_input = new Panel()
			{
				Location = new Point(coords.x, coords.y),
				Size = new Size(size.x, size.y),
				Name = "panel_input",
			};

			panel_base.Controls.Add(panel_input);

			drawing = new Bitmap(panel_input.Width, panel_input.Height, panel_input.CreateGraphics());
		}

		private void panel_MouseDown(object sender, MouseEventArgs e)
		{
			draw = true;

			pos.x = e.X;
			pos.y = e.Y;
		}

		private void panel_MouseUp(object sender, MouseEventArgs e)
		{
			draw = false;
		}

		private void panel_MouseMove(object sender, MouseEventArgs e)
		{
			if (draw)
			{
				Graphics panel = Graphics.FromImage(drawing);
				Pen pen = new Pen(Color.Black, PEN_THICKNESS)
				{
					EndCap = LineCap.Round,
					StartCap = LineCap.Round
				};

				panel.DrawLine(pen, pos.x, pos.y, e.X, e.Y);
				panel_input.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
			}

			pos.x = e.X;
			pos.y = e.Y;
		}
	}
}
