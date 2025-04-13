using System.Collections.Generic;
using NF.Main.Gameplay.Character;
using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem.Effects
{
    [CreateAssetMenu(fileName = "SphereOverlapEffect", menuName = "ScriptableObject/Ability System/Effects/SphereOverlapEffect")]
    public class SphereOverlapEffect : AEffect
    {
        [SerializeField] private Stat _radius;
        [SerializeField] private List<AEffect> _effects;
        [SerializeField] private LayerMask _layerMask;


        // Returns a list of colliders based from provided center position and radius values.
        private Collider[] GetHitColliders(Vector3 centerPos, float radius)
        {
            return Physics.OverlapSphere(centerPos, radius, _layerMask);
        }

        // Logic for applying the sphere overlap effect
        public override void ApplyEffect(GameObject source, GameObject target = null) 
        { 
            var hitColliders = GetHitColliders(source.transform.position, _radius.DefaultValue);
            var sourceCharacter = source.GetComponent<ACharacter>();
            var charactersHit = new List<GameObject>();

            Debug.Log($"Colliders found: {hitColliders.Length}");

            foreach (var collider in hitColliders)
            {
                // Prevents friendly fire. Instead of comparing string-based tags, we compare the character types within the character class.
                var colliderCharacter = collider.GetComponent<ACharacter>();
                if (colliderCharacter.CharacterType != sourceCharacter.CharacterType)
                {
                    var hitCharacter = collider.gameObject;

                    // Ensures that a character is hit once from this effect.
                    if (!charactersHit.Contains(hitCharacter))
                    {
                        // Go through the list of sub-effects to apply to the hit character.
                        foreach (var effect in _effects)
                        {
                            effect.ApplyEffect(source, hitCharacter);
                        }

                        charactersHit.Add(hitCharacter);
                    }
                }
            }
        }
    }
}
