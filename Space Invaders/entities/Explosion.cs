using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Space_Invaders.entities

{
    public class Explosion : Entity
    {
        private readonly List<IntRect> _textures = new List<IntRect>
        {
            new IntRect(539, 41, 41, 41),
            new IntRect(498, 447, 41, 41),
            new IntRect(425, 188, 60, 60),
            new IntRect(422, 379, 74, 74),
            new IntRect(422, 379, 74, 74),
            new IntRect(425, 188, 60, 60),
            new IntRect(498, 447, 41, 41),

        };

        private int _usingTextureAtPlace;
        private float _timer;


        public Explosion() : base("tank")
        {
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);

            scene.Assets.SoundManager.PlaySound("explosionCrunch_000");

            Sprite.TextureRect = _textures[0];
            _timer = 0;
            _usingTextureAtPlace = -1;
        }

        protected override void Update(Scene scene, float deltaTime)
        {
            _timer += deltaTime;
            //Only changes the texture each 0.03seconds
            if (_timer <= 0.03f)
            {
                return;
            }

            _timer = 0;

            _usingTextureAtPlace++;

            //If all textures have been used then remove this 
            if (_usingTextureAtPlace >= _textures.Count)
            {
                Destroy(scene);
                return;
            }

            Sprite.TextureRect = _textures[_usingTextureAtPlace];
            //Sets middle of sprite to the middle of the texture
            Sprite.Origin = new Vector2f(_textures[_usingTextureAtPlace].Width / 2f,
                _textures[_usingTextureAtPlace].Height / 2f);
        }
    }
}