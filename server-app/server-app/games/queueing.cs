using System.Reflection;

namespace server_app.games
{
    // contains all instances of running MULTIPLAYER games
    public static class @queueing
    {
        struct currentGames
        {
            public static List<accuracy> accuracy = [];
            // public List<game2> 
            // etc
        }
        public static void queueGame(string userID, string gameID)
        {
            // queue into game



            // format gameID
            gameID = $"server_app.games.{gameID}";

            // get class object from string input
            var assembly = Assembly.GetCallingAssembly();
            var type = assembly.GetType(gameID) ?? throw new Exception($"gameID < {gameID} > could not be found");

            // call constructor method
            ConstructorInfo[] constructor = type.GetConstructors();
            object @class = constructor[0].Invoke([userID]);

        }
    }
}
