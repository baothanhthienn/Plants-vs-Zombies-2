using System;
using SplashKitSDK;

namespace CustomProgram.Bullets
{
    class ToxicScaredy : Bullet //Inheried from Bullet class 
    {
        public ToxicScaredy(double x, double y) : base("Toxic Scaredy", "toxic.png")
        {
            X = x + 25;
            Y = y - 40;
            Damage = 10;
            SplashKit.SpriteSetVelocity(Sprite, Vel);
            SplashKit.SpriteSetX(Sprite, (float)X);
            SplashKit.SpriteSetY(Sprite, (float)Y);
        }
    }
}
