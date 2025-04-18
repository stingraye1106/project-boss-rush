using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyDeathState : EnemyBaseState
    {
        public EnemyDeathState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(DeathHash, 0.125f);
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