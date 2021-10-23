using System;
using SFML.Graphics;
using SFML.System;

namespace Space_Invaders.entities.Actors
{
    public class Enemy : SpaceShip
    {
        private Vector2f _direction;
        private bool _faceLeft;




        public override void Create(Scene scene)
        {
            Speed = 200;
            base.Create(scene);

            //Gets the "enemyBlack3" texture.
            Sprite.TextureRect = RandomEnemy();

            //Sets the middle of the ship
            Sprite.Origin = new Vector2f(51.5f, 42);

            //Spawns the ship at a random x above the screen but makes sure the enemy doesn't spawn inside of edge
            int xToSpawn = new Random().Next() % Program.ScreenW - 52;
            if (xToSpawn < 52) xToSpawn = 52;
            Position = new Vector2f(xToSpawn, -100);

            //Flips the ship so that it faces diagonally downwards either left or right
            _faceLeft = GetRandomBool();
            Sprite.Rotation = _faceLeft ? 30 : -30;

            _direction = new Vector2f(_faceLeft ? -1 : 1, 1);
        }

        protected override void Update(Scene scene, float deltaTime)
        {

            //Tries to move the ship. If it hits a wall it turns.
            if (Move(_direction * Speed * deltaTime, false))
            {
                FlipShip();
            }

            //If the ship left the screen at the bottom, teleport it on top
            if (Position.Y - 42 > Program.ScreenH)
            {
                Position = new Vector2f(Position.X, Position.Y - Program.ScreenH - 100);
            }

            if (scene.FindByType(out Player player))
            {
                //If this ship collides with the player
                if (Bounds.Intersects(player.Bounds) && !player.IsInvincible)
                {
                    scene.EventManager.PublishLivesLost(1);
                    this.Dead = true;
                }
            }

            if (FireOnCooldownTimer <= 0 && new Random().Next(50) == 0)
            {
                //50 % chance not to shoot
                if (GetRandomBool())
                {
                    //makes it try to fire again in 0.6sec
                    FireOnCooldownTimer = 0.6f;
                    return;
                }

                //Gets the position of in front of this sprite
                Vector2f bulletPos = new Vector2f(_faceLeft ? Position.X - Origin.X / 2 : Position.X + Origin.X / 2,
                    Position.Y + Origin.Y);

                ShootBullet(scene, bulletPos, _direction, false);
                FireOnCooldownTimer = 1;

                scene.Assets.SoundManager.PlaySound("laserLarge_003");
            }
        }


        public Vector2f Direction
        {
            get => _direction;
            set => _direction = value;
        }

        private void FlipShip()
        {
            _faceLeft = !_faceLeft;
            Sprite.Rotation = _faceLeft ? 45 : -45;
            _direction = new Vector2f(_faceLeft ? -1 : 1, 1);
        }


        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            Explosion explosion = new Explosion();
            explosion.Create(scene);
            explosion.Position = Position;
        }
        
        private static bool GetRandomBool()
        {
            return new Random().Next() % 2 == 1;
        }

        private static IntRect RandomEnemy()
        {
            int randomNumber = new Random().Next(19);

            switch (randomNumber)
            {
                case 0:
                    return new IntRect(423, 728, 93, 84);
                case 1:
                    return new IntRect(120, 604, 104, 84);
                case 2:
                    return new IntRect(144, 156, 103, 84);
                case 3:
                    return new IntRect(518, 325, 82, 84);
                case 4:
                    return new IntRect(346, 150, 97, 84);
                case 5:
                    return new IntRect(425, 552, 93, 84);
                case 6:
                    return new IntRect(143, 293, 104, 84);
                case 7:
                    return new IntRect(222, 0, 103, 84);
                case 8:
                    return new IntRect(518,409, 82, 84);
                case 9: 
                    return new IntRect(421, 814, 97, 84);
                case 10:
                    return new IntRect(425, 552, 93, 84);
                case 11:
                    return new IntRect(133, 412, 104, 84);
                case 12:
                    return new IntRect(224, 496, 103, 84);
                case 13:
                    return new IntRect(518, 493, 82, 84);
                case 14:
                    return new IntRect(408, 907, 97, 84);
                case 15:
                    return new IntRect(425, 384, 93, 84);
                case 16:
                    return new IntRect(120, 520, 104, 84);
                case 17:
                    return new IntRect(224, 580, 103, 84);
                case 18:
                    return new IntRect(520, 577, 82, 84);
                case 19:
                    return new IntRect(423, 644, 97, 84);
            }
            return new IntRect(423, 728, 93, 84);
        }
    }
}