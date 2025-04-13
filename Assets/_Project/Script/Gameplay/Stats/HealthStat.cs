using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.Stats
{
    [CreateAssetMenu(fileName = "HealthStat", menuName = "ScriptableObject/Stats/Health Stat")]
    public class HealthStat : Stat
    {
        private float _currentValue;

        [ShowInInspector, ReadOnly]
        public float CurrentValue
        {
            get => _currentValue;
            set
            {
                _currentValue = Mathf.Max(value, 0);
            }
        }
    }
}

