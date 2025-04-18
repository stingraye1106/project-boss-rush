using NF.Main.Core;
using NF.Main.Core.EnemyStateMachine;
using NF.Main.Gameplay.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.EnemyAI
{
    public class EnemyController : MonoExt
    {
        [TabGroup("References")][SerializeField] private Animator _animator;
        [TabGroup("References")][SerializeField] private EnemyCharacter _enemyCharacter;

        [TabGroup("Chase Settings")][SerializeField] private Transform _chaseTarget;

        [TabGroup("Idle Settings")][SerializeField] private float _idleTime;


        private StateMachine _stateMachine;
        public EnemyState EnemyState { get; set; }
        public EnemyCharacter EnemyCharacter => _enemyCharacter;
        public Transform ChaseTarget => _chaseTarget;
        public float IdleTime => _idleTime;

        private void Start()
        {
            Initialize();
            OnSubscriptionSet();
        }

        private void Awake()
        {
            SetupStateMachine();
        }
        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            _enemyCharacter.ChaseTarget = _chaseTarget;
        }

        public override void OnSubscriptionSet()
        {
            base.OnSubscriptionSet();
        }

        private void SetupStateMachine()
        {
            // Declare state machine
            _stateMachine = new StateMachine();

            // Declare enemy states here
            var idleState = new EnemyIdleState(this, _animator);
            var movingState = new EnemyMovingState(this, _animator);
            var attackingState = new EnemyBasicAttackState(this, _animator);

            // Define enemy state transitions here
            // Any(idleState, new FuncPredicate(ReturnToIdleState));
            Any(movingState, new FuncPredicate(TransitionToMovingState));
            At(movingState, attackingState, new FuncPredicate(TransitionToAttackState));
            At(attackingState, idleState, new FuncPredicate(TransitionToIdleState));

            // Set initial state here
            _stateMachine.SetState(movingState);
        }

        // Function to check if enemy should move to idle.
        private bool TransitionToIdleState()
        {
            return EnemyState == EnemyState.Idle;
        }

        // Function to check if enemy should move to the moving state.
        private bool TransitionToMovingState()
        {
            return EnemyState == EnemyState.Moving;
        }

        // Function to check if enemy should perform an ability or attack.
        private bool TransitionToAttackState()
        {
            return EnemyState == EnemyState.Attacking;
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}

