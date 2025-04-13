using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerAbility3State : PlayerBaseState
    {
        private float _timer;

        public PlayerAbility3State(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to animation in moving state
        public override void OnEnter()
        {
            base.OnEnter();
            _timer = 0f;
            _animator.CrossFade(Ability3Hash, 0.5f);
            Debug.Log("Entering ability 3 state");

            _playerController.PlayerCharacter.Abilities[2].Use(_playerController.PlayerCharacter.gameObject);
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
            Debug.Log("Exiting ability 3 state");
        }
    }
}

