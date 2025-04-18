using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerAbility1State : PlayerBaseState
    {
        public PlayerAbility1State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to animation in ability 1 state. Calls the method for using the ability itself.
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(Ability1Hash, 0.25f);
            Debug.Log("Entering ability 1 state");

            _playerController.PlayerCharacter.Abilities[0].Use(_playerController.PlayerCharacter.gameObject);
        }

        // Logic when player is currently in ability 1 state. Checks if the ability is done running, then return to idle.
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _playerController.PlayerCharacter.Abilities[0].IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _playerController.PlayerState = PlayerState.Idle;
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

