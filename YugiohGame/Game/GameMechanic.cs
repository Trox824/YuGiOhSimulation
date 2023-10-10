using System.Diagnostics.Tracing;
using YugiohGame.Game.CardComponent;
using YugiohGame.Game.CardComponent.Card;
using YugiohGame.Game.SummonStrategy;

namespace YugiohGame.Game
{
    public enum DuelState
    {
        Normal,
        ChoosingMonsterCard,
        ChoosingSpellCard,
        ChoosingTargetMonster,
        ChoosingTargetSpell

    }
    public class GameMechanic : EventListener
    {
        private DuelState _duelState;
        private Player _player;
        private Player _opponent;
        private Player _currentPlayer;
        private Player _winner;
        private CardObject _choosenCard;
        private List<MonsterCard> _choosenMonsterCardList;
        private List<SpellCard> _choosenSpellCardList;
        private List<MonsterCard> _LocationChoosingMonsterCard;
        private List<SpellCard> _LocationChoosingSpellCardList;
        private int _numberOfNeededCards;
        private MonsterCard _monsterCardTemp;
        
        private List<string> _queueCommand;
        public List<string> QueueCommand
        {
            get { return _queueCommand; }
            set { _queueCommand = value; }
        }
        
        public MonsterCard MonsterCardTemp
        {
            get { return _monsterCardTemp; }
            set { _monsterCardTemp = value; }
        }
        public List<MonsterCard> LocationChoosingMonsterCard
        {
            get { return _LocationChoosingMonsterCard; }
            set { _LocationChoosingMonsterCard = value; }
        }
        public List<SpellCard> LocationChoosingSpellCard
        {
            get { return _LocationChoosingSpellCardList; }
            set { _LocationChoosingSpellCardList = value; }
        }
        public int NumberOfNeededCards
        {
            get { return _numberOfNeededCards; }
            set { _numberOfNeededCards = value; }
        }
        public List<MonsterCard> ChoosenMonsterCardList
        {
            get { return _choosenMonsterCardList; }
            set { _choosenMonsterCardList = value; }
        }
        public DuelState DuelState
        {
            get { return _duelState; }
            set { _duelState = value; }
        }
        public CardObject ChoosenCard
        {
            get { return _choosenCard; }
            set { _choosenCard = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Player Opponent
        {
            get { return _opponent; }
            set { _opponent = value; }
        }

        public Player Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set { _currentPlayer = value; }
        }

        public GameMechanic()
        {
            _duelState = DuelState.Normal;
            _queueCommand = new List<string>();
        }
        public void ExecuteQueueCommand()
        {           
            foreach (string command in QueueCommand.ToList())
            {
                if (command == "SetAndChangeMode")
                    SetAndChangeModeMonster();
                else if (command == "AddCard")
                    ChooseCard();
                else if (command == "Attack")
                {
                    Attack();
                }
                else if (command == "Summon")
                {
                    SummonCard();
                }
                else if (command == "SummonChoosedMonster")
                {
                    SummonTributeMonster();
                }
                else if (command == "AttackChosenMonster")
                {
                    if(ChoosenCard is MonsterCard monsterCard)
                    {
                        AttackChosenMonster(monsterCard);
                    }
                    
                }
                else if (command == "ActivateEffect")
                    ActivateCardEffect();
            }
        }
        public void ActivateCardEffect()
        {
            if (Player.PlayerHand.HandCards.Contains(ChoosenCard) || Player.PlayerField.getSpells.Contains(ChoosenCard))
            {
                if (ChoosenCard is IHaveEffect Card)
                {
                    if (Card.CheckCondition(this) == true)
                    {
                        Player.ActivateCardEffect(Card, this);
                        QueueCommand.Remove("ActivateEffect");
                        
                    }
                }
            }
        }
        public void switchPlayer()
        {
            if (Winner == null)
            {
                CurrentPlayer = Player;
                Player = Opponent;
                Opponent = CurrentPlayer;
            }
        }
        public bool CheckCardSummonCondition(MonsterCard monsterCard)
        {
            if (Player.PlayerField.Phase == "Main Phase 1" || Player.PlayerField.Phase == "Main Phase 2")
                if (Player.MonsterSummoned == false)
                    if (monsterCard.Level <= 4 && Player.PlayerField.getMonsters.Count <= 4)
                        return true;
            return false;
        }
        public bool CheckCardTributeSummonCondition(MonsterCard monsterCard)
        {
            if (Player.PlayerField.Phase == "Main Phase 1" || Player.PlayerField.Phase == "Main Phase 2")
                if (Player.MonsterSummoned == false)
                    if (monsterCard.Level >= 4 && Player.PlayerField.getMonsters.Count <= 4)
                        return true;
            return false;
        }
        public bool CheckChangeModeCondition(MonsterCard monsterCard)
        {
            if (Player.PlayerField.Phase == "Main Phase 1" || Player.PlayerField.Phase == "Main Phase 2")
                foreach (MonsterCard monstercard in Player.PlayerField.getMonsters)
                    if (monsterCard == ChoosenCard)
                        if (monsterCard.HasSwitchedMode == false)
                            return true;
            return false;
        }
        public bool CheckNumberOfTributedMonster(MonsterCard monsterCard)
        {
            MonsterCardTemp = monsterCard;
            if (monsterCard.Level > 4 && monsterCard.Level <= 6)
            {
                NumberOfNeededCards = 2;
            }
            else if (monsterCard.Level > 6 && monsterCard.Level <= 8)
            {
                NumberOfNeededCards = 2;
            }
            else if (monsterCard.Level > 8)
            {
                NumberOfNeededCards = 3;
            }
            if (Player.PlayerField.getMonsters.Count >= NumberOfNeededCards)
            {
                ChoosenMonsterCardList = new List<MonsterCard>();
                LocationChoosingMonsterCard = Player.PlayerField.getMonsters;
                return true;
            }
            else
                return false;
        }
        
        public void SetAndChangeModeMonster()
        {

            if (Player.PlayerHand.HandCards.Contains(ChoosenCard))
            {
                if (ChoosenCard is NormalMonsterCard monsterCard)
                {
                    if (CheckCardSummonCondition(monsterCard) == true)
                    {
                        Player.SetSummonMonsterStrategy(new NormalSetSummonStrategy());
                        Player.SummonMonsterStrategy.Summon(monsterCard, null, Player, "Defence");
                        QueueCommand.Remove("SetAndChangeMode");
                    }
                }
                else if(ChoosenCard is SpellCard spellcard)
                {
                    if (spellcard.CheckCondition(this))
                        Player.SetSpell(spellcard);
                }
            }
            else if (Player.PlayerField.getMonsters.Contains(ChoosenCard))
            {
                if (ChoosenCard is NormalMonsterCard monsterCard)
                {
                    if (CheckChangeModeCondition(monsterCard) == true)
                    {
                        QueueCommand.Remove("SetAndChangeMode");
                        monsterCard.SwitchMode();
                    }
                }
            }

        }
        public void ChooseCard()
        {
            foreach (MonsterCard card in LocationChoosingMonsterCard)
            {
                if (card == ChoosenCard)
                {
                    card.Selected = true;
                    if (!ChoosenMonsterCardList.Contains(card))
                    {
                        ChoosenMonsterCardList.Add(card);
                        QueueCommand.Remove("AddCard");
                    }
                }
            }
            if (ChoosenMonsterCardList.Count == NumberOfNeededCards)
            {
                DuelState = DuelState.Normal;
                ExecuteQueueCommand();
            }

            QueueCommand.Remove("AddCard");
        }
        public void SummonTributeMonster()
        {
            if (NumberOfNeededCards == ChoosenMonsterCardList.Count)
            {
                Player.SetSummonMonsterStrategy(new NormalTributeSummonStrategy());
                Player.SummonMonsterStrategy.Summon(MonsterCardTemp, ChoosenMonsterCardList, Player, "Attack");
                QueueCommand.Remove("SummonChoosedMonster");
            }
                
        }
        public void SummonCard()
        {
            if (Player.PlayerHand.HandCards.Contains(ChoosenCard))
            {
                if (ChoosenCard is MonsterCard monsterCard)
                {
                    if (CheckCardSummonCondition(monsterCard) == true)
                    {
                        Player.SetSummonMonsterStrategy(new NormalSummonStrategy());
                        Player.SummonMonsterStrategy.Summon(monsterCard, null, Player, "Attack");
                        QueueCommand.Remove("Summon");
                    }
                    else if (CheckCardTributeSummonCondition(monsterCard) == true)
                    {
                        if (CheckNumberOfTributedMonster(monsterCard) == true)
                        {
                            
                            QueueCommand.Add("SummonChoosedMonster");
                            DuelState = DuelState.ChoosingMonsterCard;
                        }
                    }
                }
            }

            QueueCommand.Remove("Summon");
        }
        public bool CheckAttackCondition(MonsterCard monsterCard)
        {
            if (this.Player.PlayerField.Phase == "Battle Phase" && this.Player.AbleToAttack == true)
                if (Player.PlayerField.getMonsters.Contains(this.ChoosenCard))
                    if (monsterCard.HasAttacked == false)
                        return true;
            return false;
        }
        public void SetAttackChooseMonsterCondition(MonsterCard monsterCard)
        {
            MonsterCardTemp = monsterCard;
            NumberOfNeededCards = 1;
            ChoosenMonsterCardList = new List<MonsterCard>();
            LocationChoosingMonsterCard = Opponent.PlayerField.getMonsters;
        }
        public void AttackChosenMonster(MonsterCard monsterCard)
        {
            if (NumberOfNeededCards == ChoosenMonsterCardList.Count)
            {
                Player.Attack(MonsterCardTemp, monsterCard, Opponent);
                monsterCard.Selected = false;
                QueueCommand.Remove("AttackChosenMonster");
                DuelState = DuelState.Normal;
            }
        }
        public void Attack()
        {
            if (ChoosenCard is MonsterCard monsterCard)
            {
                if (CheckAttackCondition(monsterCard) == true)
                {
                    if (Opponent.PlayerField.getMonsters.Count == 0)
                    {
                        Player.DirectAttack(monsterCard, Opponent);
                        QueueCommand.Remove("Attack");
                    }
                    else if (Opponent.PlayerField.getMonsters.Count != 0)
                    {
                        SetAttackChooseMonsterCondition(monsterCard);
                        QueueCommand.Add("AttackChosenMonster");
                        DuelState = DuelState.ChoosingMonsterCard; 
                    }
                }
            }

            QueueCommand.Remove("Attack");
        }
        public void StartNewGame()
        {
            this.Player = new Player("Seto Kaiba", 8000, "MonsterDataSet.txt", "MonsterDataSet.txt");
            this.Opponent = new Player("Muto Yugi", 8000, "MonsterDataSet.txt", "MonsterDataSet.txt");
            for (int i = 0; i < 5; i++)
            {
                this.Player.DrawCard();
                this.Opponent.DrawCard();
            }
        }
    }
}