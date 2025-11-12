using CustomProgram.Plants;
using SplashKitSDK;

namespace CustomProgram
{
    public class Sun : GameObject
    {
        private int _existTime;
        private bool _isFalling = true;
        private double _fallSpeed = 0.5; 
        private double _targetY;   
        public Sun(SunFlower sunflower) : this(sunflower.X, sunflower.Y)
        {

        }

        public Sun(TwinSunFlower twinsunflower) : this(twinsunflower.X, twinsunflower.Y)
        {

        }

        public Sun(double x, double y) : base("Single Sun", "sun.png")
        {
            X = x;
            Y = y;
            _existTime = 0;
            _targetY = SplashKit.Rnd(450, 620);
            SplashKit.SpriteSetX(this.Sprite, (float)X - 10);
            SplashKit.SpriteSetY(this.Sprite, (float)Y - 30);
        }

        public int ExistTime
        {
            get
            {
                return _existTime;
            }
        }

        public void TickSinceSpawns()
        {
            _existTime++;
        }

        public bool IsAt(Point2D pt)
        {
            if (SplashKit.SpritePointCollision(this.Sprite, pt))
            {
                return true;
            }
            return false;
        }
        
        // Make the sun fall down to the grass
        public void FallDown()
        {
            if (_isFalling)
            {
                Y += _fallSpeed;
                SplashKit.SpriteSetY(this.Sprite, (float)Y);

                if (Y >= _targetY)
                {
                    _isFalling = false; // stop falling when reaching grass
                }
            }
        }
    }
}
