using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public class EnemyCharacter : ACharacter
    {
        [SerializeField] private float _attackRange;

        public Transform ChaseTarget { get; set; }
        public float AttackRange => _attackRange;

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
        }

        private void Update()
        {
            _movement.Direction = ChaseTarget.position;
        }

        public override void Attack()
        {
            _basicAttackAbility.Use(gameObject);
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

