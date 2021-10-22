using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders.MenuPages
{
    public class Leaderboard : MenuPage
    {
        public Leaderboard(Scene scene) : base(scene, "future")
        {
        }
        
        protected override void Render(RenderTarget target)
        {
            Scene scene = Scene.GetScene;
            Text.CharacterSize = 35;
            Text.DisplayedString = "High Score";
            Text.Position = new Vector2f(
                Program.ScreenW / 2f - Text.GetGlobalBounds().Width / 2, 20);
            target.Draw(Text);


            Text.CharacterSize = 28;
            int yPos = 70;
            for (int i = 0; i < scene.HighScores.GetHighScoreData.Count; i++)
            {
                Text.DisplayedString = $"{i + 1}";
                Text.Position = new Vector2f(20, yPos);
                target.Draw(Text);

                Text.DisplayedString = scene.HighScores.GetHighScoreData[i].Name.ToUpper();
                Text.Position = new Vector2f(70, yPos);
                target.Draw(Text);

                Text.DisplayedString = scene.HighScores.GetHighScoreData[i].Score.ToString();
                Text.Position = new Vector2f(
                    Program.ScreenW - 20 - Text.GetGlobalBounds().Width, yPos);
                target.Draw(Text);

                yPos += 50;
            }

            //Text at bottom right corner
            Text.CharacterSize = 23;
            //Sets text color to gray
            Text.FillColor = new Color(178, 178, 178);
            Text.DisplayedString = "GO BACK";
            Text.Position = new Vector2f(
                Program.ScreenW - 20 - Text.GetGlobalBounds().Width, Program.ScreenH - 40);
            target.Draw(Text);
            //Resets the color
            Text.FillColor = Color.White;
        }
        
        protected override void KeyPressed(Object sender, KeyEventArgs args)
        {

            if (args.Code != Keyboard.Key.Enter) return;
            
            Scene.GetScene.Assets.SoundManager.PlaySound("doorClose_002");

            
            //This means that the player accessed this leaderboard page through the pause menu
            if (Scene.GetScene.Paused)
            {
                Scene.GetScene.Loader.Open(Scene.GetScene, "PauseMenu");
                return;
            }
            
            Scene.GetScene.Loader.Open(Scene.GetScene, "MainMenu");
            

        }
        
    }
}