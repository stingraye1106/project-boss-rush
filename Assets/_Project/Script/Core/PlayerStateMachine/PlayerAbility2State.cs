using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerAbility2State : PlayerBaseState
    {
        public PlayerAbility2State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to ability 2 state
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(Ability2Hash, 0.25f);
            Debug.Log("Entering ability 2 state");

            _playerController.PlayerCharacter.Abilities[1].Use(_playerController.PlayerCharacter.gameObject);
        }

        // Logic when player is currently in ability 2 state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _playerController.PlayerCharacter.Abilities[1].IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _playerController.PlayerState = PlayerState.Idle;
            }
        }

        // Logic when player exits the ability 2 state
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting ability 2 state");
        }
    }
}