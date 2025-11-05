using System;
using System.Collections.Generic;
using CustomProgram.Bullets;
using CustomProgram.Zombies;
using SplashKitSDK;

namespace CustomProgram.Plants
{
    public class ScaredyShroom : ShooterPlant
    {
        private bool _isHiding;
        private int _cooldownTime;
        private int _cooldownCounter;
        private int _plantedTime;

        public ScaredyShroom(double x, double y) : base("ScaredyShroom", "Scaredy.png")
        {
            X = x;
            Y = y;
            Health = 80;
            _isHiding = true;
            _cooldownTime = 90; // ~1.5 seconds per shot
            _cooldownCounter = 0;

            // Add layers (hidden / active)
            Sprite.AddLayer(new Bitmap("Scaredy", "Resources/images/Scaredy.png"), "Hide");
            Sprite.AddLayer(new Bitmap("Scaredyshroom", "Resources/images/Scaredy-shroom.png"), "Active");

            SplashKit.SpriteSetX(Sprite, (float)X - 20);
            SplashKit.SpriteSetY(Sprite, (float)Y - 40);
        }

         public int PlantedTime //time since it planted
        {
            get
            {
                return _plantedTime;
            }
        }

        public bool IsHiding
        {
            get { return _isHiding; }
            set { _isHiding = value; }
        }

        // Called every game tick
        public void Update(List<Zombie> zombies)
        {
            bool zombieNearby = false;

            foreach (var zombie in zombies)
            {
                // Check only zombies in the same row
                if (zombie.Row == Row)
                {
                    double distance = zombie.Sprite.X - Sprite.X;
                    if (distance < 400 && distance > 0) // within range, ahead
                    {
                        zombieNearby = true;
                        break;
                    }
                }
            }

            // If zombies are nearby, pop up and shoot
            if (zombieNearby)
            {
                if (_isHiding)
                {
                    _isHiding = false;
                    ChangeLayer();
                }

                _cooldownCounter++;
                if (_cooldownCounter >= _cooldownTime)
                {
                    Shoot();
                    _cooldownCounter = 0;
                }
            }
            else
            {
                // No zombies → hide
                if (!_isHiding)
                {
                    _isHiding = true;
                    ChangeLayer();
                }
            }
        }

        public void ChangeState(List<Zombie> zombies)
        {
            bool zombieNearby = false;

            foreach (var zombie in zombies)
            {
                double distance = Math.Abs(zombie.X - X);
                if (distance < 400) // within range
                {
                    zombieNearby = true;
                    break;
                }
            }

            if (zombieNearby && _isHiding)
            {
                _isHiding = false;
                ChangeLayer();
            }
            else if (!zombieNearby && !_isHiding)
            {
                _isHiding = true;
                ChangeLayer();
            }
        }

        public override void Shoot()
        {
            if (_isHiding) return; // can't shoot while hiding

            ToxicScaredy toxic = new ToxicScaredy(X, Y);
            base.Shoot();
            BulletPeas.Add(toxic);
        }

        private void ChangeLayer()
        {
            if (_isHiding)
            {
                SplashKit.SpriteHideLayer(Sprite, 1);
                SplashKit.SpriteShowLayer(Sprite, 0);
            }
            else if (PlantedTime >= 800)
            {
                SplashKit.SpriteHideLayer(Sprite, 0);
                SplashKit.SpriteShowLayer(Sprite, 1);
            }
        }

        public override void BeAttacked(Zombie zombie)
        {
            Health -= zombie.Damage;
        }
    }
}
