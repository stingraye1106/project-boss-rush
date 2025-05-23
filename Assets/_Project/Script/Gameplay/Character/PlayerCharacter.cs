using NF.Main.Gameplay.AbilitySystem;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public class PlayerCharacter : ACharacter
    {
        [TabGroup("Player-specific references")]
        [SerializeField] private Ability _sprintAbility;

        public Subject<Unit> OnDeath;
        public Subject<Unit> OnHit;

        public Ability SprintAbility => _sprintAbility;

        protected override void OnEnable()
        {
            base.OnEnable();
            _sprintAbility.CooldownTracker.ResetTracker();
            OnDeath = new Subject<Unit>();
            OnHit = new Subject<Unit>();
        }

        // Uses the basic attack ability.
        public override void Attack()
        {
            _basicAttackAbility.Use(gameObject);
        }

        // Injects the player input into the movement controller.
        public override void Move(Vector3 direction)
        {
            _movement.Direction = direction;
        }

        // Handles damage taking and transition to player death state.
        public override void TakeDamage(float damage)
        {
            if (_hasDefenseBuff)
            {
                _health.CurrentValue -= (damage / 2f);
            } else
            {
                _health.CurrentValue -= damage;
            }

            Debug.Log($"Player took {damage} damage! Current player health: {_health.CurrentValue}");

            if (_health.CurrentValue <= 0 )
            {
                // Play death animation by invoking the on death event
                OnDeath.OnNext(Unit.Default);

                // Set game state to Game Over.
                GameManager.Instance.GameState = GameState.GameOver;
            } else
            {
                OnHit.OnNext(Unit.Default);
            }
        }

        // Will stop player movement. This is for idle states or other states that don't need movement input.
        public override void StopMovement()
        {
            _movement.Direction = Vector3.zero;
        }

        public void Sprint()
        {
            _sprintAbility.Use(gameObject);
        }
    }
}

