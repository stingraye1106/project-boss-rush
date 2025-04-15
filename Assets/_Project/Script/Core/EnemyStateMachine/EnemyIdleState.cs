using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyIdleState : EnemyBaseState
    {
        public EnemyIdleState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(IdleHash, 0.5f);
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
