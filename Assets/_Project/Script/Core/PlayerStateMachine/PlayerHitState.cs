using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerHitState : PlayerBaseState
    {
        private float _timer = 0f;
        private float _hitStateDuration;
        private PlayerInputReader _inputReader;

        public PlayerHitState(PlayerController playerController, Animator animator, float hitStateDuration, PlayerInputReader inputReader) : base(playerController, animator)
        {
            _hitStateDuration = hitStateDuration;
            _inputReader = inputReader;
        }

        // Enter hit state
        public override void OnEnter()
        {
            base.OnEnter();
            _timer = 0f;
            _animator.CrossFade(HitHash, 0.25f);
            _playerController.PlayerCharacter.StopMovement();
            _inputReader.DisablePlayerActionMap();
        }

        // Logic while hit state is running
        public override void Update()
        {
            base.Update();
            _timer += Time.deltaTime;
            if (_timer >= _hitStateDuration)
            {
                _playerController.PlayerState = PlayerState.Idle;
            }
        }

        // Exit hit state
        public override void OnExit()
        {
            base.OnExit();
            _timer = 0f;
            _inputReader.EnablePlayerActionMap();
        }
    }
}
