using UnityEngine;
using UnityEngine.Pool;

namespace NF.Main.Core.Pooling
{
    public class DefaultPoolableObject : MonoBehaviour, IPoolableObject
    {
        public bool IsReleased { get; set; }
        public IObjectPool<GameObject> Pool { get; set; }

        public void Initialize()
        {
            // Logic for initialization
        }

        public void ReturnToPool()
        {
            if (IsReleased)
                return;

            Pool?.Release(gameObject);
        }
    }
}

