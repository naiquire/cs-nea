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
    }
    public partial class main : Form
    {
        private HubConnection connection;
        private userData userData;
        public static readonly string address = "https://86.11.15.228:5252/cs-nea";
        public main(string userID)
        {
            userData = new userData()
            {
                userID = userID
            };


            initialiseComponent();
            initialiseConnection();
        }
        private async void initialiseConnection()
        {
            connection = hub_connection.configConnection();
            connection = hub_connection.addHandles(connection);
            connection = hub_connection.startConnection(connection);

            await connection.SendAsync("clientConnected", userData.userID);
        }
    }
}
