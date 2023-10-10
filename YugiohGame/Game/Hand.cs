using YugiohGame.Game.CardComponent.Card;
namespace YugiohGame.Game
{
    public class Hand
    {
        private List<CardObject> _handCards;
        private String _playerHand;
        public List<CardObject> HandCards
        {
            get { return _handCards; }
        }
        public String getPlayersHand
        {
            get { return _playerHand; }
        }

        public Hand(Player player)
        {
            _handCards = new List<CardObject>();
            _playerHand = player.PlayerName;
        }

        public void addCardToHand(CardObject card)
        {
            if (HandCards.Count < 7)
            {
                HandCards.Add(card);
            }
        }

        public void removeCardFromHand(CardObject card)
        {
            for (int i = 0; i < HandCards.Count; i++)
            {
                if (HandCards[i] == card)
                {
                    HandCards.Remove(card);
                }
            }
        }
    }
}
