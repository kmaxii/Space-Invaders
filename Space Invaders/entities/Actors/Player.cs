using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders.entities.Actors
{
    public class Player : SpaceShip
    {
        private float _spawnTimer;
        

        public bool IsInvincible => _spawnTimer > 0;

        public override void Create(Scene scene)
        {
            _spawnTimer = -1;
            Speed = 720.0f;
            base.Create(scene);
            Sprite.TextureRect = new IntRect(211, 941, 99, 75);

            //Sets the middle of the ship
            Sprite.Origin = new Vector2f(51.5f, 42);

            //Spawns the ship in the middle far down
            Position = new Vector2f(Program.ScreenW / 2f, Program.ScreenH - 100);

            //Flips the ship so that it faces upwards
          //  Sprite.Scale = new Vector2f(1, -1);



            scene.EventManager.LoseHealth += OnLoseHealth;
        }


        protected override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);

            //Reduces the spawnTimer, but stops some where close to 0
            if (_spawnTimer > 0) _spawnTimer -= deltaTime;


            //Moves the Player
            Vector2f direction = new Vector2f(0, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)) direction += new Vector2f(-1, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) direction += new Vector2f(1, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up)) direction += new Vector2f(0, -1);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down)) direction += new Vector2f(0, 1);

            //Gets the length of the hypotenuse
            var dirLength = MathF.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

            //If character is not moving, don't move it
            if (dirLength > 0.0000001)
            {
                //Limits the speed so that the player can't go faster when going diagonally
                direction /= dirLength;
                Move(direction * deltaTime * Speed, true);
            }


            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (FireOnCooldownTimer <= 0)
                {
                    //Position.Y - Origin.Y gets the top side of the sprite.

                    //Position.X + Origin.X gets the right side of the ship. Subtract 20 from that to get where the bullets should spawn.
                    Vector2f pos1 = new Vector2f(Position.X + Origin.X - 7, Position.Y - Origin.Y);
                    //Position.X - Origin.X gets the left side of the ship. Subtract 20 from that to get where the bullets should spawn.
                    Vector2f pos2 = new Vector2f(Position.X - Origin.X + 7, Position.Y - Origin.Y);

                    ShootBullet(scene, pos1, new Vector2f(0, -1), true);
                    ShootBullet(scene, pos2, new Vector2f(0, -1), true);
                    FireOnCooldownTimer = 1;

                    scene.Assets.SoundManager.PlaySound("laserLarge_000");
                }
            }
        }

        protected override void Render(RenderTarget target)
        {
            //If player is invincible, make the player gray, else make it normal
            Sprite.Color = IsInvincible ? new Color(178, 150, 150) : new Color(255, 255, 255);


            base.Render(target);
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            _spawnTimer = 2;
        }

        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.EventManager.LoseHealth -= OnLoseHealth;
        }
    }
}