using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    [CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObject/Ability System/Ability")]
    public class Ability : ScriptableObject
    {
        [SerializeField] private string _uniqueId;
        [SerializeField] private AbilityBehaviour _behaviourReference;

        public string UniqueId => _uniqueId;
        public AbilityBehaviour BehaviourReference => _behaviourReference;
    }
}

