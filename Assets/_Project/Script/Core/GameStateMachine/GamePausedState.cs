using NF.Main.Gameplay;
using NF.Main.UI;
using UnityEngine;

namespace NF.Main.Core.GameStateMachine
{
    public class GamePausedState : GameBaseState
    {
        private PlayerInputReader _inputReader;

        public GamePausedState(GameManager gameManager, GameState gameState, PlayerInputReader inputReader) : base(gameManager, gameState)
        {
            _inputReader = inputReader;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game paused state");

            Time.timeScale = 0f;
            _inputReader.DisablePlayerActionMap();
            UIManager.Instance.ShowPauseScreen();
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.Instance.HidePauseScreen();
        }
    }
}