namespace YugiohGame.GameState
{
    public interface IGameState //state pattern
    {
        void NextState();
        void PreviousState();
        void Update();


    }
}
