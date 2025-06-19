using System.Windows.Forms;

namespace client_app.menus
{
    public class profile : Form
    {
        private Panel panel;
        public profile(Panel main)
        {
            panel = main;
        }
        public Panel applyControls(userData userData)
        {
            panel.SuspendLayout();

            panel.Controls.Clear();
            // add profile control stuff here

            panel.ResumeLayout();
            return panel;
        }
    }
}
