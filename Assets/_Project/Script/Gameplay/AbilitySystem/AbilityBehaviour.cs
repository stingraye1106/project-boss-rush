using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "ScriptableObject/Ability System/AbilityBehaviour")]
    public class AbilityBehaviour : ScriptableObject
    {
        [SerializeField] private List<AbilityPhase> _abilityPhases;

        public List<AbilityPhase> AbilityPhases => _abilityPhases;



        private IEnumerator ExecutePhases(GameObject abilityUser)
        {
            foreach (var phase in _abilityPhases)
            {
                yield return phase.TriggerPhase(abilityUser);
            }
        }

        public void TriggerBehaviour(GameObject abilityUser)
        {
            abilityUser.GetComponent<MonoBehaviour>().StartCoroutine(ExecutePhases(abilityUser));
        }
    }
}

