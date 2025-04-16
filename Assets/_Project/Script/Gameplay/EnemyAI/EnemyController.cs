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
        [TabGroup("References")][SerializeField] private EnemyBrain _enemyBrain;
        [TabGroup("References")][SerializeField] private Transform _chaseTarget;

        private StateMachine _stateMachine;
        public EnemyState EnemyState { get; set; }

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
            AddEvent(_enemyBrain.MoveTrigger, OnEnemyMove);
        }

        private void SetupStateMachine()
        {
            // Declare state machine
            _stateMachine = new StateMachine();

            // Declare enemy states here
            var idleState = new EnemyIdleState(this, _animator);
            var movingState = new EnemyMovingState(this, _animator);

            // Define enemy state transitions here
            Any(idleState, new FuncPredicate(ReturnToIdleState));
            At(idleState, movingState, new FuncPredicate(TransitionToMovingState));

            // Set initial state here
            _stateMachine.SetState(idleState);
        }

        // Function to check if we should move to idle.
        private bool ReturnToIdleState()
        {
            return EnemyState == EnemyState.Idle;
        }

        // Function to check if we should move to the moving state.
        private bool TransitionToMovingState()
        {
            return EnemyState == EnemyState.Moving;
        }

        private void OnEnemyMove(bool canMove)
        {
            Debug.Log("Hello there");
            if (canMove)
            {
                EnemyState = EnemyState.Moving;
                _enemyCharacter.Move(_chaseTarget.position);
            } else
            {
                EnemyState = EnemyState.Idle;
                _enemyCharacter.StopMovement();
            }
                
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}

