using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityBehaviour", menuName = "ScriptableObject/Ability System/AbilityBehaviour")]
    public class AbilityBehaviour : ScriptableObject
    {
        [SerializeField] private List<AbilityPhase> _abilityPhases;
        private bool _isDone;

        public List<AbilityPhase> AbilityPhases => _abilityPhases;
        public bool IsDone => _isDone;



        private void ResetBehaviour()
        {
            foreach (var phase in _abilityPhases) 
            {
                phase.Reset(); 
            }

            _isDone = false;
        }

        private bool ArePhasesDone()
        {
            foreach (var phase in _abilityPhases)
            {
                if (!phase.IsDone)
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerator RunPhases(GameObject source)
        {
            foreach (var phase in _abilityPhases)
            {
                yield return phase.TriggerPhase(source);
            }

            if (ArePhasesDone())
            {
                _isDone = true;
            }
        }

        public void TriggerBehaviour(GameObject source)
        {
            ResetBehaviour();
            source.GetComponent<MonoBehaviour>().StartCoroutine(RunPhases(source));
        }
    }
}

