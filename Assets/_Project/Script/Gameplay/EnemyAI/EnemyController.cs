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

        [TabGroup("Hit State Settings")][SerializeField] private float _hitStateDuration;
        [TabGroup("Hit State Settings")][SerializeField] private float _idleAfterHitDuration;


        private StateMachine _stateMachine;
        public EnemyState EnemyState { get; set; }
        public EnemyCharacter EnemyCharacter => _enemyCharacter;
        public Transform ChaseTarget => _chaseTarget;

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
            AddEvent(_enemyCharacter.OnDeath, _ => OnEnterDeath());
            AddEvent(_enemyCharacter.OnHit, _ => OnEnterHit());
        }

        private void SetupStateMachine()
        {
            // Declare state machine
            _stateMachine = new StateMachine();

            // Declare enemy states here
            var idleState = new EnemyIdleState(this, _animator, _idleTime);
            var movingState = new EnemyMovingState(this, _animator);
            var attackingState = new EnemyBasicAttackState(this, _animator);
            var deathState = new EnemyDeathState(this, _animator);
            var hitState = new EnemyHitState(this, _animator, _hitStateDuration);
            var decideState = new EnemyDecideState(this, _animator);
            var ability1State = new EnemyAbility1State(this, _animator);
            var ability2State = new EnemyAbility2State(this, _animator);
            var ability3State = new EnemyAbility3State(this, _animator);
            var idleAfterHitState = new EnemyIdleAfterHitState(this, _animator, _idleAfterHitDuration);

            // Define enemy state transitions here
            Any(movingState, new FuncPredicate(TransitionToMovingState));
            At(movingState, decideState, new FuncPredicate(TransitionToDecideState));
            At(decideState, attackingState, new FuncPredicate(TransitionToAttackState));
            At(decideState, ability1State, new FuncPredicate(TransitionToAbility1State));
            At(decideState, ability2State, new FuncPredicate(TransitionToAbility2State));
            At(decideState, ability3State, new FuncPredicate(TransitionToAbility3State));
            Any(idleState, new FuncPredicate(TransitionToIdleState));

            // Transitions when being hit / killed
            Any(deathState, new FuncPredicate(TransitionToDeathState));
            Any(hitState, new FuncPredicate(TransitionToHitState));
            At(hitState, idleAfterHitState, new FuncPredicate(TransitionToIdleAfterHitState));

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

        // Func predicate for transitioning to death state.
        private bool TransitionToDeathState()
        {
            return EnemyState == EnemyState.Death;
        }

        // Func predicate for transitioning to hit state.
        private bool TransitionToHitState()
        {
            return EnemyState == EnemyState.Hit;
        }

        // Func predicate for transitioning to decision state.
        private bool TransitionToDecideState()
        {
            return EnemyState == EnemyState.Decide;
        }

        // Func predicate for transitioning to ability 1 state.
        private bool TransitionToAbility1State()
        {
            return EnemyState == EnemyState.Ability1;
        }

        // Func predicate for transitioning to ability 1 state.
        private bool TransitionToAbility2State()
        {
            return EnemyState == EnemyState.Ability2;
        }

        // Func predicate for transitioning to ability 1 state.
        private bool TransitionToAbility3State()
        {
            return EnemyState == EnemyState.Ability3;
        }

        // Func predicate for transitioning to idle after hit state
        private bool TransitionToIdleAfterHitState()
        {
            return EnemyState == EnemyState.IdleAfterHit;
        }

        // Method called when invoking the on death event.
        private void OnEnterDeath()
        {
            EnemyState = EnemyState.Death;
        }

        // Method called when invoking the on hit event.
        private void OnEnterHit()
        {
            EnemyState = EnemyState.Hit;
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}

