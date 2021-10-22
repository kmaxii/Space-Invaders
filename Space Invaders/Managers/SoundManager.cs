using System;
using System.Collections.Generic;
using SFML.Audio;

namespace Space_Invaders.Managers
{
    public class SoundManager
    {
        private readonly Music _inGameMusic;
        private readonly List<Sound> _soundsBeingPlayed;
        private readonly Dictionary<string, SoundBuffer> _sounds;


        public SoundManager(Scene scene)
        {
            _inGameMusic = new Music("assets/Space Shooter Template Music.ogg");
            scene.EventManager.Update += DispatchDoneSounds;
            _soundsBeingPlayed = new List<Sound>();
            _sounds = new Dictionary<string, SoundBuffer>();
        }

        public void PlayInGameMusic()
        {
            _inGameMusic.Loop = true;
            //Lowers the volume by 25%
            _inGameMusic.Volume = 50f;
            _inGameMusic.Play();
        }

        public void StopInGameMusic()
        {
            _inGameMusic.Stop();
        }

        public void PauseInGameMusic()
        {
            _inGameMusic.Pause();
        }


        public void PlaySound(string soundName)
        {
            Sound sound = new Sound(LoadSound(soundName));
            sound.Pitch = GetRandomPitch();
            sound.Play();
            _soundsBeingPlayed.Add(sound);
        }

        private void DispatchDoneSounds(Scene scene, float deltaTime)
        {
            for (int i = _soundsBeingPlayed.Count - 1; i >= 0; i--)
            {
                Sound sound = _soundsBeingPlayed[i];
                if (sound.Status == SoundStatus.Stopped)
                {
                    _soundsBeingPlayed.RemoveAt(i);
                    sound.Dispose();
                }
            }
        }

        private SoundBuffer LoadSound(string name)
        {
            if (_sounds.TryGetValue(name, out SoundBuffer found))
            {
                return found;
            }

            string fileName = $"{AssetManager.AssetPath}/{name}.ogg";
            SoundBuffer sound = new SoundBuffer(fileName);
            _sounds.Add(name, sound);
            return sound;
        }

        public void DisposeAllSounds()
        {
            _inGameMusic.Stop();
            _inGameMusic.Dispose();
            for (int i = _soundsBeingPlayed.Count - 1; i >= 0; i--)
            {
                Sound sound = _soundsBeingPlayed[i];
                sound.Stop();
                _soundsBeingPlayed.RemoveAt(i);
                sound.Dispose();
            }

            foreach (SoundBuffer soundBuffer in _sounds.Values)
            {
                soundBuffer.Dispose();
            }

            _sounds.Clear();
        }
        
        private static float GetRandomPitch()
        {
            //Returns a random number between 0.7 and 1.3
            return (float) new Random().NextDouble() * (1.3f - 0.7f) + 0.7f;
        }
    }
}