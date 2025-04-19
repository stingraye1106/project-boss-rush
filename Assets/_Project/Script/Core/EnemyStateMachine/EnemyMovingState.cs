using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyMovingState : EnemyBaseState
    {
        public EnemyMovingState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(MovingHash, 0.25f);
            _enemyController.EnemyCharacter.Move(_enemyController.ChaseTarget.position);
        }

        public override void Update()
        {
            base.Update();
            float distanceFromPlayer = Vector3.Distance(_enemyController.EnemyCharacter.transform.position, _enemyController.ChaseTarget.position);
            bool isWithinAttackRange = distanceFromPlayer <= _enemyController.EnemyCharacter.AttackRange;
            if (isWithinAttackRange)
            {
                _enemyController.EnemyState = EnemyState.Decide;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _enemyController.EnemyCharacter.StopMovement();
        }
    }
}

