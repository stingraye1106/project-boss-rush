using NF.Main.Gameplay;
using UnityEngine;

namespace NF.Main.Core.GameStateMachine
{
    public class GameVictoryState : GameBaseState
    {
        public GameVictoryState(GameManager gameManager, GameState gameState) : base(gameManager, gameState)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Win state");
        }
    }
}

