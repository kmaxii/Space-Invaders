using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders
{
    static class Program
    {

        public static RenderWindow Window;
              
        public const int ScreenW = 1920;
        public const int ScreenH = 1080;
        
        static void Main()
        {
            using (Window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Space Invader"))
            {
                Scene scene = new Scene();
                Window.Closed +=
                    (_, _) =>
                    {
                        scene.Assets.SoundManager.DisposeAllSounds();
                        scene.HighScores.SaveData();
                        Window.Close();
                    };
                Clock clock = new Clock();
                scene.Loader.Open(scene, "MainMenu");
                
                
                while (Window.IsOpen)
                {
                    Window.DispatchEvents();
                    float deltaTime = clock.Restart().AsSeconds();
                    
                    scene.UpdateAll(deltaTime);
                    
                    Window.Clear();
                    
                    scene.RenderAll(Window);
                    Window.Display();
                }
            }
        }
    }
}