using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using client_app.games;
using client_app.menus;
using Microsoft.AspNetCore.SignalR.Client;

namespace client_app
{
    public struct userData
    {
        public string userID;
        public List<friendData> friends;

        public string localisation;

        public int rank;
        public double accuracy;
    }
    public struct friendData
    {
        public string userID;
        public bool online;

        public int rank;
        public double accuracy;
    }
    public struct game
    {
        public static string gameID;
        public static string type;
        public static List<string> users;
    }
    public partial class main : Form
    {
        public static HubConnection connection;
        public static userData userData;
        public static readonly string address = "http://86.11.15.228:5252/cs-nea";

        //private Bitmap drawing;
        public main(string userID)
        {
            userData = new userData()
            {
                userID = userID
            };


            /// <summary>
            /// this is a possible way of implementing localisation efficiently
            Dictionary<string, Dictionary<string, string>> localisation = new Dictionary<string, Dictionary<string, string>>();
            /// 
            /// first string is the word in english
            /// the second dictionary stores the translations in the form (language, translation)
            /// 
            /// for example if the localisation is set to french:
            /// <code>
            /// localisation["friends"]["french"]
            /// </code>
            /// "amis" would be outputted
            /// 
            /// </summary>


            InitializeComponent();
            initialiseConnection();
        }
        private async void initialiseConnection()
        {
            connection = hub_connection.configConnection(address + "/connections");
            connection = hub_connection.addHandles(connection);
            connection = hub_connection.startConnection(connection);

            userData = await connection.InvokeAsync<userData>("clientConnected", userData.userID);
        }

        private async Task requestProfile(string userID)
        {
            userData user = await connection.InvokeAsync<userData>("requestProfile", userID);

            profile profile = new profile(panel_main);
            panel_main = profile.applyControls(user);
        }

        private void btn_queueAccuracy_Click(object sender, EventArgs e)
        {
            accuracy.queue_accuracy();
        }
    }
}
