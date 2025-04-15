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

        private StateMachine _stateMachine;
        public EnemyState EnemyState { get; set; }

        private void Start()
        {
            Initialize();
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
        }

        private void SetupStateMachine()
        {
            // Declare state machine
            _stateMachine = new StateMachine();

            // Declare enemy states here
            var idleState = new EnemyIdleState(this, _animator);

            // Define enemy state transitions here
            Any(idleState, new FuncPredicate(ReturnToIdleState));

            // Set initial state here
            _stateMachine.SetState(idleState);
        }

        private bool ReturnToIdleState()
        {
            return EnemyState == EnemyState.Idle;
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}

