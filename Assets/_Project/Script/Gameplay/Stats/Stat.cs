using UnityEngine;

namespace NF.Main.Gameplay.Stats
{
    [CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObject/Stats/Stat")]
    public class Stat : ScriptableObject
    {
        [SerializeField] private float _defaultValue;

        public float DefaultValue { get { return _defaultValue; } set { _defaultValue = value; } }
    }
}