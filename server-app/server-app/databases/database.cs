using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Data.Sqlite;

namespace server_app.databases
{
    // handles all requests to the SQL database
    public struct userData
    {
        public string userID;
        public Dictionary<char, (double, TimeSpan)> statistics;
        public string aboutMe;
        public DateTime dateCreated;
        public int rank;
    }
    public static class @database
    {
        private static readonly string dbPath = @"Data Source=C:\Users\boyss\Documents\General\Relay\github\cs-nea-app\server-app\server-app\databases\maindb.sqlite";
        private static readonly SqliteConnection connection = new(dbPath);

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
                string query = "INSERT INTO userData VALUES(@userID, @password, @aboutMe, @dateCreated, @rank)";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@aboutMe", "");
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
            if (!userExists(userID))
            {
                return null;
            }

            userData userData = new();
            Dictionary<char, (double, TimeSpan)> statistics = [];

            string query = "SELECT aboutMe, dateCreated, rank, localisation FROM userData WHERE userData.userID = @userID";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userID", userID);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userData.aboutMe = reader.GetString(0);
                    userData.dateCreated = reader.GetDateTime(2);
                    userData.rank = reader.GetInt32(3);
                }
            }

            query = "SELECT letter, accuracy, time FROM statistics WHERE userID = @userID";
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userID", userID);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    char letter = reader.GetChar(0);
                    double accuracy = reader.GetDouble(1);
                    TimeSpan time = reader.GetTimeSpan(2);

                    statistics[letter] = (accuracy, time);
                }
            }

            userData.userID = userID;
            userData.statistics = statistics;
            return userData;
        }
    }
}
