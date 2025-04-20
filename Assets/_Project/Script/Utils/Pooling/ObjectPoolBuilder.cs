using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace NF.Main.Core.Pooling
{
    public class ObjectPoolBuilder
    {
        private int _defaultCapacity = 3;
        private int _maxSize = 10;
        private List<GameObject> _gameObjects;
        private IObjectPool<GameObject> _pool;

       
        // Constructors for object pool builder
        public ObjectPoolBuilder(GameObject gameObject)
        {
            CheckIfPoolable(gameObject);
            _gameObjects = new List<GameObject>() { gameObject };
        }

        public ObjectPoolBuilder(List<GameObject> gameObjects) 
        {
            foreach (var go in gameObjects)
            {
                CheckIfPoolable(go);
            }
            _gameObjects = gameObjects;
        }

        // Error-checking if object is poolable
        private void CheckIfPoolable(GameObject gameObject)
        {
            if (gameObject.GetComponent<IPoolableObject>() == null)
            {
                throw new System.NullReferenceException($"To enable pooling for {gameObject.name}, please implement {nameof(IPoolableObject)}.");
            }
        }

        // Build functions
        public ObjectPoolBuilder WithCapacity(int defaultCapacity)
        {
            _defaultCapacity = defaultCapacity;
            return this;
        }

        public ObjectPoolBuilder WithMaxSize(int maxSize)
        {
            _maxSize = maxSize;
            return this;
        }

        public IObjectPool<GameObject> Build()
        {
            var gameObjectPool = new GameObjectPool(_gameObjects, _defaultCapacity, _maxSize);
            _pool = gameObjectPool.Pool;
            return _pool;
        }
    }
}

