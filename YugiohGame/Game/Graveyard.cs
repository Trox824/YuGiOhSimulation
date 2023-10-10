using SplashKitSDK;
using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game
{
    public class Graveyard
    {
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
        public Graveyard()
        {
            _cardList = new List<CardObject>();
            _bitmap = new Bitmap("DownsideCard", "CardDownSide.png");
        }

        public void addCardToList(CardObject card)
        {
            _cardList.Add(card);
        }

        public CardObject RemoveOneCard(CardObject Card)
        {
            _cardList.Remove(Card);
            return Card;
        }
    }
}
