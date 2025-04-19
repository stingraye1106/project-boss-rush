using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyAbility1State : EnemyBaseState
    {
        public EnemyAbility1State(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        // Transitions to animation in ability 1
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(Ability1Hash, 0.25f);
            Debug.Log("Entering ability 1 state");

            _enemyController.EnemyCharacter.Abilities[0].Use(_enemyController.EnemyCharacter.gameObject);
        }

        // Logic when player is currently in ability 1 state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _enemyController.EnemyCharacter.Abilities[0].IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _enemyController.EnemyState = EnemyState.Idle;
            }
        }

        // Logic when player exits the ability 1 state
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting ability 1 state");
        }
    }
}

