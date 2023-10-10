using YugiohGame.Game.CardComponent;
using YugiohGame.Game.CardComponent.Card;
using YugiohGame.Game.SummonStrategy;

namespace YugiohGame.Game
{
    public class Player
    {
        private string _name;
        public int _lifePoints;
        private Hand _hand;
        private Deck _extraDeck;
        private Deck _mainDeck;
        public Field _field;
        private bool _monsterSummoned;
        private bool _ableToAttack;
        private ISummonStrategy _summonStrategy;
        public bool AbleToAttack
        {
            get { return _ableToAttack; }
            set { _ableToAttack = value; }
        }
        public string PlayerName
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool MonsterSummoned
        {
            get { return _monsterSummoned; }
            set { _monsterSummoned = value; }
        }

        public Hand PlayerHand
        {
            get { return _hand; }
        }

        public Deck ExtraDeck
        {
            get { return _extraDeck; }
        }

        public Deck MainDeck
        {
            get { return _mainDeck; }
        }

        public Field PlayerField
        {
            get { return _field; }
        }

        public int LifePoints
        {
            get { return _lifePoints; }
            set { _lifePoints = value; }
        }

        public Player(string name, int lifePoints, string mainDeckPath, string extraDeckPath)
        {
            _name = name;
            _lifePoints = lifePoints;
            _hand = new Hand(this);
            _extraDeck = new Deck(this, extraDeckPath);
            _mainDeck = new Deck(this, mainDeckPath);
            _field = new Field(this, _extraDeck, _mainDeck);
            _monsterSummoned = false;
            _ableToAttack = false;

        }

        public bool SelectCardInHand(CardObject card)
        {
            if (PlayerHand.HandCards.Contains(card))
            {
                return true;
            }
            return false;
        }

        public bool SelectMonsterCardOnField(MonsterCard card)
        {
            if (PlayerField.getMonsters.Contains(card))
            {
                return true;
            }
            return false;
        }

        public bool SelectSpellCardOnField(SpellCard card)
        {
            if (PlayerField.getSpells.Contains(card))
            {
                return true;
            }
            return false;
        }

        //Cards' actions

        public ISummonStrategy SummonMonsterStrategy
        {
            get
            {
                return _summonStrategy;
            }
        }

        public void SetSummonMonsterStrategy(ISummonStrategy summonStrategy)
        {
            _summonStrategy = summonStrategy;
        }

        public void SummonMonster(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, int slotNumber, string mode)
        {
            _summonStrategy.Summon(monsterCard, tributeMonsters, this, mode);
        }

        public void Attack(MonsterCard monsterCard, MonsterCard targetedMonsterCard, Player opponent)
        {
            monsterCard.HasAttacked = true;

            if (monsterCard.Mode == "Attack" && targetedMonsterCard.Mode == "Attack")
            {
                if (monsterCard.Attack > targetedMonsterCard.Attack)
                {
                    Console.WriteLine("1");
                    opponent.PlayerField.RemoveMonster(targetedMonsterCard);
                    opponent.PlayerField.AddToGraveyard(targetedMonsterCard);
                    opponent.LifePoints = opponent.LifePoints - (monsterCard.Attack - targetedMonsterCard.Attack);
                }
                if (monsterCard.Attack < targetedMonsterCard.Attack)
                {
                    Console.WriteLine("2");
                    PlayerField.RemoveMonster(monsterCard);
                    PlayerField.AddToGraveyard(monsterCard);
                    this.LifePoints = this.LifePoints - (targetedMonsterCard.Attack - monsterCard.Attack);
                }
                if(monsterCard.Attack == targetedMonsterCard.Attack)
                {
                    opponent.PlayerField.RemoveMonster(targetedMonsterCard);
                    this.PlayerField.RemoveMonster(monsterCard);
                }
            }
            if (monsterCard.Mode == "Attack" && targetedMonsterCard.Mode == "Defence")
            {
                targetedMonsterCard.FaceDown = false;
                if (monsterCard.Attack >= targetedMonsterCard.Defence)
                {
                    opponent.PlayerField.RemoveMonster(targetedMonsterCard);
                    opponent.PlayerField.AddToGraveyard(targetedMonsterCard);
                }
                if (monsterCard.Attack < targetedMonsterCard.Defence)
                {
                    PlayerField.RemoveMonster(monsterCard);
                    PlayerField.AddToGraveyard(monsterCard);
                    this.LifePoints = this.LifePoints - (targetedMonsterCard.Attack - monsterCard.Attack);
                }
            }
        }

        public void DirectAttack(MonsterCard monsterCard, Player opponent)
        {

            monsterCard.HasAttacked = true;
            opponent.LifePoints = opponent.LifePoints - (monsterCard.Attack);
        }

        public void SwitchMonsterMode(MonsterCard monsterCard)
        {
            monsterCard.HasSwitchedMode = true;
            if (monsterCard.Mode == "Attack")
            {
                monsterCard.Mode = "Defense";
            }
            else
            {
                monsterCard.Mode = "Attack";
            }
        }
        public void SetSpell(SpellCard card)
        {
            card.FaceDown = true;
            PlayerField.setSpell(card);
            PlayerHand.HandCards.Remove(card);
        }
        public CardObject DrawCard()
        {
            if (PlayerHand.HandCards.Count <= 7)
            {
                CardObject card = MainDeck.DrawOneCard();
                PlayerHand.addCardToHand(card);
                return card;
            }
            else
            {
                throw new Exception();
            }
        }

        public void endPhase(String phase)
        {
            if (phase == "Main Phase 1")
            {
                PlayerField.Phase = "Battle Phase";
            }
            else if (phase == "Battle Phase")
            {
                PlayerField.Phase = "Main Phase 2";
            }
            else if (phase == "Main Phase 2")
            {
                endTurn();
            }
        }
        public void ActivateCardEffect(IHaveEffect card, GameMechanic game)
        {
            card.ExecuteEffect(game);
            PlayerHand.HandCards.Remove(card as CardObject);
            PlayerField.getSpells.Remove(card as SpellCard);
            PlayerField.Graveyard.addCardToList(card as CardObject);
        }
        public void endTurn()
        {
            MonsterSummoned = false;
            PlayerField.Phase = "Main Phase 1";
            for (int i = 0; i < PlayerField.getMonsters.Count(); i++)
            {
                PlayerField.getMonsters[i].HasAttacked = false;
                PlayerField.getMonsters[i].HasSwitchedMode = false;
            }
            DrawCard();
        }
    }
}