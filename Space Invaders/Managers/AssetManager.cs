using System.Collections.Generic;
using SFML.Graphics;

namespace Space_Invaders.Managers
{
    public class AssetManager
    {
        public static readonly string AssetPath = "assets";
        private readonly Dictionary<string, Texture> _textures;
        private readonly Dictionary<string, Font> _fonts;
        public readonly SoundManager SoundManager;

        public AssetManager(Scene scene)
        {
            _textures = new Dictionary<string, Texture>();
            _fonts = new Dictionary<string, Font>();
            SoundManager = new SoundManager(scene);
        }

        public Texture LoadTexture(string name)
        {
            if (_textures.TryGetValue(name, out Texture found))
            {
                return found;
            }

            string fileName = $"{AssetPath}/{name}.png";
            Texture texture = new Texture(fileName);
            _textures.Add(name, texture);
            return texture;
        }

        public Font LoadFont(string name)
        {
            if (_fonts.TryGetValue(name, out Font found))
            {
                return found;
            }

            string fileName = $"{AssetPath}/{name}.ttf";
            Font font = new Font(fileName);
            _fonts.Add(name, font);
            return font;
        }
    }
}