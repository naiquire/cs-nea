using System.Reflection;

namespace server_app.games
{
    // contains all instances of running MULTIPLAYER games
    public static class @queueing
    {
        public struct currentGames
        {
            public static List<accuracy> accuracy = [];
            // public List<game2> 
            // etc
        }
        public static currentGames games;
        public static void queue_accuracy(string userID)
        {
            foreach (var game in currentGames.accuracy)
            {
                // check if space available
            }


        }
    }
}
