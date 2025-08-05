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

        public input(Panel panel, (int, int) pos, (int, int) size)
        {
            this.panel = panel;
            loadPanel(pos, size);
        }

        /// <summary>
        /// Enables the input panel for drawing and attaches mouse event handlers.
        /// </summary>
        public void enablePanel()
        {
            drawing = new Bitmap(panel_input.Width, panel_input.Height, panel_input.CreateGraphics());
            clearPanel();

            panel_input.MouseDown += panel_MouseDown;
            panel_input.MouseUp += panel_MouseUp;
            panel_input.MouseMove += panel_MouseMove;
        }

        /// <summary>
        /// Disables user interaction with the input panel and returns the current drawing.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> representing the current drawing on the panel.</returns>
        public Bitmap disablePanel()
        {
            panel_input.MouseDown -= panel_MouseDown;
            panel_input.MouseUp -= panel_MouseUp;
            panel_input.MouseMove -= panel_MouseMove;

            return drawing;
        }

        /// <summary>
        /// Clears the panel of any drawing.
        /// </summary>
        public void clearPanel()
        {
			Graphics.FromImage(drawing).Clear(Color.White);
			panel_input.CreateGraphics().DrawImageUnscaled(drawing, new Point(0, 0));
		}

		/// <summary>
		/// Creates and adds a new panel to the main container at the specified location and size.
		/// </summary>
		/// <param name="coords"></param>
		/// <param name="size"></param>
		public void loadPanel((int x, int y) coords, (int x, int y) size)
        {
            panel_input = new Panel()
            {
                Location = new Point(coords.x, coords.y),
                Size = new Size(size.x, size.y),
                Name = "panel_input",
            };

            panel.Controls.Add(panel_input);
        }

        /// <summary>
        /// Handles the <see cref="Control.MouseDown"></see> event for the drawing panel./>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;

            pos.x = e.X;
            pos.y = e.Y;
        }

        /// <summary>
        /// Handles the <see cref="Control.MouseUp"></see> event for the drawing panel./>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
        }

		/// <summary>
		/// Handles the <see cref="Control.MouseMove"></see> event for the drawing panel./>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                Graphics panel = Graphics.FromImage(drawing);
                Pen pen = new Pen(Color.Black, 32)
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
