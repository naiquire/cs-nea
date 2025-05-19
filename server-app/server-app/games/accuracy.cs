namespace server_app.games
{
    // 1 player game
    // measures time and accuracy only, basically training???
    public class @accuracy : abstractGame
    {
        public const bool online = false;
        public accuracy(List<string> userIDs) : base(userIDs)
        {
            
            startGame();
        }
        public override void startGame()
        {
            base.startGame();
            
            for (int i = 0; i < 10; i++)
            {
                // 10 rounds

            }

        }
    }
}
