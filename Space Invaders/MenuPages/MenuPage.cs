using System;
using SFML.Graphics;
using SFML.Window;

namespace Space_Invaders.MenuPages
{
    public abstract class MenuPage
    {
        protected readonly Text Text;

        protected MenuPage(Scene scene, string font)
        {
            Text = new Text();
            Text.Font = scene.Assets.LoadFont(font);
        }
        
        public void Open(Scene scene)
        {
            scene.EventManager.Render += Render;
            Program.Window.KeyPressed += KeyPressed;
        }

        public void Close(Scene scene)
        {
            scene.EventManager.Render -= Render;
            Program.Window.KeyPressed -= KeyPressed;
        }

        protected abstract void Render(RenderTarget target);


        protected abstract void KeyPressed(Object sender, KeyEventArgs args);

    }
}