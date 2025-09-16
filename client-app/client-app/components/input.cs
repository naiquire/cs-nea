using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace client_app.components
{
    public class input : Form
    {
        private readonly Panel panel;

        private Panel panel_input;
        private Bitmap drawing;

        private bool draw = false;
        private (int x, int y) pos;

        const int PEN_THICKNESS = 40;

        public input(Panel panel, (int, int) pos, (int, int) size)
        {
            this.panel = panel;
            loadPanel(pos, size);
            clearPanel(); // forces layout perform
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

            panel.Controls.Add(panel_input);

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

        public double[] ImageToArray()
		{
			Bitmap resize = new Bitmap(drawing, new Size(28, 28));
			int width = resize.Width;
			int height = resize.Height;
			double[] pixels = new double[width * height];

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					Color c = resize.GetPixel(x, y);
					double gray = (0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
					pixels[y * width + x] = 1.0 - (gray / 255.0);
				}
			}

			return pixels;
		}


        public void preprocess(Bitmap original)
        {
            /* crop image to actual drawing
             * take largest dimension and add padding to other dimension such that square
             * shift image either right or down by half the padding added to centre it
             * scale to 24x24
             * add 2 pixels padding all sides
            */

            Bitmap cropped = cropImage(original);

            Bitmap cropImage(Bitmap image)
            {
                int left = image.Width + 1;
                int top = image.Height + 1;
                int right = -1;
                int bottom = -1;

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        if (image.GetPixel(y, x) != Color.White) // found a foreground pixel
                        {
                            if (y < top) top = y;
                            if (y > bottom) bottom = y;
                            if (x < left) left = x;
                            if (x > right) right = x;
                        }
                    }
                }

                return image.Clone(new Rectangle(left, top, right - left, bottom - top), image.PixelFormat);
            }

            Bitmap squareCropImage(Bitmap image)
            {
                image.sele
            }
		}
	}
}
