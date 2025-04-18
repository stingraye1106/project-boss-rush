using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerBasicAttackState : PlayerBaseState
    {
        public PlayerBasicAttackState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to animation in basic attack
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(AttackHash, 0.25f);
            Debug.Log("Entering basic attack state");

            _playerController.PlayerCharacter.Attack();
        }

        // Logic when player is currently in moving state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _playerController.PlayerCharacter.BasicAttackAbility.IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _playerController.PlayerState = PlayerState.Idle;
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

