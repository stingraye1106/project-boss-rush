using System;
using System.Collections;
using System.Collections.Generic;
using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    [CreateAssetMenu(fileName = "AbilityPhase", menuName = "ScriptableObject/Ability System/AbilityPhase")]
    public class AbilityPhase : ScriptableObject
    {
        [SerializeField] private Stat _duration;
        [SerializeField] private List<EffectData> _effectsList;
        private Dictionary<float, List<AEffect>> _effectsDictionary;

        public IEnumerator TriggerPhase(GameObject abilityUser)
        {
            float timer = 0f;
            List<float> triggeredEffectKeys = new List<float>();
            _effectsDictionary = new EffectListToDictionaryConverter().GetEffectDictionary(_effectsList);

            while (timer < _duration.DefaultValue)
            {
                timer += Time.deltaTime;
                float normalizedTime = timer / _duration.DefaultValue;

                foreach (var key in _effectsDictionary.Keys)
                {
                    if (normalizedTime >= key && !triggeredEffectKeys.Contains(key))
                    {
                        foreach (var effect in _effectsDictionary[key])
                        {
                            effect.ApplyEffect(abilityUser);
                        }

                        triggeredEffectKeys.Add(key);
                    }
                }

                yield return null;
            }

            Debug.Log("Phase done");
        }
    }
}

