using client_app.components;
using client_app.menus;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading;
using System.Windows.Forms;

namespace client_app.games
{
    // accuracy
    public partial class accuracy : abstractMenu
    {
        private main main;
        public static async void queue_accuracy()
        {
            var loading = new queueing("Queueing <Accuracy>");
            //await main.connection.InvokeAsync("queueGame", "accuracy", main.userData.userID);
            //Thread.Sleep(5000);
            loading.close();
        }
        public static void join_accuracy()
        {
            
        }
        public void start_accuracy(main main)
        {
            // load game
            this.main = main;
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
