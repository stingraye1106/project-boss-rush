using NF.Main.Core;
using NF.Main.Core.GameStateMachine;
using NF.Main.Gameplay.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay
{
    public class GameManager : SingletonPersistent<GameManager>
    {
        [TabGroup("References")][SerializeField] private PlayerInputReader _inputReader;
        [TabGroup("References")][SerializeField] private PlayerCharacter _playerCharacter;
        [TabGroup("References")][SerializeField] private EnemyCharacter _enemyCharacter;

        public GameState GameState;

        private StateMachine _stateMachine;
        
        private void Awake()
        {
            Initialize();
            OnSubscriptionSet();
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public override void Initialize(object data = null)
        {
            base.Initialize(data);
            GameState = GameState.Playing;
            SetupStateMachine();
            _inputReader.EnablePlayerActions();
        }

        public override void OnSubscriptionSet()
        {
            base.OnSubscriptionSet();
            AddEvent(_inputReader.PauseTrigger, _ => OnPauseButtonPress());
        }

        private void SetupStateMachine()
        {
            // State Machine
            _stateMachine = new StateMachine();

            // Declare states
            var pausedState = new GamePausedState(this, GameState.Paused, _inputReader);
            var playingState = new GamePlayingState(this, GameState.Playing, _inputReader);
            var gameOverState = new GameOverState(this, GameState.GameOver, _inputReader, _playerCharacter, _enemyCharacter);
            var victoryState = new GameVictoryState(this, GameState.Victory, _inputReader, _playerCharacter, _enemyCharacter);


            // Define transitions
            At(playingState, pausedState, new FuncPredicate(() => GameState == GameState.Paused));
            At(playingState, gameOverState, new FuncPredicate(() => GameState == GameState.GameOver));
            At(playingState, victoryState, new FuncPredicate(() => GameState == GameState.Victory));
            
            Any(playingState, new FuncPredicate(() => GameState == GameState.Playing));

            // Set initial state
            _stateMachine.SetState(playingState);
        }

        private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

        public void OnPauseButtonPress()
        {
            if (GameState == GameState.Playing)
            {
                GameState = GameState.Paused;
            } else if (GameState == GameState.Paused)
            {
                GameState = GameState.Playing;
            }
        }
    }
}