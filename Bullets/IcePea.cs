using CustomProgram.Plants;
using SplashKitSDK;

namespace CustomProgram.Bullets
{
    class IcePea : Bullet //Inheried from Bullet class 
    {
        public IcePea(ShooterPlant shooter) : base("Ice Pea", "peaice.png") //Construtor for IcePea class
        {
            X = shooter.X + 25;
            Y = shooter.Y - 10;
            Damage = 10;
            SplashKit.SpriteSetVelocity(Sprite, Vel);
            SplashKit.SpriteSetX(Sprite, (float)X);
            SplashKit.SpriteSetY(Sprite, (float)Y);
        }
    }
}
