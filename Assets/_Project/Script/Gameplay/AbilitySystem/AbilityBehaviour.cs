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



        private void ResetPhases()
        {
            foreach (var phase in _abilityPhases) 
            {
                phase.Reset(); 
            }
            Debug.Log("Phases reset.");
        }

        private IEnumerator RunPhases(GameObject source)
        {
            foreach (var phase in _abilityPhases)
            {
                yield return phase.TriggerPhase(source);
            }
        }

        public void TriggerBehaviour(GameObject source)
        {
            ResetPhases();
            source.GetComponent<MonoBehaviour>().StartCoroutine(RunPhases(source));
        }
    }
}

