using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public class EnemyCharacter : ACharacter
    {
        protected override void Awake()
        {
            _health.CurrentValue = _health.DefaultValue;
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        public override void TakeDamage(float damage)
        {
            _health.CurrentValue -= damage;
            Debug.Log($"Enemy took {damage} damage! Current enemy health: {_health.CurrentValue}");
        }

        public override void StopMovement()
        {
            throw new System.NotImplementedException();
        }
    }
}

