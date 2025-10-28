using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace CustomProgram
{
    public class SeedChosenPanel : GameObject
    {
        public SeedChosenPanel() :base("Main Menu","menu1.jpg")
        {
            SplashKit.SpriteSetX(this.Sprite, 0);
            SplashKit.SpriteSetY(this.Sprite, 0);
        }

    }
}
