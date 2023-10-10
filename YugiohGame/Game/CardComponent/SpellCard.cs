using System.Xml.Linq;

namespace YugiohGame.Game.CardComponent.Card
{
    public abstract class SpellCard : CardObject, IHaveEffect
    {
        private string _owner;
        private string _description;
        private string _locationid;
        private bool _facedDown;
        private string _imageSmall;
        private string _imageLarge;
        private int _cardId;
        private string[] _conditions = null;
        private string[] _effects = null;

        public SpellCard(int id, string name, string CardTypes, string desc, string attribute) : base(name, id.ToString())
        {
            Selected = false;
            Id = id;
            Name = name;
            Description = desc;
        }
        public abstract bool CheckCondition(GameMechanic game);
        public abstract void ExecuteEffect(GameMechanic game);

        
    }
}
