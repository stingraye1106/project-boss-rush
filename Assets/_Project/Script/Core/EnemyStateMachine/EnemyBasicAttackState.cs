using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyBasicAttackState : EnemyBaseState
    {
        public EnemyBasicAttackState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        // Transitions to animation in basic attack
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(AttackHash, 0.25f);
            Debug.Log("Entering basic attack state");

            _enemyController.EnemyCharacter.Attack();
        }

        // Logic when player is currently in moving state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _enemyController.EnemyCharacter.BasicAttackAbility.IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _enemyController.EnemyState = EnemyState.Idle;
            }
        }

        // Logic when player exits the moving state
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting basic attack state");
        }
    }
}

