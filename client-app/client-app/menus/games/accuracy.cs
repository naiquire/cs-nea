using client_app.menus;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;

namespace client_app.games
{
    // accuracy
    public partial class accuracy : abstractMenu
    {
        public static async void queue_accuracy()
        {
            await main.connection.InvokeAsync("queueGame", "accuracy", main.userData.userID);
            join_accuracy();
        }
        public static void join_accuracy()
        {
            // load lobby or something idk its offline
        }
        public void start_accuracy()
        {
            // load game
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
