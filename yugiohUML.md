 ```mermaid

    classDiagram
        MonsterCard --|> CardObject
        SpellCard --|> CardObject
        ICardFactory <|-- AbstractCardFactory
        AbstractCardFactory <|-- MonsterCardFactory 
        AbstractCardFactory  <|-- SpellCardFactory
        NormalMonsterCard  --|> MonsterCard
        EffectMonsterCard --|> MonsterCard
        NormalSpellCard --|> SpellCard
        NormalTrapCard  --|> SpellCard
        IHaveEffect  <|-- SpellCard
        SpellCard  --|> PodOfGreed
        ISummonStrategy <|-- NormalSummonStrategy
        ISummonStrategy <|-- NormalTributeSummonStrategy
        Player o--> ISummonStrategy
        MonsterCardFactory ..> NormalMonsterCard
        MonsterCardFactory ..> EffectMonsterCard
        SpellCardFactory..> NormalSpellCard
        SpellCardFactory..> NormalTrapCard
        TextLibrary --> ICardFactory
        TextLibrary --> DatasetProcessor
        Deck --> TextLibrary
        Player --> Hand
        Player --> Deck
        Player --> Field
        Field --> Graveyard
        GameMechanic --> Player
        GameContext --> GameContext
        IGameState <-- GameContext
        IGameState <|-- InGameState
        IGameState <|-- MainMenuState
        IGameState <|-- ChoosingDeckState
        InGameState --> InGameGUI
       
        InGameGUI --> Button
        InGameGUI <-- GameMechanic
    class CardObject{
        -Bitmap _bitmap
        -Sprite _sprite
        -float _x
        -float _y
        -string _name
        -string _owner
        -string _description
        -string _locationid
        -bool _facedDown
        -int _cardId
        -string _location
        -Bitmap _downsideCard
        -Sprite _spriteDownsideCard
        -Sprite _spriteUpsideCard
        -bool _selected
        GameObject(string name, string id)
        +UpdateBitmapForSpriteBitmap(Bitmap bitmap)
    }
    class MonsterCard{
        
        -bool _haveAttacked
        -bool _switchedMode
        -bool _hasAttacked
        -bool _hasSwitchedMode
        -int _attack
        -int _defence
        -int _level
        -string _attribute
        -string _mode
        MonsterCard(int id, string name, string CardTypes, string desc, int lvl, int atk, int def, string attribute)
        
    }
    class NormalMonsterCard{
        NormalMonsterCard(int id, string name, string CardTypes, string desc, int lvl, int atk, int def, string attribute)
    }
    class AbstractCardFactory{
        <<Abstract>>
        +CreateCard(string[] MonsterInformation) ICard
    }
    class ICardFactory{
        <<Interface>>
        CreateCard(string[] MonsterInformation) ICard
    }
    class MonsterCardFactory{
        CreateCard(string[] MonsterInformation) ICard 
    }
    class SpellCardFactory{
        CreateCard(string[] cardInformation) CardObject
    }
    class ISummonStrategy{
        <<Interface>>
        Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, int slotNumber, string mode) Void
    }
    class NormalSummonStrategy{
        Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, int slotNumber, string mode) Void
    }
    class NormalTributeSummonStrategy{
        Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, int slotNumber, string mode) Void
    }
    class Deck{
        -TextLibary _library
        -static Random rng 
        -List<CardObject> _cardList
        -Bitmap _bitmap
        Deck(Player player, string deckPath)
        +DrawOneCard() Icard
        +Shuffle() void
        +addCardToList(CardObject card) void
        +RemoveOneCard(CardObject Card) CardObject
    }
    class Field{
        -String _fieldSide
        -List<MonsterCard> _monsterZone
        -List<SpellCard> _spellZone
        -List<SpellCard> _fieldSpellZone
        -Graveyard _graveyard
        Field(Player player, Deck extraDeck, Deck MainDeck)
        +setMonster(MonsterCard monsterCard, int LocationNumber) Void
        +setSpell(SpellCard spellCard, int LocationNumber) Void
        +RemoveMonster(MonsterCard monsterCard) Void
        +RemoveSpell(SpellCard spellCard) void
        +AddToGraveyard(ICard Card) void
        +RemoveFromGraveyard(ICard Card)
    }
    class Graveyard{
        -List<CardObject> _cardList
        -Bitmap _bitmap
        +Graveyard()
        +addCardToList(CardObject card) void
        +RemoveOneCard(CardObject Card) CardObject
    }
    class Hand{
        -List<ICard> _handCards
        -String _playerHand
        Hand(Player player)
        +addCardToHand(ICard card) Void
        +removeCardFromHand(ICard card) Void
    }
    class Player{
        -string _name;
        +int _lifePoints;
        -Hand _hand;
        -Deck _extraDeck;
        -Deck _mainDeck;
        -Field _field;
        -bool _monsterSummoned;
        -ISummonStrategy _summonStrategy;
        Player(string name, int lifePoints, string mainDeckPath, string extraDeckPath)
        +SelectCardInHand(ICard card) bool
        +SelectMonsterCardOnField(MonsterCard card) bool
        +SelectSpellCardOnField(SpellCard card) bool
        +GetSummonMonsterStrategy() ISummonStrategy
        SetSummonMonsterStrategy(ISummonStrategy summonStrategy) Void
        +SummonMonster(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, int slotNumber, string mode) void
        +Attack() Void
        +DirectAttack() Void
        +SwitchMonsterMode(MonsterCard monsterCard)
        +drawCard() ICard
        +endPhase(String phase) Void
        +endTurn()
    }
    class GameMechanic{
        - DuelState _duelState
        - Player _player
        - Player _opponent
        - Player _currentPlayer
        - Player _winner
        - CardObject _choosenCard
        - List<MonsterCard> _choosenMonsterCardList
        - List<SpellCard> _choosenSpellCardList
        - List<MonsterCard> _LocationChoosingMonsterCard
        - List<SpellCard> _LocationChoosingSpellCardList
        - int _numberOfNeededCards
        - MonsterCard _monsterCardTemp
        +GameMechanic()
        +StartNewGame() void
        +switchPlayer() void
    }
    class InGameGUI{
        -Point2D _p
        -GameInitialization _game
        -Button _summonMonsterButton
        -Button _changeModeMonster
        -Button _AttackButton
        -CardObject _choosenCard
        -List<Button> _buttons
        -IsCardChoosedListener _isCardChoosedListener
        -Bitmap _background
        -Sprite _backgroundSprite
        -Font _font
        +Draw(Point2D point) void
        +DrawHandCard(Point2D point) void
        +DrawCardsInZone(Point2D point) void
        +DrawMonsterInPlayerZone(Point2D point) void
        +DrawMonsterInOpponentZone(Point2D point) void
        +DrawSpellInPlayerZone(Point2D point) void
    }
    class GameContext{
        -IGameState _state
        -static GameContext _gameContext
        -GameContext(Window window)
        +Update() void
        +GetGameInstance(Window window) GameContext

    }
    class IGameState{
        <<Interface>>
        NextState() void
        PreviousState() void
        Update() void
    }
    class InGameState{
        -InGameGUI _gui
        -Point2D _point
        -Button _buttonA
        -Button _buttonB
        -Button _buttonX
        -Button _buttonY
        -List<Button> buttons
        -int _phaseCount
        -bool _isAbleToAction
        -int _turnCount
        +InGameState(Window window)
        +NextState() void
        +PreviousState() void
        +Update() void
    }
    class Button{
        <<Abstract>>
        -ButtonState _buttonState
        -int _isClickedTime
        -bool _isClicked
        -string _name
        -Bitmap _bitmap
        -Sprite _sprite
        -double _x
        -double _y
        +Button(string name, string filename, int X, int Y)
        +Draw() void
        +CheckButtonState(Point2D point) void 
        -IsHovered() bool
        -IsClicked(Point2D point) bool
        +NormalButton() void
        +HoverButton() void
        +ClickButton() void
        +ExecuteFunction(GameMechanic game)void
    }
    class InGameGUI{
        - Point2D _p
        - GameMechanic _game
        - Bitmap _background
        - Sprite _backgroundSprite
        - Font _font
        - Button _buttonA
        - Button _buttonB
        - Button _buttonX
        - Button _buttonY
        - List<Button> buttons
        - Bitmap _cardCover
        - Sprite _cardCoverSprite
        +Draw(Point2D point) void
        +DrawButton(Point2D point) void
        +DrawHandCard(Point2D point) void
        +DrawCardsInZone(Point2D point) void
        +DrawMonsterInPlayerZone(Point2D point) void
        +DrawMonsterInOpponentZone(Point2D point) void
        +DrawSpellInPlayerZone(Point2D point) void
        +DrawSpellInOpponentZone(Point2D point) void
        +DrawInformationPanel() void
        +DrawChoosingCardState() void
    }
    class SpellCard{
        -string _name
        -string[] _cardTypes
        -string _owner
        -string _description
        -string _locationid
        -bool _facedDown
        -string _imageSmall
        -string _imageLarge
        -int _cardId
        -string[] _conditions = null
        -string[] _effects = null
        +SpellCard(int id, string[] CardTypes, string name, string desc) : base(name, id.ToString())
    }
    class PodOfGreed {
        + CheckCondition(GameMechanic game) bool
        + ExecuteEffect(GameMechanic game) void
    }

         
```