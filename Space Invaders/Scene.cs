using System.Collections.Generic;
using SFML.Graphics;
using Space_Invaders.entities;
using Space_Invaders.High_Score;
using Space_Invaders.Managers;

namespace Space_Invaders
{
    public class Scene
    {
        private readonly List<Entity> _entities;
        public readonly AssetManager Assets;
        public readonly EventManager EventManager;
        public readonly SceneLoader Loader;
        public readonly HighScore HighScores;
        public readonly EnemySpawner EnemySpawner;
        public static Scene GetScene;
        public bool Paused = false;


        public Scene()
        {
            GetScene = this;
            EventManager = new EventManager();
            Assets = new AssetManager(this);

            _entities = new List<Entity>();
            Background background = new Background();
            background.Create(this);

            HighScores = new HighScore();
            Loader = new SceneLoader();
            EnemySpawner = new EnemySpawner();
        }

        public void Spawn(Entity entity)
        {
            _entities.Add(entity);
            entity.Create(this);
        }

        public void UpdateAll(float deltaTime)
        {
            if (deltaTime > 0.01)
            {
                deltaTime = 0.01f;
            }


            EventManager.UpdateEvents(this, deltaTime);

            //Loop that removes dead entities.
            for (int i = 0; i < _entities.Count;)
            {
                Entity entity = _entities[i];
                if (entity.Dead)
                {
                    _entities.RemoveAt(i);
                    entity.Destroy(this);
                }
                else i++;
            }
        }

        public void RenderAll(RenderTarget target)
        {

            EventManager.PublishRender(target);
        }


        public bool FindByType<T>(out T found) where T : Entity
        {
            foreach (Entity entity in _entities)
            {
                if (entity is T typed && !entity.Dead)
                {
                    found = typed;
                    return true;
                }
            }

            found = default(T);
            return false;
        }

        public void Clear()
        {
            //Removes all entities that are not marked as don't destroy on load
            EventManager.ClearScreen(this);
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                Entity entity = _entities[i];
                _entities.RemoveAt(i);
                entity.Destroy(this);
            }

            EnemySpawner.Pause(this);
            Assets.SoundManager.StopInGameMusic();
        }

        public IEnumerable<Entity> FindIntersects(FloatRect bounds)
        {
            int lastEntity = _entities.Count - 1;

            //Loop that goes through all entities and see if they intersect with the bounds sent in
            for (int i = lastEntity; i >= 0; i--)
            {
                Entity entity = _entities[i];
                if (entity.Dead) continue;
                if (entity.Bounds.Intersects(bounds))
                {
                    yield return entity;
                }
            }
        }
    }
}