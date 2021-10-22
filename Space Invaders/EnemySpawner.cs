using System;
using Space_Invaders.entities.Actors;

namespace Space_Invaders
{
    public class EnemySpawner
    {
        private float _lastSpawn;
        private int _chanceToSpawn;

        public EnemySpawner()
        {
            _chanceToSpawn = 5;
        }

        private void OnTimePassed(Scene scene, float time)
        {
            if (_lastSpawn < 0.5)
            {
                _lastSpawn += 0.1f;
                return;
            }

            switch (time)
            {
                case < 10:
                    //Spawns on average one ship each 2.5 sec

                    _chanceToSpawn = 5;
                    break;
                case < 20:
                    //Spawns on average one ship each 2 sec

                    _chanceToSpawn = 4;
                    break;
                case < 40:
                    //Spawns on average one ship each 1.5sec
                    _chanceToSpawn = 3;
                    break;
                case < 100:
                    //Spawns on average one ship per second
                    _chanceToSpawn = 2;
                    break;
                case >= 100:
                    //Spawns a ship each half second
                    _chanceToSpawn = 1;
                    break;
            }

            SpawnEnemy(scene, _chanceToSpawn);
        }

        private void SpawnEnemy(Scene scene, int chance)
        {
            if (new Random().Next(chance) == 0)
            {
                scene.Spawn(new Enemy());
            }

            _lastSpawn = 0;
        }

        public void Pause(Scene scene)
        {
            scene.EventManager.TimePassed -= OnTimePassed;
        }

        public void Start(Scene scene)
        {
            scene.EventManager.TimePassed += OnTimePassed;
        }
    }
}