namespace server_app.connections
{
    public static class connectionMapping
    {
        public static Dictionary<string, connectionMap> map = [];
        public static void addAccount(string userID, string connectionID)
        {
            if (map.TryGetValue(userID, out connectionMap connection))
            {
                connection.account = connectionID;
            }
            else
            {
                map.Add(userID, new connectionMap() { account = connectionID });
            }
        }
        public static void addQueueing(string userID, string connectionID)
        {
            if (map.TryGetValue(userID, out connectionMap connection))
            {
                connection.account = connectionID;
            }
            else
            {
                map.Add(userID, new connectionMap() { queuing = connectionID });
            }
        }
        public static void addSocial(string userID, string connectionID)
        {
            if (map.TryGetValue(userID, out connectionMap connection))
            {
                connection.account = connectionID;
            }
            else
            {
                map.Add(userID, new connectionMap() { social = connectionID });
            }
        }
    }
    public struct connectionMap
    {
        public string account;
        public string queuing;
        public string social;
    }
}
