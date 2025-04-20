using UniRx;
using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public class EnemyCharacter : ACharacter
    {
        [Header("Enemy Character-Specific Fields")]
        [SerializeField] private float _attackRange;

        [Header("Event Triggers")]
        public Subject<Unit> OnDeath;
        public Subject<Unit> OnHit;

        public Transform ChaseTarget { get; set; }
        public float AttackRange => _attackRange;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnDeath = new Subject<Unit>();
            OnHit = new Subject<Unit>();
        }

        // Enemy will pursue the selected chase target.
        private void Update()
        {
            _movement.Direction = ChaseTarget.position;
        }

        // Enemy will use the basic attack during basic attack state.
        public override void Attack()
        {
            _basicAttackAbility.Use(gameObject);
        }

        // Enables movement of the enemy.
        public override void Move(Vector3 direction)
        {
            _movement.CanMove = true;
        }

        // Handles taken damage and transition to enemy death state.
        public override void TakeDamage(float damage)
        {
            if (_hasDefenseBuff)
            {
                _health.CurrentValue -= (damage / 2);
            } else
            {
                _health.CurrentValue -= damage;
            }
            Debug.Log($"Enemy took {damage} damage! Current enemy health: {_health.CurrentValue}");

            if (_health.CurrentValue <= 0) 
            {
                // Play death animation by invoking the on death event
                OnDeath.OnNext(Unit.Default);

                // Set game state to victory.
                GameManager.Instance.GameState = GameState.Victory;
            } else
            {
                OnHit.OnNext(Unit.Default);
            }
        }

        // Stops movement. This is for states that don't require the enemy to chase the player.
        public override void StopMovement()
        {
            _movement.CanMove = false;
        }
    }
}

