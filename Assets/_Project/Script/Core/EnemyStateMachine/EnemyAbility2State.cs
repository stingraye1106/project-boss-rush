using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyAbility2State : EnemyBaseState
    {
        public EnemyAbility2State(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        // Transitions to animation in ability 2
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(Ability2Hash, 0.25f);
            Debug.Log("Entering enemy ability 2 state");

           _enemyController.EnemyCharacter.Abilities[1].Use(_enemyController.EnemyCharacter.gameObject);
        }

        // Logic when player is currently in ability 2 state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _enemyController.EnemyCharacter.Abilities[1].IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _enemyController.EnemyState = EnemyState.Idle;
            }
        }

        // Logic when player exits the ability 2 state
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting enemy ability 2 state");
        }
    }
}
