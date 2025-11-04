using System;
using SplashKitSDK;

namespace CustomProgram
{
    public class Pizza : GameObject //winning message
    {
        public Pizza() :base("Pizza","Resources/images/pizza.png")
        {
            SplashKit.SpriteSetX(this.Sprite, 380);
            SplashKit.SpriteSetY(this.Sprite, 110);
        }
    }
}
