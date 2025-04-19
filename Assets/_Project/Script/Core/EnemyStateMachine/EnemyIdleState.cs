using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyIdleState : EnemyBaseState
    {
        private float _timer;
        private float _idleTime;

        public EnemyIdleState(EnemyController enemyController, Animator animator, float idleTime) : base(enemyController, animator)
        {
            _idleTime = idleTime;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _animator.CrossFade(IdleHash, 0.25f);
            _timer = 0f;
        }

        public override void Update()
        {
            base.Update();

            _timer += Time.deltaTime;
            if (_timer >= _idleTime)
            {
                _enemyController.EnemyState = EnemyState.Moving;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            _timer = 0f;
        }
    }

}
