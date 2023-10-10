using SplashKitSDK;

namespace YugiohGame.Game
{
    public abstract class GameObject
    {
        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
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
        private Bitmap _bitmap;
        private Sprite _sprite;
        private float _x;
        private float _y;

        public GameObject(string name, string id)
        {
            _bitmap = new Bitmap(name, id + ".jpg");
            _sprite = SplashKit.CreateSprite(_bitmap);
        }
        public void UpdateBitmapForSprite(Bitmap bitmap)
        {
            Sprite = new Sprite(bitmap);
            Sprite.X = X;
            Sprite.Y = Y;
        }


    }
}
