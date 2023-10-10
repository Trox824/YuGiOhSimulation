using SplashKitSDK;
using YugiohGame.Game;
using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.GUI
{
    public class InGameGUI
    {
        private Point2D _p;
        private GameMechanic _game;
        private CardObject _choosenCard;
        private Bitmap _background;
        private Sprite _backgroundSprite;
        private Font _font;
        
        private Bitmap _cardCover;
        private Sprite _cardCoverSprite;
        
        
        public CardObject ChoosenCard
        {
            get { return _choosenCard; }
            set { _choosenCard = value; }
        }
        

        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public Sprite BackgroundSprite
        {
            get { return _backgroundSprite; }
            set { _backgroundSprite = value; }
        }

        public Point2D P
        {
            get { return _p; }
        }

        public GameMechanic Game
        {
            get { return _game; }
        }

        public InGameGUI()
        {
            _game = new GameMechanic();

            _background = new Bitmap("background", "Background.JPG");
            _backgroundSprite = new Sprite(_background);

            

            _cardCover = new Bitmap("cover", "cover.JPG");
            _cardCoverSprite = new Sprite(_cardCover);
        }

        public void Draw(Point2D point)
        {
            _backgroundSprite.Draw();
            DrawCardsInZone(point);
            DrawHandCard(point);
            DrawInformationPanel();

            
        }

        

        public void DrawHandCard(Point2D point)
        {
            int i = 0;
            int cardSpace = 100;
            foreach (CardObject card in Game.Player.PlayerHand.HandCards)
            {
                SplashKit.SpriteSetScale(card.Sprite, (float)0.5);
                card.X = i * 50 + 1000;
                card.Y = 580;
                card.Sprite.Draw();
                if (SplashKit.SpritePointCollision(card.Sprite, point) && SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Game.ChoosenCard = card;
                    
                }
                    
                i++;
            }
        }

        public void DrawCardsInZone(Point2D point)
        {
            DrawMonsterInPlayerZone(point);
            DrawMonsterInOpponentZone(point);
            DrawSpellInOpponentZone(point);
            DrawSpellInPlayerZone(point);
        }

        public void DrawMonsterInPlayerZone(Point2D point)
        {
            int i = 0;
            foreach (MonsterCard card in Game.Player.PlayerField.getMonsters)
            {
                if (card.FaceDown == true)
                    card.UpdateBitmapForSprite(card.DownsideCard);
                if (card.FaceDown == false)
                    card.UpdateBitmapForSprite(card.Bitmap);
                SplashKit.SpriteSetScale(card.Sprite, (float)0.73);
                if (card.Mode == "Defence")
                    SplashKit.SpriteSetRotation(card.Sprite, 270);
                card.X = i * 190 + 50;
                card.Y = 370; // 370 // 570

                if (card.Selected == true && card.Mode == "Attack")
                    SplashKit.FillRectangle(Color.Black, card.X + 20, card.Y + 30, card.Sprite.Width - 42, card.Sprite.Width + 16);
                else if (card.Selected == true && card.Mode == "Defence")
                    SplashKit.FillRectangle(Color.Black, card.X - 9, card.Y + 58, card.Sprite.Width + 16, card.Sprite.Width - 40);
                card.Sprite.Draw();
                if (SplashKit.SpritePointCollision(card.Sprite, point) && SplashKit.MouseClicked(MouseButton.LeftButton))
                    Game.ChoosenCard = card;
                i++;
            }
        }

        public void DrawMonsterInOpponentZone(Point2D point)
        {
            int i = 0;
            foreach (MonsterCard card in Game.Opponent.PlayerField.getMonsters)
            {
                if (card.FaceDown == true)
                    card.UpdateBitmapForSprite(card.DownsideCard);
                if (card.FaceDown == false)
                    card.UpdateBitmapForSprite(card.Bitmap);
                SplashKit.SpriteSetScale(card.Sprite, (float)0.73);
                SplashKit.SpriteSetRotation(card.Sprite, 180);
                if (card.Mode == "Defence")
                    SplashKit.SpriteSetRotation(card.Sprite, 90);

                card.X = i * 190 + 50;
                card.Y = 175; // -25
                if (card.Selected == true && card.Mode == "Attack")
                    SplashKit.FillRectangle(Color.Black, card.X + 20, card.Y + 30, card.Sprite.Width - 42, card.Sprite.Width + 16);
                else if (card.Selected == true && card.Mode == "Defence")
                    SplashKit.FillRectangle(Color.Black, card.X - 9, card.Y + 58, card.Sprite.Width + 16, card.Sprite.Width - 40);
                card.Sprite.Draw();
                if (SplashKit.SpritePointCollision(card.Sprite, point) && SplashKit.MouseClicked(MouseButton.LeftButton))
                    Game.ChoosenCard = card;
                i++;
            }
        }

        public void DrawSpellInPlayerZone(Point2D point)
        {
            int i = 0;
            foreach (SpellCard card in Game.Player.PlayerField.getSpells)
            {
                if (card.FaceDown == true)
                    card.UpdateBitmapForSprite(card.DownsideCard);
                if (card.FaceDown == false)
                    card.UpdateBitmapForSprite(card.Bitmap);
                SplashKit.SpriteSetScale(card.Sprite, (float)0.73);
                card.X = i * 190 + 50;
                card.Y = 570; // 370 // 570
                if (card.Selected == true)
                    SplashKit.FillRectangle(Color.Black, card.X + 20, card.Y + 30, card.Sprite.Width - 42, card.Sprite.Width + 16);
                card.Sprite.Draw();
                if (SplashKit.SpritePointCollision(card.Sprite, point) && SplashKit.MouseClicked(MouseButton.LeftButton))
                {                  
                    Game.ChoosenCard = card;                    
                }
                    
                i++;
            }
        }

        public void DrawSpellInOpponentZone(Point2D point)
        {
            int i = 0;
            foreach (SpellCard card in Game.Opponent.PlayerField.getSpells)
            {
                if (card.FaceDown == true)
                    card.UpdateBitmapForSprite(card.DownsideCard);
                if (card.FaceDown == false)
                    card.UpdateBitmapForSprite(card.Bitmap);
                SplashKit.SpriteSetScale(card.Sprite, (float)0.73);
                SplashKit.SpriteSetRotation(card.Sprite, 180);
                card.X = i * 190 + 50;
                card.Y = -25; // -25
                if (card.Selected == true)
                    SplashKit.FillRectangle(Color.Black, card.X + 20, card.Y + 30, card.Sprite.Width - 42, card.Sprite.Width + 16);
                card.Sprite.Draw();
                if (SplashKit.SpritePointCollision(card.Sprite, point) && SplashKit.MouseClicked(MouseButton.LeftButton))
                    Game.ChoosenCard = card;
                i++;
            }
        }

        public void DrawInformationPanel()
        {
            if (Game.DuelState == DuelState.Normal)
                SplashKit.DrawText(Game.Player.PlayerField.Phase, Color.Black, "BRAVEEightyone-Regular.ttf", 40, 1125, 10);
            else if (Game.DuelState == DuelState.ChoosingMonsterCard)
                SplashKit.DrawText("Choosing Cards", Color.Black, "BRAVEEightyone-Regular.ttf", 40, 1125, 10);
            else if (Game.DuelState == DuelState.ChoosingTargetMonster)
                SplashKit.DrawText("Chossing Target", Color.Black, "BRAVEEightyone-Regular.ttf", 40, 1125, 10);
            if (Game.ChoosenCard != null)
            {
                Game.ChoosenCard.X = 1050;
                Game.ChoosenCard.Y = 225;
                SplashKit.SpriteSetScale(Game.ChoosenCard.Sprite, (float)1);
                SplashKit.SpriteSetRotation(Game.ChoosenCard.Sprite, 0);
                
                Game.ChoosenCard.Sprite.Draw();
            }

            SplashKit.FillRectangle(Color.White, 1050, 500, 500, 120);

            SplashKit.DrawText($"{Game.Player.PlayerName}: {Game.Player.LifePoints} LP", Color.Red, "BRAVEEightyone-Regular.ttf", 30, 1050, 500);
            SplashKit.DrawText($"Main Deck: {Game.Player.MainDeck.CardList.Count} | Extra: {Game.Player.ExtraDeck.CardList.Count}", Color.Black, "BRAVEEightyone-Regular.ttf", 30, 1050, 540);
            SplashKit.DrawText($"Graveyard: {Game.Player.PlayerField.Graveyard.CardList.Count}", Color.Black, "BRAVEEightyone-Regular.ttf", 30, 1050, 580);

            SplashKit.FillRectangle(Color.White, 1050, 70, 500, 120);
            SplashKit.DrawText($"{Game.Opponent.PlayerName}: {Game.Opponent.LifePoints} LP", Color.Red, "BRAVEEightyone-Regular.ttf", 30, 1050, 70);
            SplashKit.DrawText($"Main Deck: {Game.Opponent.MainDeck.CardList.Count} | Extra: {Game.Opponent.ExtraDeck.CardList.Count}", Color.Black, "BRAVEEightyone-Regular.ttf", 30, 1050, 110);
            SplashKit.DrawText($"Graveyard: {Game.Opponent.PlayerField.Graveyard.CardList.Count}", Color.Black, "BRAVEEightyone-Regular.ttf", 30, 1050, 150);
        }
        public void DrawChoosingCardState()
        {


        }
        public void Update()
        {

        }

    }
}