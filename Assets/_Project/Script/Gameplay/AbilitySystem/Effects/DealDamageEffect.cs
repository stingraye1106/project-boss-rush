using NF.Main.Gameplay.Character;
using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem.Effects
{
    [CreateAssetMenu(fileName = "DealDamageEffect", menuName = "ScriptableObject/Ability System/Effects/DealDamageEffect")]
    public class DealDamageEffect : AEffect
    {
        [SerializeField] private Stat _damage;
        [SerializeField] private float _enhancedDamageMultiplier;
        private float _defaultDamageMultiplier;

        private void OnEnable()
        {
            _defaultDamageMultiplier = 1.0f;
        }

        // Logic for damage computation
        private float ComputeDamage(float baseDamage, float additionalDamage, float damageMultiplier)
        {
            return (baseDamage + additionalDamage) * damageMultiplier;
        }


        // Logic for applying the deal damage effect.
        public override void ApplyEffect(GameObject source, GameObject target)
        {
            var sourceCharacter = source.GetComponent<ACharacter>();
            var damageMultiplier = source.GetComponent<IAttacker>().HasAttackBuff ? _enhancedDamageMultiplier : _defaultDamageMultiplier;
            var computedDamage = ComputeDamage(sourceCharacter.AttackPower.DefaultValue, _damage.DefaultValue, damageMultiplier);

            if (target.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(computedDamage);
        }
    }
}

