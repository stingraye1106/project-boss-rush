using NF.Main.Gameplay.Character;
using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem.Effects
{
    [CreateAssetMenu(fileName = "DealDamageEffect", menuName = "ScriptableObject/Ability System/Effects/DealDamageEffect")]
    public class DealDamageEffect : AEffect
    {
        [SerializeField] private Stat _damage;

        // Logic for applying the deal damage effect.
        public override void ApplyEffect(GameObject hitTarget)
        {
            if (hitTarget.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage.DefaultValue);
        }
    }
}

