using UnityEngine;
using UnityEngine.Pool;

namespace NF.Main.Core.Pooling
{
    public interface IPoolableObject
    {
        bool IsReleased { get; set; }
        IObjectPool<GameObject> Pool { get; set; }
        void Initialize();
        void ReturnToPool();
    }

}
