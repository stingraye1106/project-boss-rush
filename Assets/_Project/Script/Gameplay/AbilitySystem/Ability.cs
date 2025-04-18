using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Ability System/Ability")]
    public class Ability : ScriptableObject
    {
        [SerializeField] private string _uniqueId;
        [SerializeField] private AbilityBehaviour _behaviourReference;
        [SerializeField] private Stat _cooldown;
        private CooldownTracker _cooldownTracker;

        public string UniqueId => _uniqueId;
        public Stat Cooldown => _cooldown;
        public CooldownTracker CooldownTracker { get => _cooldownTracker; set => _cooldownTracker = value; } 




        private void OnEnable()
        {
            _cooldownTracker = new CooldownTracker(this);
            _cooldownTracker.ResetTracker();
        }

        public bool IsAbilityBehaviourDone()
        {
            return _behaviourReference.IsDone;
        }

        public void Use(GameObject source)
        {
            if (_cooldownTracker.CanUseAbility())
            {
                _cooldownTracker.EnterCooldown();
                _behaviourReference.TriggerBehaviour(source);
            }
        }
    }
}

