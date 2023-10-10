namespace YugiohGame.Game.CardComponent.Card
{
    public abstract class MonsterCard : CardObject
    {
        private bool _hasAttacked;
        private bool _hasSwitchedMode;
        private int _attack;
        private int _defence;
        private int _level;
        private string _attribute;
        private string _mode;

        public bool HasAttacked
        {
            get { return _hasAttacked; }
            set { _hasAttacked = value; }
        }

        public bool HasSwitchedMode
        {
            get { return _hasSwitchedMode; }
            set { _hasSwitchedMode = value; }
        }

        public virtual int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }

        public virtual int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        public MonsterCard(int id, string name, string CardTypes, string desc, int lvl, int atk, int def, string attribute) : base(name, id.ToString())
        {
            Selected = false;
            Id = id;
            Name = name;
            Description = desc;
            _level = lvl;
            _attack = atk;
            _defence = def;   
            _mode = "Attack";
        }
        public void SwitchMode()
        {
            if (Mode == "Defence")
            {
                Mode = "Attack";
                FaceDown = false;
            }
            else if (Mode == "Attack")
                Mode = "Defence";
            HasSwitchedMode = true;
        }
        //get effect and condition function ( have not implemented )
    }
}