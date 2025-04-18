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
        public PlayerCharacter PlayerCharacter => _playerCharacter;
        
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
            var basicAttackState = new PlayerBasicAttackState(this, _animator);
            var ability1State = new PlayerAbility1State(this, _animator);
            var ability2State = new PlayerAbility2State(this, _animator);
            var ability3State = new PlayerAbility3State(this, _animator);

            // Define Player State Transitions
            Any(idleState, new FuncPredicate(ReturnToIdleState));
            At(idleState, movingState, new FuncPredicate(TransitionToMovingState));
            At(idleState, basicAttackState, new FuncPredicate(TransitionToBasicAttackState));
            At(movingState, basicAttackState, new FuncPredicate(TransitionToBasicAttackState));
            At(idleState, ability1State, new FuncPredicate(TransitionToAbility1State));
            At(movingState, ability1State, new FuncPredicate(TransitionToAbility1State));
            At(idleState, ability2State, new FuncPredicate(TransitionToAbility2State));
            At(movingState, ability2State, new FuncPredicate(TransitionToAbility2State));
            At(idleState, ability3State, new FuncPredicate(TransitionToAbility3State));
            At(movingState, ability3State, new FuncPredicate(TransitionToAbility3State));

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

        private bool TransitionToAbility1State()
        {
            return PlayerState == PlayerState.Ability1;
        }

        private bool TransitionToAbility2State()
        {
            return PlayerState == PlayerState.Ability2;
        }

        private bool TransitionToAbility3State() 
        { 
            return PlayerState == PlayerState.Ability3;
        }

        private bool TransitionToBasicAttackState()
        {
            return PlayerState == PlayerState.Attacking;
        }

        private bool CanMove()
        {
            return PlayerState == PlayerState.Idle || PlayerState == PlayerState.Moving;
        }

        private bool CanAttackOrUseAbility()
        {
            return PlayerState != PlayerState.Attacking && PlayerState != PlayerState.Ability1 && PlayerState != PlayerState.Ability2 && PlayerState != PlayerState.Ability3;
        }
        
        //Method that handles logic when the attack button is pressed
        private void OnAttack()
        {
            bool isBasicAttackUsable = _playerCharacter.BasicAttackAbility.CooldownTracker.CanUseAbility();
            if (isBasicAttackUsable)
            {
                if (CanAttackOrUseAbility())
                {
                    _playerCharacter.StopMovement();

                    PlayerState = PlayerState.Attacking;
                }
                else
                {
                    Debug.Log("Ability/attack ongoing");
                }
            } else
            {
                Debug.Log("Basic attack on cooldown");
            }
        }
        
        //Player movement logic is handled here
        private void OnPlayerMove(Vector2 movementDirection)
        {
            if (CanMove())
            {
                if (movementDirection != Vector2.zero)
                {
                    PlayerState = PlayerState.Moving;

                    Debug.Log($"Player Movement: {movementDirection}");
                    var convertedDirection = new Vector3(movementDirection.x, 0, movementDirection.y);
                    _playerCharacter.Move(convertedDirection);
                }
                else
                {
                    PlayerState = PlayerState.Idle;
                    _playerCharacter.StopMovement();
                }
            }
        }

        private void OnActivateAbility1()
        {
            bool isAbility1Usable = _playerCharacter.Abilities[0].CooldownTracker.CanUseAbility();
            if (isAbility1Usable)
            {
                if (CanAttackOrUseAbility())
                {
                    _playerCharacter.StopMovement();

                    PlayerState = PlayerState.Ability1;
                }
                else
                {
                    Debug.Log("Ability/attack ongoing");
                }
            } else
            {
                Debug.Log("Ability 1 currently in cooldown");
            }

        }

        private void OnActivateAbility2()
        {
            bool isAbility2Usable = _playerCharacter.Abilities[1].CooldownTracker.CanUseAbility();
            if (isAbility2Usable)
            {
                if (CanAttackOrUseAbility())
                {
                    _playerCharacter.StopMovement();

                    PlayerState = PlayerState.Ability2;
                }
                else
                {
                    Debug.Log("Ability/attack ongoing");
                }
            } else
            {
                Debug.Log("Ability 2 currently in cooldown");
            }
        }

        private void OnActivateAbility3()
        {
            bool isAbility3Usable = _playerCharacter.Abilities[2].CooldownTracker.CanUseAbility();
            if (isAbility3Usable)
            {
                if (CanAttackOrUseAbility())
                {
                    _playerCharacter.StopMovement();

                    PlayerState = PlayerState.Ability3;
                }
                else
                {
                    Debug.Log("Ability/attack ongoing");
                }
            } else
            {
                Debug.Log("Ability 3 currently in cooldown");
            }
        }
    }
}