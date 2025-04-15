using NF.Main.Gameplay.Movement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public class EnemyCharacter : ACharacter
    {
        [TabGroup("Enemy Movement Settings")][SerializeField] private Transform _chaseTarget;


        private void Update()
        {
            _movement.Direction = _chaseTarget.position;
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Vector3 direction)
        {
            _movement.CanMove = true;
        }

        public override void TakeDamage(float damage)
        {
            _health.CurrentValue -= damage;
            Debug.Log($"Enemy took {damage} damage! Current enemy health: {_health.CurrentValue}");
        }

        public override void StopMovement()
        {
            _movement.CanMove = false;
        }
    }
}

