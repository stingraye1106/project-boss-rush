using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerAbility3State : PlayerBaseState
    {
        public PlayerAbility3State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to animation in ability 3 state
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(Ability3Hash, 0.25f);
            Debug.Log("Entering ability 3 state");

            _playerController.PlayerCharacter.Abilities[2].Use(_playerController.PlayerCharacter.gameObject);
        }

        // Logic when player is currently in ability 3 state
        public override void Update()
        {
            base.Update();
            bool isAbilityBehaviourDone = _playerController.PlayerCharacter.Abilities[2].IsAbilityBehaviourDone();

            if (isAbilityBehaviourDone)
            {
                _playerController.PlayerState = PlayerState.Idle;
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

