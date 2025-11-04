using SplashKitSDK;

namespace CustomProgram.Zombies
{
    public class ZombieFootball : Zombie
    {

        public ZombieFootball() : base("Zombie Football", "Running_Zombie_football.png")
        {
            int row = SplashKit.Rnd(0, 4);
            if (row == 3)
            {
                Y = 120 + 95 * row;
            }
            else if (row == 3)
            {
                Y = 110 + 95 * row;
            }
            else if (row == 0)
            {
                Y = 80;
            }
            else if (row == 1)
            {
                Y = 90 + 95 * row;
            }
            else
            {
                Y = 100 + 95 * row;
            }
            X = SplashKit.ScreenWidth() + 10;
            Health = 500;
            Vector2D _vel = new Vector2D();
            _vel.X = -0.5;
            _vel.Y = 0;
            Vel = _vel;
            Damage = 99;
            Row = row;
            SplashKit.SpriteSetX(Sprite, (float)X);
            SplashKit.SpriteSetY(Sprite, (float)Y);
            SplashKit.SpriteSetVelocity(Sprite, Vel);
            SplashKit.SpriteAddLayer(Sprite, new Bitmap("Zombie Football", "Resources/images/Eating_Zombie_football.png"), "Football Attack");
            SplashKit.SpriteAddLayer(Sprite, new Bitmap("Normal Zombie Attack", "Resources/images/NormalZombieAttack.png"), "Attack");
            SplashKit.SpriteAddLayer(Sprite, new Bitmap("Normal Zombie Attack", "Resources/images/NormalZombieAttack.png"), "Attack");
        }

        public override void ChangeLayer()
        {
            if (Health > 100)
            {
                SplashKit.SpriteShowLayer(Sprite, 0);
                SplashKit.SpriteHideLayer(Sprite, 1);
                SplashKit.SpriteHideLayer(Sprite, 2);
            }
            else if (Health <= 100 && IsEating)
            {
                SplashKit.SpriteHideLayer(Sprite, 0);
                SplashKit.SpriteHideLayer(Sprite, 1);
                SplashKit.SpriteShowLayer(Sprite, 2);
            }
            else if (Health <= 100 && !IsEating)
            {
                SplashKit.SpriteHideLayer(Sprite, 2);
                SplashKit.SpriteHideLayer(Sprite, 0);
                SplashKit.SpriteShowLayer(Sprite, 1);
            }

        }


    }
}
