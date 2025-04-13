using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    public abstract class AEffect : ScriptableObject
    {
        public abstract void ApplyEffect(GameObject source, GameObject target = null);
    }
}

