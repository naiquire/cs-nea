using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace client_app.games
{
    // accuracy
    public partial class accuracy : Form // change to main once completed
    {
        public async void queueAccuracy()
        {
            await main.connection.InvokeAsync("queueGame", "accuracy", main.userData.userID);
            join_accuracy();
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
