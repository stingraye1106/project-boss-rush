using NF.Main.Gameplay.AbilitySystem;
using NF.Main.Gameplay.EnemyAI;
using System.Collections.Generic;
using UnityEngine;

namespace NF.Main.Core.EnemyStateMachine
{
    public class EnemyDecideState : EnemyBaseState
    {
        private Dictionary<EnemyState, Ability> _abilityDictionary;

        public EnemyDecideState(EnemyController enemyController, Animator animator) : base(enemyController, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Deciding action...");

            int randomIndex = UnityEngine.Random.Range(0, _enemyController.EnemyCharacter.Abilities.Count);
            _enemyController.EnemyState = DecideEnemyState(_enemyController.EnemyCharacter.Abilities, randomIndex);
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Decision made");
        }

        private EnemyState DecideEnemyState(List<Ability> abilities, int abilityIndex)
        {
            if (!IsAllAbilitiesOnCd(abilities))
            {
                switch (abilityIndex)
                {
                    case 0:
                        if (abilities[0].CooldownTracker.CanUseAbility())
                            return EnemyState.Ability1;
                        break;
                    case 1:
                        if (abilities[1].CooldownTracker.CanUseAbility())
                            return EnemyState.Ability2;
                        break;
                    case 2:
                        if (abilities[2].CooldownTracker.CanUseAbility())
                            return EnemyState.Ability3;
                        break;
                }
            }

            return EnemyState.Attacking;
        }

        private bool IsAllAbilitiesOnCd(List<Ability> abilities)
        {
            foreach (var ability in abilities) 
            {
                if (ability.CooldownTracker.CanUseAbility())
                {
                    return false;
                }
            }

            return true;
        }
    }
}

