using Microsoft.Data.Sqlite;

namespace server_app.databases
{
    // handles all requests to the SQL database
    public static class @database
    {
        private static readonly string dbPath = "";
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
                string query = "INSERT INTO userData VALUES()"; //             update once db created                ------------------------------------------------------
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@password", password);

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
    }
}
