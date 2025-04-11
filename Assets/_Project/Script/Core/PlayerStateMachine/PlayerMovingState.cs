using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    //Handles all logic for when player goes in, out, and during moving state
    public class PlayerMovingState : PlayerBaseState
    {
        public PlayerMovingState(PlayerController playerController, Animator animator) : base(playerController, animator)
        {
        }

        // Transitions to animation in moving state
        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(MovingHash, 0.5f);
            Debug.Log("Entering Player Moving State");
        }

        // Logic when player is currently in moving state
        public override void Update()
        {
            base.Update();
            Debug.Log("Currently in moving state");
        }

        // Logic when player exits the moving state
        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting Player Moving State");
        }
    }
}

