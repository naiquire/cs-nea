using Microsoft.Data.Sqlite;

namespace server_app.databases
{
    // handles all requests to the SQL database
    public struct userData
    {
        public double accuracy;
        public TimeSpan time;
        public string aboutMe;
        public DateTime dateCreated;
        public int rank;
    }
    public static class @database
    {
        private static readonly string dbPath = @"C:\Users\boyss\Documents\General\Relay\github\cs-nea-app\server-app\server-app\databases\maindb.sqlite, Version=3";
        private static SqliteConnection connection = new(dbPath);

        public static int loginRequest(string userID, string password)
        {
            string query = "SELECT userData.password FROM userData WHERE userData.userID = @userID";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userID", userID);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetString(0) == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                // account does not exist
                return -1;
            }
        }
        public static bool accountRequest(string userID, string password)
        {
            if (!userExists(userID))
            {
                string query = "INSERT INTO userData VALUES(@userID, @password, @accuracy, @time, @aboutMe, @dateCreated, @rank)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@accuracy", null);
                    command.Parameters.AddWithValue("@time", null);
                    command.Parameters.AddWithValue("@aboutMe", null);
                    command.Parameters.AddWithValue("@dateCreated", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@rank", 300);

                    command.ExecuteNonQuery();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool userExists(string userID)
        {
            string query = "SELECT userData.userID FROM userData WHERE userData.userID = @userID";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userID", userID);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    return true;
                }
                return false;
            }
        }
        public static userData? loadUserData(string userID)
        {
            string query = "SELECT * FROM userData WHERE userData.userID = @userID";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userID", userID);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    return new userData()
                    {
                        accuracy = reader.GetDouble(0),
                        time = reader.GetTimeSpan(1),
                        aboutMe = reader.GetString(2),
                        dateCreated = reader.GetDateTime(3),
                        rank = reader.GetInt32(4),
                    };
                }
            }
            return null;
        }
    }
}
