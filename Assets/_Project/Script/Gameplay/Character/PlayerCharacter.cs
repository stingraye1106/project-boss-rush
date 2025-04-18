using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    public class PlayerCharacter : ACharacter
    {
        public override void Attack()
        {
            _basicAttackAbility.Use(gameObject);
        }

        public override void Move(Vector3 direction)
        {
            _movement.Direction = direction;
        }

        public override void TakeDamage(float damage)
        {
            _health.CurrentValue -= damage;
        }

        public override void StopMovement()
        {
            _movement.Direction = Vector3.zero;
        }
    }
}

