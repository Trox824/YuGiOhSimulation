using SplashKitSDK;

namespace YugiohGame.Game.CardComponent.Card
{
    public abstract class CardObject
    {
        private Bitmap _bitmap;
        private Sprite _sprite;
        private float _x;
        private float _y;
        private string _name;
        private string _owner;
        private string _description;
        private string _locationid;
        private bool _facedDown;
        private int _cardId;
        private string _location;
        private Bitmap _downsideCard;
        private Sprite _spriteDownsideCard;
        private Sprite _spriteUpsideCard;
        private bool _selected;

        public int Id
        {
            get { return _cardId; }
            set { _cardId = value; }
        }

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool FaceDown
        {
            get { return _facedDown; }
            set { _facedDown = value; }
        }

        public Bitmap DownsideCard
        {
            get { return _downsideCard; }
            set { _downsideCard = value; }
        }

        public Sprite SpriteDownsideCard
        {
            get { return _spriteDownsideCard; }
            set { _spriteDownsideCard = value; }
        }

        public Sprite SpriteUpsideCard
        {
            get { return _spriteUpsideCard; }
            set { _spriteUpsideCard = value; }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public float X
        {
            get { return Sprite.X; }
            set { Sprite.X = value; }
        }

        public float Y
        {
            get { return Sprite.Y; }
            set { Sprite.Y = value; }
        }

        public Sprite Sprite
        {
            get
            {
                return _sprite;
            }
            set
            {
                _sprite = value;
            }
        }

        public CardObject(string name, string id)
        {
            _bitmap = new Bitmap(name, id + ".jpg");
            _sprite = SplashKit.CreateSprite(_bitmap);
            DownsideCard = new Bitmap("DownsideCard", "CardDownSide.png");
            _selected = false;
            _facedDown = false;
        }

        public void UpdateBitmapForSprite(Bitmap bitmap)
        {
            Sprite = new Sprite(bitmap);
            Sprite.X = X;
            Sprite.Y = Y;
        }

        public bool IsAt(Point2D p)
        {
            return SplashKit.PointInRectangle(p, SplashKit.RectangleFrom(Sprite.X, Sprite.Y, Bitmap.Width, Bitmap.Height));
        }
    }
}