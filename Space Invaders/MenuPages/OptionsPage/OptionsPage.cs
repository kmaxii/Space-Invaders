using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders.MenuPages.OptionsPage
{
    public class OptionsPage : MenuPage
    {
        private readonly List<Option> _options;

        private int _highlighted;


        public OptionsPage(Scene scene) : base(scene, "future")
        {
            _options = new List<Option>();
        }


        public OptionsPage AddOption(string optionName, Action<Scene, string> action, string stringInAction)
        {
            _options.Add(new Option(optionName, action, stringInAction));
            return this;
        }

        protected override void Render(RenderTarget target)
        {
            Text.CharacterSize = ((Program.ScreenW + Program.ScreenH) / 2) / 28;
            
            


            //Makes sure that the text is in the center of the screen
            int yPos = Program.ScreenH / 2 - (_options.Count * 30) / 2;

            for (int i = 0; i < _options.Count; i++)
            {
                //If the player is on this option, highlight it
                if (i == _highlighted)
                {
                    Text.FillColor = Color.Cyan;
                }

                Text.DisplayedString = _options[i].Text;

                //Sets the position to the middle of the screen at y level of y pos
                Text.Position = new Vector2f(Program.ScreenW / 2f - Text.GetGlobalBounds().Width / 2, yPos);

                target.Draw(Text);

                yPos += ((Program.ScreenW + Program.ScreenH) / 2) / 20;

                //Resets the color
                Text.FillColor = Color.White;
            }
        }

        protected override void KeyPressed(Object sender, KeyEventArgs args)
        {
            Scene scene = Scene.GetScene;
            if (args.Code == Keyboard.Key.Down)
            {
                _highlighted++;
                //If it is at the bottom of selected options, don't move it any further
                if (_highlighted >= _options.Count) _highlighted = _options.Count - 1;

                //If it moved play a sound
                else scene.Assets.SoundManager.PlaySound("doorOpen_002");
            }

            if (args.Code == Keyboard.Key.Up)
            {
                _highlighted--;

                //If it is at the top of selected options, don't move it any further
                if (_highlighted < 0) _highlighted = 0;

                //If it moved play a sound
                else scene.Assets.SoundManager.PlaySound("doorOpen_002");
            }

            if (args.Code == Keyboard.Key.Enter)
            {
                scene.Assets.SoundManager.PlaySound("doorOpen_002");

                _options[_highlighted].InvokeAction(scene);
            }
        }
    }
}