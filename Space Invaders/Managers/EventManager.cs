using System;
using SFML.Graphics;

namespace Space_Invaders.Managers
{
    public delegate void ValueChangedEvent(Scene scene, int value);
    public delegate void ValueChangedEventWithFloat(Scene scene, float value);

    public class EventManager
    {
        public event ValueChangedEvent LoseHealth;
        public event Action<Scene, float> TimePassed;
        public event ValueChangedEventWithFloat Update;
        public event Action<RenderTarget> Render;
        public event Action<Scene> Clear;
        private int _livesLost;
        private double _time;
        private float _gameTime;
        
        public void PublishLivesLost(int amount) => _livesLost += amount;

        public void ClearScreen(Scene scene)
        {
            Clear?.Invoke(scene);
        }
        

        public void UpdateEvents(Scene scene, float deltaTime)
        {
            if (scene.Paused) return;
            
            
            _gameTime += deltaTime;
            Update?.Invoke(scene, deltaTime);
           

            if (_livesLost != 0)
            {
                LoseHealth?.Invoke(scene,_livesLost);
                _livesLost = 0; 
            }
            
            _time += deltaTime;
            if (_time >= 0.1f)
            {
                TimePassed?.Invoke(scene, _gameTime);
                _time = 0;
            }

        }

        public void PublishRender(RenderTarget target)
        {
            Render?.Invoke(target);
        }

        public void ResetTime()
        {
            _gameTime = 0;
        }
    }
}