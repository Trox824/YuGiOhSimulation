using YugiohGame.Game.CardComponent.AbstractFactory;
using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.CardLibrary
{
    public class TextLibary
    {

        private List<CardObject> _cardLibrary;
        private ICardFactory _factory;
        private DataSetProcessor _processor;
        private List<string[]> _cards;

        public List<CardObject> CardLibrary
        {
            get { return _cardLibrary; }
        }

        public TextLibary(string filePath)
        {
            _processor = new DataSetProcessor();
            _cardLibrary = new List<CardObject>();
            _cards = _processor.ReadMonstersFromCsv(filePath);
            for (int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i][2] == "NormalMonster")
                {
                    _factory = new MonsterCardFactory();
                }
                else if (_cards[i][2] == "SpellCard")
                {
                    _factory = new SpellCardFactory();
                }

                CardObject card = _factory.CreateCard(_cards[i]);

                _cardLibrary.Add(card);
            }
        }
    }
}