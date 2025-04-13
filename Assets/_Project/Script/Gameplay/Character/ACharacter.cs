using NF.Main.Core;
using NF.Main.Gameplay.Movement;
using Sirenix.OdinInspector;
using NF.Main.Gameplay.Stats;
using UnityEngine;
using System.Collections.Generic;
using NF.Main.Gameplay.AbilitySystem;

namespace NF.Main.Gameplay.Character
{
    public abstract class ACharacter : MonoExt, IAttacker, IDamageable, IMovable
    {
        // Used when we need to compare if the character is a player or an enemy
        [SerializeField] private CharacterType _characterType;

        // Stats per character
        [TabGroup("Stats")][SerializeField] protected HealthStat _health;
        [TabGroup("Stats")][SerializeField] protected AttackPowerStat _attackPower;
        [TabGroup("Stats")][SerializeField] protected SpeedStat _speed;

        // Ability list
        [SerializeField] protected List<Ability> _abilities;

        // Movement logic
        protected IMovement _movement;

        public CharacterType CharacterType => _characterType;
        public HealthStat Health => _health;
        public AttackPowerStat AttackPower => _attackPower;
        public SpeedStat Speed => _speed;
        public List<Ability> Abilities => _abilities;


        protected virtual void Awake()
        {
            _movement = GetComponent<IMovement>();
            _movement.Speed = _speed.DefaultValue;
            _health.CurrentValue = _health.DefaultValue;
        }

        public abstract void Attack();

        public abstract void TakeDamage(float damage);

        public abstract void Move(Vector3 direction);
    }
}