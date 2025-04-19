using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyAbility3State : EnemyBaseState
    {
        public EnemyAbility3State(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        // Transitions to animation in ability 3
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(Ability3Hash, 0.25f);
            Debug.Log("Entering ability 3 state");

            _enemyController.EnemyCharacter.Abilities[2].Use(_enemyController.EnemyCharacter.gameObject);
        }

        // Logic when player is currently in ability 3 state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _enemyController.EnemyCharacter.Abilities[2].IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _enemyController.EnemyState = EnemyState.Idle;
            }
        }

        // Logic when player exits the ability 3 state
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting ability 3 state");
        }
    }
}

