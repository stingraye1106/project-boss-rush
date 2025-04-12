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



        // Returns a list of colliders based from center, width, height, depth, and rotation using Physics.OverlapBox.
        private Collider[] GetHitColliders(Vector3 centerPos, float width, float height, float depth, Quaternion rotation)
        {
            var boxExtents = new Vector3(width, height, depth);
            return Physics.OverlapBox(centerPos, boxExtents, rotation);
        }


        // Logic for applying the rect overlap effect.
        public override void ApplyEffect(GameObject user)
        {
            var hitColliders = GetHitColliders(user.transform.position, _width.DefaultValue, _height.DefaultValue, _depth.DefaultValue, user.transform.rotation);
            var userCharacter = user.GetComponent<ACharacter>();
            var charactersHit = new List<GameObject>();

            foreach (var collider in hitColliders)
            {
                // Prevents friendly fire. Instead of comparing string-based tags, we compare the character types within the character class.
                var colliderCharacter = collider.GetComponent<ACharacter>();
                if (colliderCharacter.CharacterType != userCharacter.CharacterType)
                {
                    var hitCharacter = collider.gameObject;

                    // Ensures that a character is hit once from this effect.
                    if (!charactersHit.Contains(hitCharacter)) 
                    {
                        // Go through the list of sub-effects to apply to the hit character.
                        foreach (var effect in _effects)
                        {
                            effect.ApplyEffect(hitCharacter);
                        }

                        charactersHit.Add(hitCharacter);
                    }
                }
            }
        }
    }
}

