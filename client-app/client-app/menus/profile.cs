using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.menus
{
    public partial class profile : abstractMenu
    {
        private readonly main main;
        private readonly userData userData;
        public profile(main main, userData user)
        {
            this.main = main;
            userData = user;

            InitializeComponent();
        }
    }
}
