using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders.MenuPages
{
    public class EnterNamePage : MenuPage
    {
        private string _enteredText = "";

        public EnterNamePage(Scene scene) : base(scene, "future")
        {
        }

        protected override void Render(RenderTarget target)
        {
            Scene scene = Scene.GetScene;
            Text.CharacterSize = 33;
            Text.DisplayedString = $"Total Score: {scene.HighScores.GetScoreToBeAdded}";
            Text.Position = new Vector2f(
                Program.ScreenW / 2f - Text.GetGlobalBounds().Width / 2, Program.ScreenH / 2f);
            target.Draw(Text);

            Text.CharacterSize = 23;
            Text.DisplayedString = $"Name: {_enteredText}";
            Text.Position = new Vector2f(
                Program.ScreenW / 2f - Text.GetGlobalBounds().Width / 2, Program.ScreenH / 2 + 40);
            target.Draw(Text);


            if (_enteredText == "")
            {
                //Sets text color to gray
                Text.FillColor = new Color(178, 178, 178);
                Text.DisplayedString = "Enter a name...";
            }
            else
            {
                Text.FillColor = Color.Green;
                Text.DisplayedString = "Done!";
            }

            Text.Position = new Vector2f(
                Program.ScreenW - 20 - Text.GetGlobalBounds().Width, Program.ScreenH - 40);
            target.Draw(Text);

            //Resets the color
            Text.FillColor = Color.White;
        }

        protected override void KeyPressed(Object sender, KeyEventArgs args)
        {
            Scene scene = Scene.GetScene;
            //If Pressed Enter
            if (args.Code == Keyboard.Key.Enter)
            {
                //Doesn't allow first letter to be a space
                if (_enteredText == "")
                {
                    return;
                }


                //Adds the high score
                scene.HighScores.AddHighScore(_enteredText);

                //Starts rendering the leaderboard
                scene.Loader.Open(scene, "Leaderboard");

                //Plays a sound
                scene.Assets.SoundManager.PlaySound("doorOpen_002");

                return;
            }

            if (args.Code == Keyboard.Key.Backspace && _enteredText.Length > 0)
            {
                _enteredText = _enteredText.Remove(_enteredText.Length - 1);
                return;
            }

            if (_enteredText.Length >= 11)
            {
                return;
            }


            if (!isAcceptedKey(args.Code))
            {
                return;
            }

            if (args.Code == Keyboard.Key.Space)
            {
                if (_enteredText.Length > 0)
                {
                    _enteredText += " ";
                }
                return;
            }

            _enteredText += args.Code.ToString();
        }

        private bool isAcceptedKey(Keyboard.Key key)
        {
            return key == Keyboard.Key.A || key == Keyboard.Key.B || key == Keyboard.Key.C ||
                   key == Keyboard.Key.D || key == Keyboard.Key.E || key == Keyboard.Key.F ||
                   key == Keyboard.Key.G || key == Keyboard.Key.H || key == Keyboard.Key.I ||
                   key == Keyboard.Key.J || key == Keyboard.Key.K || key == Keyboard.Key.L ||
                   key == Keyboard.Key.M || key == Keyboard.Key.N || key == Keyboard.Key.O ||
                   key == Keyboard.Key.P || key == Keyboard.Key.Q || key == Keyboard.Key.R ||
                   key == Keyboard.Key.S || key == Keyboard.Key.T || key == Keyboard.Key.U ||
                   key == Keyboard.Key.V || key == Keyboard.Key.W || key == Keyboard.Key.X ||
                   key == Keyboard.Key.Y || key == Keyboard.Key.Z || key == Keyboard.Key.Space ||
                   key == Keyboard.Key.Comma || key == Keyboard.Key.Period || key == Keyboard.Key.Subtract;
        }
    }
}