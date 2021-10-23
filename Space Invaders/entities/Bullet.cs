using System;
using System.Linq;
using SFML.Graphics;
using SFML.System;
using Space_Invaders.entities.Actors;

namespace Space_Invaders.entities

{
    public class Bullet : Entity
    {
        private const float BulletSpeed = 800f;
        private bool _isFriendly;
        private Vector2f _direction;


        public Bullet() : base("sheet")
        {
        }

        public void Create(Scene scene, Vector2f bulletDirection, Vector2f spawnPos, bool isFriendlyInput)
        {
            this._direction = bulletDirection;
            this._isFriendly = isFriendlyInput;

            base.Create(scene);
            Sprite.TextureRect = new IntRect(856, 421, 9, 54);

            //Sets the origin to the middle 
            Sprite.Origin = new Vector2f(4.5f, 27);

            Sprite.Position = spawnPos;

            //If the bullet is not going down, flip the texture
            if (bulletDirection.Y > 0)
            {
                Sprite.Scale = new Vector2f(1, -1);
            }

            scene.EventManager.Clear += Destroy;
        }

        public void Rotate(int amount)
        {
            Sprite.Rotation = amount;
        }

        protected override void Update(Scene scene, float deltaTime)
        {

            Sprite.Position += BulletSpeed * _direction * deltaTime;

            if (IsOutside(Position))
            {
                Destroy(scene);
            }


            //If the bullet was shot by an enemy
            if (!_isFriendly)
            {
                if (scene.FindByType(out Player player))
                {
                    //If the bullet collides with the player
                    if (Bounds.Intersects(player.Bounds) && !player.IsInvincible)
                    {
                        Console.WriteLine("Bullet hit player");
                        scene.EventManager.PublishLivesLost(1);
                        Explosion explosion = new Explosion();
                        explosion.Create(scene);
                        explosion.Position = Position;

                    }
                }
            }
            //If the bullet was shot by the player
            else
            {
                //Get an enemy if it collides with one
                foreach (Enemy found in scene.FindIntersects(Bounds).OfType<Enemy>())
                {
                    found.Dead = true;
                    this.Dead = true;
                    Destroy(scene);
                }
            }
        }

        protected override void Render(RenderTarget target)
        {
            if (!_isFriendly)
            {
                //If the bullet is harmful make it a bit red
                Sprite.Color = new Color(255, 150, 150);
            }

            base.Render(target);
        }

        private static bool IsOutside(Vector2f position)
        {
            return (position.X - 20 > Program.ScreenW) || (position.X + 20 < 0) ||
                   (position.Y - 20 > Program.ScreenH) ||
                   (position.Y + 20 < 0);
        }
    }
}