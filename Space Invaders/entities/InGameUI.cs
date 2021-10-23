using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders.entities
{
    public class InGameUi : Entity
    {
        private readonly Text _scoreText;
        private const int MaxHealth = 3;
        private int _currentHealth;
        private int _currentScore;

        public InGameUi() : base("sheet")
        {
            _scoreText = new Text();
        }

        public override void Create(Scene scene)
        {
            base.Create(scene);
            Sprite.TextureRect = new IntRect(797, 173, 30, 33);
            _scoreText.CharacterSize = 12;
            _scoreText.Font = scene.Assets.LoadFont("future");
            _scoreText.DisplayedString = $"Score: {_currentScore}";
            _currentHealth = MaxHealth;
            scene.EventManager.LoseHealth += OnLoseHealth;
            scene.EventManager.TimePassed += OnScoreGain;
        }

        protected override void Update(Scene scene, float deltaTime)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                scene.Loader.Pause(scene);
            }
            
        }

        protected override void Render(RenderTarget target)
        {
            Sprite.Position = new Vector2f(10, 10);

            //Puts as many lives as health
            for (int i = 0; i < _currentHealth; i++)
            {
                base.Render(target);
                Sprite.Position += new Vector2f(30, 0);
            }

            _scoreText.Position = new Vector2f(
                Program.ScreenW - 10 - _scoreText.GetGlobalBounds().Width, 12);
            _scoreText.DisplayedString = $"Score: {_currentScore}";

            target.Draw(_scoreText);
        }

        private void OnLoseHealth(Scene scene, int amount)
        {
            _currentHealth--;
            Console.WriteLine($"Lost health. Total health: {_currentHealth}");
            if (_currentHealth <= 0)
            {
                scene.Clear();
                scene.HighScores.EnterNewHighScore(_currentScore, scene);
                _currentScore = 0;
            }
        }

        private void OnScoreGain(Scene scene, float time)
        {
            _currentScore++;
        }

        public override void Destroy(Scene scene)
        {
            base.Destroy(scene);
            scene.EventManager.LoseHealth -= OnLoseHealth;
            scene.EventManager.TimePassed -= OnScoreGain;
        }

     
    }
}