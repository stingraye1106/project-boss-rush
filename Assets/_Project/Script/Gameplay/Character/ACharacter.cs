using NF.Main.Core;
using NF.Main.Gameplay.Movement;
using Sirenix.OdinInspector;
using NF.Main.Gameplay.Stats;
using UnityEngine;
using System.Collections.Generic;
using NF.Main.Gameplay.AbilitySystem;

namespace NF.Main.Gameplay.Character
{
    public abstract class ACharacter : MonoExt, IAttacker, IDamageable, IMovable, IHealable
    {
        // Used when we need to compare if the character is a player or an enemy
        [SerializeField] private CharacterType _characterType;

        // Stats per character
        [TabGroup("Stats")][SerializeField] protected HealthStat _health;
        [TabGroup("Stats")][SerializeField] protected AttackPowerStat _attackPower;
        [TabGroup("Stats")][SerializeField] protected SpeedStat _speed;

        // Ability list
        [TabGroup("Abilities")][SerializeField] protected Ability _basicAttackAbility;
        [TabGroup("Abilities")][SerializeField] protected List<Ability> _abilities;

        // For buffs
        protected bool _hasAttackBuff;
        protected bool _hasDefenseBuff;

        // Movement logic
        protected IMovement _movement;

        public CharacterType CharacterType => _characterType;
        public HealthStat Health => _health;
        public AttackPowerStat AttackPower => _attackPower;
        public SpeedStat Speed => _speed;
        public Ability BasicAttackAbility => _basicAttackAbility;
        public List<Ability> Abilities => _abilities;
        public bool HasAttackBuff { get => _hasAttackBuff; set => _hasAttackBuff = value; }
        public bool HasDefenseBuff { get => _hasDefenseBuff; set => _hasDefenseBuff = value; }



        protected virtual void OnEnable()
        {
            _movement = GetComponent<IMovement>();
            _movement.Speed = _speed.DefaultValue;
            _health.CurrentValue = _health.DefaultValue;
            ResetCooldowns();
        }

        public abstract void Attack();

        public abstract void TakeDamage(float damage);

        public abstract void Move(Vector3 direction);

        public abstract void StopMovement();

        public void Heal(float healAmount)
        {
            _health.CurrentValue += healAmount;
            _health.CurrentValue = Mathf.Clamp(_health.CurrentValue, 0f, _health.DefaultValue);
        }

        private void ResetCooldowns()
        {
            _basicAttackAbility.CooldownTracker.ResetTracker();
            foreach (var ability in _abilities)
            {
                ability.CooldownTracker.ResetTracker();
            }
        }
    }
}