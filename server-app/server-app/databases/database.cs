using Microsoft.Data.Sqlite;

namespace server_app.databases
{
    // handles all requests to the SQL database
    public class @database
    {
        private static readonly string dbPath = "";
        private SqliteConnection connection = new(dbPath);
    }
}
