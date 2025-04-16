using UniRx;
using UnityEngine;

namespace NF.Main.Gameplay.EnemyAI
{
    // Component that controls what the enemy AI should do
    public class EnemyBrain : MonoBehaviour
    {
        public Subject<bool> MoveTrigger;
        public Subject<Unit> BasicAttackTrigger;
        public Subject<Unit> Ability1Trigger;
        public Subject<Unit> Ability2Trigger;
        public Subject<Unit> Ability3Trigger;

        private void OnEnable()
        {
            MoveTrigger = new Subject<bool>();
            BasicAttackTrigger = new Subject<Unit>();
            Ability1Trigger = new Subject<Unit>();
            Ability2Trigger = new Subject<Unit>();
            Ability3Trigger = new Subject<Unit>();
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

