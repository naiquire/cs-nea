using client_app.menus;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace client_app.games
{
    // accuracy
    public partial class accuracy : abstractMenu
    {
        private main main;
        private abstractMenu menu;
        public static async void queue_accuracy()
        {
            await main.connection.InvokeAsync("queueGame", "accuracy", main.userData.userID);
        }
        public static void join_accuracy()
        {
            // load lobby or something idk its offline
        }
        public void start_accuracy(main main)
        {
            // load game
            this.main = main;
            this.menu = main;
            InitializeComponent(); // initialiseAccuracy
        }
        public static async void round_accuracy(char letter)
        {
            // load input class and return the array drawn
            double[] input = null;

            await main.connection.InvokeAsync("receiveSubmission", "accuracy", main.userData.userID, input);
        }
    }
}
