using YugiohGame.Game.CardComponent.Card;
namespace YugiohGame.Game
{
    public class Field
    {
        private String _fieldSide;
        private List<MonsterCard> _monsterZone;
        private List<SpellCard> _spellZone;
        private List<SpellCard> _fieldSpellZone;
        private Graveyard _graveyard;
        private String _phase;

        public String FieldSide
        {
            get { return _fieldSide; }
        }
        public List<MonsterCard> getMonsters
        {
            get { return _monsterZone; }
        }
        public List<SpellCard> getSpells
        {
            get { return _spellZone; }
        }

        public String Phase
        {
            get { return _phase; }
            set { _phase = value; }
        }

        public Graveyard Graveyard
        {
            get { return _graveyard; }
        }
        public Field(Player player, Deck extraDeck, Deck MainDeck)
        {
            _fieldSide = player.PlayerName;
            _monsterZone = new List<MonsterCard>();
            _spellZone = new List<SpellCard>();
            _fieldSpellZone = new List<SpellCard>();
            _graveyard = new Graveyard();
            _phase = "Main Phase 1";

        }

        public void setMonster(MonsterCard monsterCard)
        {
            if (_monsterZone.Count < 6)
            {
                _monsterZone.Add(monsterCard);
            }
        }


        public void setSpell(SpellCard spellCard)
        {
            if (_spellZone.Count < 6)
            {
                _spellZone.Add(spellCard);
            }
        }



        public void RemoveMonster(MonsterCard monsterCard)
        {
            _monsterZone.Remove(monsterCard);
        }

        public void RemoveSpell(SpellCard spellCard)
        {
            _spellZone.Remove(spellCard);
        }
        public void AddToGraveyard(CardObject Card)
        {
            Graveyard.addCardToList(Card);
        }
        public void RemoveFromGraveyard(CardObject Card)
        {
            Graveyard.RemoveOneCard(Card);
        }
    }
}
