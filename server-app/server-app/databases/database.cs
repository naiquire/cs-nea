using Microsoft.Data.Sqlite;

namespace server_app.databases
{
    public class @database
    {
        private static readonly string dbPath = "";
        private SqliteConnection connection = new SqliteConnection(dbPath);
    }
}
