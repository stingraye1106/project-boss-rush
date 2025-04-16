using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyMovingState : EnemyBaseState
    {
        public EnemyMovingState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(MovingHash, 0.25f);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}

