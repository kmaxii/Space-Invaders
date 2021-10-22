using SFML.Graphics;
using SFML.System;

namespace Space_Invaders.entities

{
    public abstract class Entity
    {
        private readonly string _textureName;
        protected readonly Sprite Sprite;
        public bool Dead;

        protected Entity(string textureName)
        {
            this._textureName = textureName;
            Sprite = new Sprite();
        }

        public Vector2f Position
        {
            get => Sprite.Position;
            set => Sprite.Position = value;
        }


        public FloatRect Bounds => Sprite.GetGlobalBounds();

        public virtual void Create(Scene scene)
        {
            scene.EventManager.Update += Update;
            scene.EventManager.Render += Render;
            Sprite.Texture = scene.Assets.LoadTexture(_textureName);
        }

        protected virtual void Render(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        protected virtual void Update(Scene scene, float deltaTime)
        {
        }


        public virtual void Destroy(Scene scene)
        {
            Dead = true;
            scene.EventManager.Update -= Update;
            scene.EventManager.Render -= Render;
        }
    }
}