using NF.Main.Core;
using NF.Main.Gameplay.Character.Stats;
using NF.Main.Gameplay.Movement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public abstract class ACharacter : MonoExt, IAttacker, IDamageable, IMovable
    {
        [TabGroup("Stats")][SerializeField] protected HealthStat _health;
        [TabGroup("Stats")][SerializeField] protected AttackPowerStat _attackPower;
        [TabGroup("Stats")][SerializeField] protected SpeedStat _speed;

        protected IMovement _movement;

        public HealthStat Health => _health;
        public AttackPowerStat AttackPower => _attackPower;
        public SpeedStat Speed => _speed;


        protected void Awake()
        {
            _movement = GetComponent<IMovement>();
            _movement.Speed = _speed.Value;
        }

        public abstract void Attack();

        public abstract void TakeDamage(float damage);

        public abstract void Move(Vector3 direction);
    }
}