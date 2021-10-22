using System;
using Space_Invaders.entities;
using Space_Invaders.entities.Actors;
using Space_Invaders.MenuPages;
using Space_Invaders.MenuPages.OptionsPage;

namespace Space_Invaders
{
    public class SceneLoader
    {
        private MenuPage _openPage;


        public SceneLoader()
        {
            _openPage = null;
        }


        public void Open(Scene scene, string name)
        {
            _openPage?.Close(scene);

            switch (name)
            {
                case "MainMenu":
                    _openPage = new OptionsPage(scene)
                        .AddOption("New Game", StartGame, "_")
                        .AddOption("High score", Open, "Leaderboard")
                        .AddOption("Quit", CloseProgram, "_");
                    break;

                case "Leaderboard":
                    _openPage = new Leaderboard(scene);
                    break;

                case "EnterNamePage":
                    _openPage = new EnterNamePage(scene);
                    break;

                case "PauseMenu":
                    _openPage = new OptionsPage(scene)
                        .AddOption("Continue", Continue, "_")
                        .AddOption("New Game", RestartGame, "_")
                        .AddOption("High score", Open, "Leaderboard")
                        .AddOption("Quit", CloseProgram, "_");
                    break;
            }

            if (_openPage == null)
            {
                Console.WriteLine($"MISSING IMPLEMENTATION OF {name}");
                return;
            }


            _openPage.Open(scene);
        }


        private void StartGame(Scene scene, string _)
        {
            _openPage?.Close(scene);
            scene.EventManager.ResetTime();

            scene.Assets.SoundManager.PlayInGameMusic();
            scene.Spawn(new InGameUi());
            scene.Spawn(new Player());
            scene.EnemySpawner.Start(scene);
        }

        private static void CloseProgram(Scene scene, string _)
        {
            Program.Window.Close();
            scene.Assets.SoundManager.DisposeAllSounds();
            scene.HighScores.SaveData();
        }

        private void Continue(Scene scene, string _)
        {
            scene.Paused = false;
            _openPage.Close(scene);
            scene.Assets.SoundManager.PlayInGameMusic();

        }

        public void Pause(Scene scene)
        {
            scene.Paused = true;
            Open(scene, "PauseMenu");
            scene.Assets.SoundManager.PauseInGameMusic();
        }

        private void RestartGame(Scene scene, string _)
        {
            scene.Paused = false;
            scene.Clear();
            StartGame(scene, "_");
        }
    }
}