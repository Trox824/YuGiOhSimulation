using SplashKitSDK;
using YugiohGame.Game;
using YugiohGame.GUI;

namespace YugiohGame.GameState
{
    public class InGameState : IGameState
    {
        private InGameGUI _gui;
        private Point2D _point;
        
        private int _phaseCount;
        private bool _isAbleToAction;
        private int _turnCount;

        public InGameGUI GUI
        {
            get { return _gui; }
        }

        public InGameState(Window window)
        {
            _gui = new InGameGUI();
            _gui.Game.StartNewGame();
            _point = new Point2D();
            GUI.Draw(_point);
            _phaseCount = 0;
        }

        public void NextState()
        {
        }

        public void PreviousState()
        {
            //return to chosen seed state
        }

        public void Update()
        {
            SplashKit.ProcessEvents();
            SplashKit.RefreshScreen(60);

            _point.X = SplashKit.MouseX();
            _point.Y = SplashKit.MouseY();
            GUI.Draw(_point);
            if (GUI.Game.DuelState == DuelState.Normal)
            {
                if (SplashKit.KeyTyped(KeyCode.TabKey))
                {
                    GUI.Game.Player.endPhase(GUI.Game.Player.PlayerField.Phase);
                    _phaseCount++;
                    if (_phaseCount == 3)
                    {
                        GUI.Game.switchPlayer();
                        _phaseCount = 0;
                        _turnCount++;
                    }
                    if (_turnCount != 0)
                        GUI.Game.Player.AbleToAttack = true;
                }
                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    GUI.Game.QueueCommand.Add("Summon");
                    GUI.Game.ExecuteQueueCommand();
                }
                if (SplashKit.KeyTyped(KeyCode.AKey))
                {
                    GUI.Game.QueueCommand.Add("Attack");
                    GUI.Game.ExecuteQueueCommand();
                }
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    GUI.Game.QueueCommand.Add("SetAndChangeMode");
                    GUI.Game.ExecuteQueueCommand();
                }
                if (SplashKit.KeyTyped(KeyCode.DKey))
                {
                    GUI.Game.QueueCommand.Add("ActivateEffect");
                    GUI.Game.ExecuteQueueCommand();
                }

            }
            else if(GUI.Game.DuelState == DuelState.ChoosingMonsterCard)
            {
                if (SplashKit.KeyTyped(KeyCode.LeftShiftKey))
                {
                    GUI.Game.QueueCommand.Add("AddCard");
                    GUI.Game.ExecuteQueueCommand();
                }
            }




        }

    }
}