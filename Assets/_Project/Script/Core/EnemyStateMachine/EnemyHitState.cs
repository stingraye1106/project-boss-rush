using NF.Main.Gameplay.EnemyAI;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyHitState : EnemyBaseState
    {
        private float _timer;
        private float _hitStateDuration;

        public EnemyHitState(EnemyController enemyController, Animator animator, float hitStateDuration) : base(enemyController, animator)
        {
            _hitStateDuration = hitStateDuration;
        }

        // Logic on enter hit state
        public override void OnEnter()
        {
            base.OnEnter();
            _timer = 0f;
            _animator.CrossFade(HitHash, 0.125f);
        }

        // Logic during hit state
        public override void Update()
        {
            base.Update();
            _timer += Time.deltaTime;
            if (_timer >= _hitStateDuration)
            {
                _enemyController.EnemyState = EnemyState.IdleAfterHit;
            }
        }

        // Logic on exit hit state
        public override void OnExit()
        {
            base.OnExit();
            _timer = 0f;
        }
    }
}

