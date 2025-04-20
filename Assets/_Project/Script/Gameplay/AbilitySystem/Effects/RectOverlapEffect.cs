using System.Collections.Generic;
using NF.Main.Gameplay.Character;
using NF.Main.Gameplay.Stats;
using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem.Effects
{
    [CreateAssetMenu(fileName = "RectOverlapEffect", menuName = "ScriptableObject/Ability System/Effects/RectOverlapEffect")]
    public class RectOverlapEffect : AEffect
    {
        [SerializeField] private Stat _width;
        [SerializeField] private Stat _height;
        [SerializeField] private Stat _depth;
        [SerializeField] private List<AEffect> _effects;
        [SerializeField] private LayerMask _layerMask;
        private float _offset = 2f;
        private float _dotThreshold = 0.25f;


        // Returns a list of colliders based from center, width, height, depth, and rotation using Physics.OverlapBox.
        private Collider[] GetHitColliders(Vector3 centerPos, float width, float height, float depth, Quaternion rotation)
        {
            var boxExtents = new Vector3(width, height, depth);
            var offset = Vector3.forward * _offset;
            return Physics.OverlapBox(centerPos + offset, boxExtents, rotation, _layerMask);
        }

        // Function for getting the dot product which will be used if the target is in front of the source/user of the ability
        private float GetDotFromSourceToTarget(GameObject source, GameObject target)
        {
            Vector3 toTarget = (target.transform.position - source.transform.position).normalized;
            float dot = Vector3.Dot(source.transform.forward, toTarget);
            return dot;
        }


        // Logic for applying the rect overlap effect.
        public override void ApplyEffect(GameObject source, GameObject target = null)
        {
            var hitColliders = GetHitColliders(source.transform.position, _width.DefaultValue, _height.DefaultValue, _depth.DefaultValue, source.transform.rotation);
            var sourceCharacter = source.GetComponent<ACharacter>();
            var charactersHit = new List<GameObject>();

            Debug.Log($"Colliders found: {hitColliders.Length}");

            foreach (var collider in hitColliders)
            {
                Debug.Log(collider.gameObject.name);
                // Prevents friendly fire. Instead of comparing string-based tags, we compare the character types within the character class.
                // Also check if it is in front of the source.
                var colliderCharacter = collider.GetComponent<ACharacter>();
                var dotFromSourceToHit = GetDotFromSourceToTarget(source, collider.gameObject);
                if (colliderCharacter.CharacterType != sourceCharacter.CharacterType && dotFromSourceToHit > _dotThreshold)
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

