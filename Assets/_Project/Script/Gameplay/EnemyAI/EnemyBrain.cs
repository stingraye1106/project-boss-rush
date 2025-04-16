using NF.Main.Gameplay.AbilitySystem;
using NF.Main.Gameplay.Character;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace NF.Main.Gameplay.EnemyAI
{
    public class EnemyBrain : MonoBehaviour
    {
        private List<Ability> _enemyAbilities;

        public Subject<bool> MoveTrigger;
        public Subject<Unit> BasicAttackTrigger;
        public Subject<Unit> Ability1Trigger;
        public Subject<Unit> Ability2Trigger;
        public Subject<Unit> Ability3Trigger;

        private void OnEnable()
        {
            _enemyAbilities = gameObject.GetComponent<EnemyCharacter>().Abilities;

            MoveTrigger = new Subject<bool>();
            BasicAttackTrigger = new Subject<Unit>();
            Ability1Trigger = new Subject<Unit>();
            Ability2Trigger = new Subject<Unit>();
            Ability3Trigger = new Subject<Unit>();
        }

        // Get random ability
        private Ability GetRandomAbility()
        {
            int randomIndex = UnityEngine.Random.Range(0, _enemyAbilities.Count);
            return _enemyAbilities[randomIndex];
        }

        // Trigger event that will enable the chase state.
        private void ChaseTarget()
        {
            MoveTrigger.OnNext(true);
        }

        // Trigger event that will use the basic attack.
        private void UseBasicAttack()
        {
            BasicAttackTrigger.OnNext(UniRx.Unit.Default);
        }

        // Trigger event that will use ability 1.
        private void UseAbility1()
        {
            Ability1Trigger.OnNext(UniRx.Unit.Default);
        }

        // Trigger event that will use ability 2.
        private void UseAbility2()
        {
            Ability2Trigger.OnNext(UniRx.Unit.Default);
        }

        // Trigger event that will use ability 3.
        private void UseAbility3()
        {
            Ability3Trigger.OnNext(UniRx.Unit.Default);
        }
    }
}

