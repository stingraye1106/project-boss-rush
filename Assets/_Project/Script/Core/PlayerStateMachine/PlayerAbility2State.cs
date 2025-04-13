using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerAbility2State : PlayerBaseState
    {
        private float _timer;

        public PlayerAbility2State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to animation in moving state
        public override void OnEnter()
        {
            base.OnEnter();
            _timer = 0f;
            _animator.CrossFade(Ability2Hash, 0.5f);
            Debug.Log("Entering ability 2 state");

            _playerController.PlayerCharacter.Abilities[1].Use(_playerController.PlayerCharacter.gameObject);
        }

        // Logic when player is currently in moving state
        public override void Update()
        {
            base.Update();
            _timer += Time.deltaTime;
            // to do: fix animation sometimes stoppinng halfway
            if (_timer >= _animator.GetCurrentAnimatorStateInfo(0).length)
            {
                _playerController.PlayerState = PlayerState.Idle;
            }
        }

        // Logic when player exits the moving state
        public override void OnExit()
        {
            base.OnExit();
            _timer = 0f;
            Debug.Log("Exiting ability 2 state");
        }
    }
}

