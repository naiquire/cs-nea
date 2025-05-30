using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    public partial class main : Form
    {
        public HubConnection connection;
        public static userData userData;
        public static readonly string address = "http://86.11.15.228:5252/cs-nea";
        //public static readonly string address = "http://192.168.0.251:3900/cs-nea";
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

            await connection.SendAsync("clientConnected", userData.userID);
        }

        private async Task requestProfile(string userID)
        {
            await connection.SendAsync("requestProfile", userID);
        }
    }
}
