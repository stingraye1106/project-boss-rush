using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace NF.Main.Core.Pooling
{
    public class GameObjectPool
    {
        private ObjectPool<GameObject> _pool;
        private List<GameObject> _prefabs = new List<GameObject>();

        public ObjectPool<GameObject> Pool => _pool;

        // Constructors for game object pool
        public GameObjectPool(GameObject prefab, int defaultCapacity, int maxSize)
        {
            _pool = new ObjectPool<GameObject>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyPooledObject, true, defaultCapacity, maxSize);
            _prefabs = new List<GameObject>() { prefab };
        }

        public GameObjectPool(List<GameObject> prefabs, int defaultCapacity, int maxSize)
        {
            _pool = new ObjectPool<GameObject>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyPooledObject, true, defaultCapacity, maxSize);
            _prefabs = prefabs;
        }


        // Get random prefab from pre-loaded list of prefabs
        private GameObject GetPrefab()
        {
            int index = UnityEngine.Random.Range(0, _prefabs.Count);
            return _prefabs[index];
        }

        // Invoked when object is created
        private GameObject CreatePooledObject()
        {
            var go = Object.Instantiate(GetPrefab());
            var pooledObject = go.GetComponent<IPoolableObject>();
            pooledObject.Pool = _pool;
            pooledObject.IsReleased = true;
            go.SetActive(false);

            return go;
        }

        // Invoked when object is taken from pool
        private void OnTakeFromPool(GameObject go)
        {
            var pooledObject = go.GetComponent<IPoolableObject>();
            pooledObject.IsReleased = false;
            go.SetActive(false);

            pooledObject.Initialize();
        }

        // Invoked when object is returned to pool
        private void OnReturnToPool(GameObject go)
        {
            var pooledObject = go.GetComponent<IPoolableObject>();
            pooledObject.IsReleased = true;
            go.SetActive(false);
        }

        // Invoked when object is destroyed
        private void OnDestroyPooledObject(GameObject go)
        {
            Object.Destroy(go);
        }

        // Public function used for getting an object from the pool
        public GameObject Get()
        {
            return _pool.Get();
        }
    }

}

