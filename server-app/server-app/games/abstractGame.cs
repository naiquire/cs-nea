namespace server_app.games
{
    public struct @stats
    {
        public int correct;
        public int epochs;
        public List<TimeSpan> time;
    }
    public abstract class abstractGame
    {
        protected List<string> userIDs;
        protected int maxPlayers;

        protected stats stats;
        // some kind of structure saving the progress

        public abstractGame(List<string> userIDs)
        {
            this.userIDs = userIDs;
        }
        public virtual void startGame()
        {
            // generic stuff for any game
 
        }
        public int getMaxPlayers() => maxPlayers;


    }
    
}
