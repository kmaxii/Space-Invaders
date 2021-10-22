using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Space_Invaders.entities

{
    public class Background : Entity

    {
        private readonly List<float> _positions;

        private const float ScrollingSpeed = 300f;

        private int _amountHorizontal;

        public Background() : base("background")
        {
            _positions = new List<float>();
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);

            //Spawns a background everywhere from top until bottom
            for (int i = Program.ScreenH; i > -Sprite.TextureRect.Height; i -= Sprite.TextureRect.Height)
            {
                _positions.Add(i);
            }
            

            int pixelsInBackground = Program.ScreenW;
            
            //Sets the "amountHorizontal" int to how many backgrounds need to be spawned horizontally
            while (pixelsInBackground > 0)
            {
                _amountHorizontal++;
                pixelsInBackground -= Sprite.TextureRect.Width;
            }

            Console.WriteLine(_amountHorizontal);
        }

        protected override void Update(Scene scene, float deltaTime)
        {
            base.Update(scene, deltaTime);


            for (int i = 0; i < _positions.Count; i++)
            {
                _positions[i] += ScrollingSpeed * deltaTime;
            }

            if (_positions[^1] > -10)
            {
                _positions.Add(-Sprite.TextureRect.Height);
            }

            if (_positions[0] > Program.ScreenH)
            {
                _positions.RemoveAt(0);
            }

        }

        protected override void Render(RenderTarget target)
        {
            int xPos = 0;

            //For each horizontal background picture that is needed
            for (int j = 0; j < _amountHorizontal; j++)
            {
                //Spawn one in each line vertical
                foreach (var t in _positions)
                {
                    Position = new Vector2f(xPos, t);
                    base.Render(target);
                }
                
                xPos += Sprite.TextureRect.Width;
            }
        }
    }
}