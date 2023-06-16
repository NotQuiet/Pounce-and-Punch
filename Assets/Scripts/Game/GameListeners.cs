namespace Game
{
    public class GameListeners
    {
        public interface IGameListener
        {
            
        }
        public interface IStartGameListener : IGameListener
        {
            void OnStartGame();
        }
        
        public interface IFinishGameListener : IGameListener
        {
            void OnFinishGame();
        }
    }
}