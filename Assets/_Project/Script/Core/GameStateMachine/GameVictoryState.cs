using NF.Main.Gameplay;
using NF.Main.Gameplay.Character;
using NF.Main.UI;
using UnityEngine;

namespace NF.Main.Core.GameStateMachine
{
    public class GameVictoryState : GameBaseState
    {
        private PlayerInputReader _inputReader;
        private PlayerCharacter _player;
        private EnemyCharacter _enemy;

        public GameVictoryState(GameManager gameManager, GameState gameState, PlayerInputReader inputReader, PlayerCharacter player, EnemyCharacter enemy) : base(gameManager, gameState)
        {
            _inputReader = inputReader;
            _player = player;
            _enemy = enemy;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Win state");

            _player.StopMovement();
            _enemy.StopMovement();
            _inputReader.DisablePlayerActionMap();
            UIManager.Instance.ShowVictoryScreen();
        }
    }
}