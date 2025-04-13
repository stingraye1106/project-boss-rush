using System;
using NF.Main.Core;
using NF.Main.Core.PlayerStateMachine;
using NF.Main.Gameplay.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.PlayerInput
{
    public class PlayerController : MonoExt
    {
        [TabGroup("References")][SerializeField] private PlayerInputReader _playerInput;
        [TabGroup("References")][SerializeField] private Animator _animator;
        [TabGroup("References")][SerializeField] private PlayerCharacter _playerCharacter;

        private StateMachine _stateMachine;
        public PlayerState PlayerState { get; set; }
        
        private void Start()
        {
            Initialize();
            OnSubscriptionSet();
        }

        private void Awake()
        {
            SetupStateMachine();
        }

        //Call the current states update method
        private void Update()
        {
            _stateMachine.Update();
        }

        //Call the current states fixed update method
        private void FixedUpdate()
        {
            _stateMachine.Update();
        }

        //Initialize needed data
        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            _playerInput.EnablePlayerActions();
        }

        // Set up all the events
        public override void OnSubscriptionSet()
        {
            base.OnSubscriptionSet();
            AddEvent(_playerInput.Attack, _ => OnAttack());
            AddEvent(_playerInput.Movement, OnPlayerMove);
            AddEvent(_playerInput.PlayerAbility1, _ => OnActivateAbility1());
            AddEvent(_playerInput.PlayerAbility2, _ => OnActivateAbility2());
            AddEvent(_playerInput.PlayerAbility3, _ => OnActivateAbility3());
        }


        //Sets up animation states and transitions
        private void SetupStateMachine()
        {
            // State Machine
            _stateMachine = new StateMachine();
            
            // Declare Player States
            var idleState = new PlayerIdleState(this, _animator);
            var movingState = new PlayerMovingState(this, _animator);
            
            // Define Player State Transitions
            Any(idleState, new FuncPredicate(ReturnToIdleState));
            At(idleState, movingState, new FuncPredicate(TransitionToMovingState));
            
            // Set Initial State
            _stateMachine.SetState(idleState);
        }
        
        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
        
        //Method that handles the condition if the player should return to idle state
        private bool ReturnToIdleState()
        {
            return PlayerState == PlayerState.Idle;
        }

        //Method that handles the condition if the player should transition to moving state
        private bool TransitionToMovingState()
        {
            return PlayerState == PlayerState.Moving;
        }
        
        //Method that handles logic when the attack button is pressed
        private void OnAttack()
        {
            Debug.Log($"Attack Performed");
        }
        
        //Player movement logic is handled here
        private void OnPlayerMove(Vector2 movementDirection)
        {
            if (movementDirection != Vector2.zero)
            {
                PlayerState = PlayerState.Moving;
            } else
            {
                PlayerState = PlayerState.Idle;
            }

            Debug.Log($"Player Movement: {movementDirection}");
            var convertedDirection = new Vector3(movementDirection.x, 0, movementDirection.y);
            _playerCharacter.Move(convertedDirection);
        }

        private void OnActivateAbility1()
        {
            Debug.Log($"Ability 1 activated");
            _playerCharacter.Abilities[0].Use(_playerCharacter.gameObject);
        }

        private void OnActivateAbility2()
        {
            Debug.Log($"Ability 2 activated");
            _playerCharacter.Abilities[1].Use(_playerCharacter.gameObject);
        }

        private void OnActivateAbility3()
        {
            Debug.Log($"Ability 3 activated");
            _playerCharacter.Abilities[2].Use(_playerCharacter.gameObject);
        }
    }
}