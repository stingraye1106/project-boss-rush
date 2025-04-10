using UnityEngine;

namespace NF.Main.Gameplay.Character.Stats
{
    public class Stat : ScriptableObject
    {
        [SerializeField] private float _value;

        public float Value { get { return _value; } set { _value = value; } }
    }
}