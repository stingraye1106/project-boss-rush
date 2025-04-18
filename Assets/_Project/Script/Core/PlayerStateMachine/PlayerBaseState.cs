using NF.Main.Gameplay.PlayerInput;
using UnityEngine;

namespace NF.Main.Core.PlayerStateMachine
{
    public class PlayerBaseState: BaseState
    {
        protected readonly PlayerController _playerController;
        protected readonly Animator _animator;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int AttackHash = Animator.StringToHash("Basic Attack");
        protected static readonly int HitHash = Animator.StringToHash("Hit");
        protected static readonly int DeathHash = Animator.StringToHash("Death");
        protected static readonly int MovingHash = Animator.StringToHash("Moving");
        protected static readonly int Ability1Hash = Animator.StringToHash("Ability 1");
        protected static readonly int Ability2Hash = Animator.StringToHash("Ability 2");
        protected static readonly int Ability3Hash = Animator.StringToHash("Ability 3");

        protected PlayerBaseState(PlayerController playerController, Animator animator)
        {
            _playerController = playerController;
            _animator = animator;
        }
    }
    
    public enum PlayerState
    {
        Idle,
        Attacking,
        Hit,
        Death,
        Moving,
        Ability1,
        Ability2,
        Ability3
    }
}

