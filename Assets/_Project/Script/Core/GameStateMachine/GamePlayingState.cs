using NF.Main.Gameplay;
using NF.Main.UI;
using UnityEngine;

namespace NF.Main.Core.GameStateMachine
{
    public class GamePlayingState : GameBaseState
    {
        private PlayerInputReader _inputReader;

        public GamePlayingState(GameManager gameManager, GameState gameState, PlayerInputReader inputReader) : base(gameManager, gameState)
        {
            _inputReader = inputReader;
        }
    
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Game playing state");

            Time.timeScale = 1.0f;
            _inputReader.EnablePlayerActionMap();
            UIManager.Instance.ShowGameplayScreen();
        }

        public override void OnExit()
        {
            base.OnExit();
            UIManager.Instance.HideGameplayScreen();
        }
    }
}