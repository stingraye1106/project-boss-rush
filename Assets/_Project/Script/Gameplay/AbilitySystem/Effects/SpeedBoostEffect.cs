using NF.Main.Gameplay.Character;
using NF.Main.Gameplay.Movement;
using NF.Main.Gameplay.Stats;
using System.Collections;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem.Effects
{
    [CreateAssetMenu(fileName = "SpeedBoostEffect", menuName = "ScriptableObject/Ability System/Effects/SpeedBoostEffect")]
    public class SpeedBoostEffect : AEffect
    {
        [SerializeField] private Stat _speedMultiplierBoost;
        [SerializeField] private Stat _defaultDuration;

        private IEnumerator IEApplySprint(IMovement sourceMovement)
        {
            var defaultMultiplier = sourceMovement.SpeedMultiplier;
            sourceMovement.SpeedMultiplier = _speedMultiplierBoost.DefaultValue;
            Debug.Log($"{sourceMovement.SpeedMultiplier}");
            var timer = _defaultDuration.DefaultValue;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            sourceMovement.SpeedMultiplier = defaultMultiplier;
        }

        public override void ApplyEffect(GameObject source, GameObject target = null) 
        {
            var sourceMovement = source.GetComponent<IMovement>();
            source.GetComponent<ACharacter>().StartCoroutine(IEApplySprint(sourceMovement));
        }
    }
}

