using SplashKitSDK;
using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game
{
    public abstract class CardHolder
    {
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
        public CardHolder()
        {
            _cardList = new List<CardObject>();
            _bitmap = new Bitmap("DownsideCard", "CardDownSide.png");
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

        public virtual CardObject RemoveOneCard(CardObject Card)
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
