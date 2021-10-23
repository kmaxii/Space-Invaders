using SFML.System;

namespace Space_Invaders.entities.Actors
{
    public abstract class SpaceShip : Entity
    {
        protected float Speed;
        protected float FireOnCooldownTimer;

        protected SpaceShip() : base("sheet")
        {
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            scene.EventManager.TimePassed += OnTimePassed;
        }
        

        protected Vector2f Origin => Sprite.Origin;


        protected void ShootBullet(Scene scene, Vector2f bulletSpawnPos,
            Vector2f bulletDirection, bool isFriendly)
        {
            Bullet bullet = new Bullet();
            bullet.Create(scene, bulletDirection, bulletSpawnPos, isFriendly);

            if (bulletDirection.X < 0)
            {
                bullet.Rotate(45);
            }

            if (bulletDirection.X > 0)
            {
                bullet.Rotate(-45);
            }
        }

        protected bool Move(Vector2f movement, bool checkYCollision)
        {
            bool hitWall = false;
            Vector2f newPos = Sprite.Position + movement;

            //If the new position will collide with a wall
            if (newPos.X > Program.ScreenW - Sprite.Origin.X || newPos.X < 0 + Sprite.Origin.X)
            {
                hitWall = true;
                newPos.X = Sprite.Position.X;
            }

            if (checkYCollision)
            {
                //IF the new position will collide with the top or bottom 
                if (newPos.Y > Program.ScreenH - Sprite.Origin.Y || newPos.Y < 0 + Sprite.Origin.Y)
                {
                    hitWall = true;

                    newPos.Y = Sprite.Position.Y;
                }
            }

            Sprite.Position = newPos;
            return hitWall;
        }

        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.EventManager.TimePassed -= OnTimePassed;
        }

        private void OnTimePassed(Scene scene, float time)
        {
            FireOnCooldownTimer -= 0.1f;
            if (FireOnCooldownTimer < 0)
            {
                FireOnCooldownTimer = 0;
            }
        }
    }
}