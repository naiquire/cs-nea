using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_app.games
{
    // accuracy
    public partial class accuracy : Form // change to main once completed
    {
        public void queueAccuracy()
        {
            connection.SendAsync("queueGame", "accuracy", userData.userID);
        }
        public void join_accuracy()
        {
            // load lobby or something idk its offline
        }
        public void start_accuracy()
        {
            // load game
            InitializeComponent(); // initialiseAccuracy
        }
        public void round(char letter)
        {

        }
    }
}
