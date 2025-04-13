using NF.Main.Gameplay.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Ability System/Ability")]
    public class Ability : ScriptableObject
    {
        [SerializeField] private string _uniqueId;
        [SerializeField] private AbilityBehaviour _behaviourReference;
        [SerializeField] private Stat _cooldown;
        private float _remainingCooldown;

        public string UniqueId => _uniqueId;
        public AbilityBehaviour BehaviourReference => _behaviourReference;
        public Stat Cooldown => _cooldown;
        [ShowInInspector, ReadOnly] public float RemainingCooldown { get { return _remainingCooldown; } set { _remainingCooldown = value; } }



        public bool CanUse()
        {
            return _remainingCooldown == 0f;
        }

        public void ResetCooldown()
        {
            _remainingCooldown = 0f;
        }

        public void Use(GameObject source)
        {
            Debug.Log($"Used ability!");
            if (CanUse())
            {
                _behaviourReference.TriggerBehaviour(source);
            }
        }
    }
}

