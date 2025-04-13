using NF.Main.Gameplay.Character;
using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem.Effects
{
    [CreateAssetMenu(fileName = "DealDamageEffect", menuName = "ScriptableObject/Ability System/Effects/DealDamageEffect")]
    public class DealDamageEffect : AEffect
    {
        [SerializeField] private Stat _damage;

        // Logic for damage computation
        private float ComputeDamage(float baseDamage, float additionalDamage)
        {
            return baseDamage + additionalDamage;
        }


        // Logic for applying the deal damage effect.
        public override void ApplyEffect(GameObject source, GameObject target)
        {
            var sourceCharacter = source.GetComponent<ACharacter>();
            var computedDamage = ComputeDamage(sourceCharacter.AttackPower.DefaultValue, _damage.DefaultValue);

            if (target.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(computedDamage);
        }
    }
}

