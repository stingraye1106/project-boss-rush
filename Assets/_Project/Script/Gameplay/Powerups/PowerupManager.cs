using NF.Main.Core;
using NF.Main.Core.Pooling;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace NF.Main.Gameplay.Powerups
{
    public class PowerupManager : MonoExt
    {
        [TabGroup("Powerup Prefabs")][SerializeField] private List<GameObject> _powerupPrefabs;

        [TabGroup("Spawn Settings")][SerializeField] private Vector2 _randomXRange;
        [TabGroup("Spawn Settings")][SerializeField] private Vector2 _randomZRange;
        [TabGroup("Spawn Settings")][SerializeField] private float _spawnInterval;

        private Dictionary<int, IObjectPool<GameObject>> _allPools = new Dictionary<int, IObjectPool<GameObject>>();

        private void OnEnable()
        {
            Initialize();
            InvokeRepeating(nameof(SpawnRandomPowerup), _spawnInterval, _spawnInterval);
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            InitializePools();
        }

        private void Update()
        {
            if (GameManager.Instance.GameState == GameState.GameOver || GameManager.Instance.GameState == GameState.Victory)
            {
                CancelInvoke(nameof(SpawnRandomPowerup));
            }
        }

        // Create object pools
        private void InitializePools()
        {
            for (int i = 0; i < _powerupPrefabs.Count; i++)
            {
                if (!_allPools.ContainsKey(i))
                {
                    var pool = new ObjectPoolBuilder(_powerupPrefabs[i]).WithCapacity(3).WithMaxSize(20).Build();
                    _allPools[i] = pool;
                }
            }
        }

        // Get random position for spawn points
        private Vector3 GetRandomPosition()
        {
            float defaultY = 1f;
            float randomX = UnityEngine.Random.Range(_randomXRange.x, _randomXRange.y);
            float randomZ = UnityEngine.Random.Range(_randomZRange.y, _randomZRange.y);
            return new Vector3(randomX, defaultY, randomZ);
        }

        // Logic for spawning the powerup.
        private GameObject SpawnRandomPowerup()
        {
            var randomIndex = UnityEngine.Random.Range(0, _powerupPrefabs.Count);
            var randomPool = _allPools[randomIndex];
            var randomPowerup = randomPool.Get();
            randomPowerup.transform.position = GetRandomPosition();
            randomPowerup.SetActive(true);
            return randomPowerup;
        }
    }
}

