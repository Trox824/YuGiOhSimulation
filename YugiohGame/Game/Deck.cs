using SplashKitSDK;
using YugiohGame.CardLibrary;
using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game
{
    public class Deck
    {
        private TextLibary _library;
        private static Random rng = new Random();
        private List<CardObject> _cardList;
        private Bitmap _bitmap;

        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public List<CardObject> CardList
        {
            get { return _cardList; }
            set { _cardList = value; }
        }

        public Deck(Player player, string deckPath)
        {
            _library = new TextLibary(deckPath);
            _cardList = new List<CardObject>();
            _cardList = _library.CardLibrary;

            _bitmap = new Bitmap("DownsideCard", "CardDownSide.png");
        }

        public CardObject DrawOneCard()
        {
            CardObject Card = this.RemoveOneCard(CardList.First());
            return Card;
        }

        public void Shuffle()
        {
            var rnd = new Random();
            var randomized = _cardList.OrderBy(item => rnd.Next());
        }

        public virtual void addCardToList(CardObject card)
        {
            _cardList.Add(card);
        }

        public CardObject RemoveOneCard(CardObject Card)
        {
            _cardList.Remove(Card);
            return Card;
        }

        public void Draw(float x, float y)
        {
            for (float i = 0; i < _cardList.Count; i++)
            {
                SplashKit.DrawBitmap(Bitmap, x, y);
            }
        }
    }
}