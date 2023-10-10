using SplashKitSDK;
using YugiohGame.GameState;

namespace YugiohGame
{
    public class Program
    {
        public static void Main()
        {
            Window myWindow = new Window("Yu-Gi-Oh Master Duel!", 1500, 800);
            while (!myWindow.CloseRequested)
            {
                GameContext.GetGameInstance(myWindow).Update();
            }
        }
    }
}
