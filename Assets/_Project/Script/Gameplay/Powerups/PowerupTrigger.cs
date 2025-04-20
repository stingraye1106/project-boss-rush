using NF.Main.Core.Pooling;
using NF.Main.Gameplay.Character;
using UnityEngine;

namespace NF.Main.Gameplay.Powerups
{
    public class PowerupTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ACharacter>(out var character))
            {
                var powerup = GetComponent<APowerup>();
                powerup.ApplyPowerup(character.gameObject);
                RemovePowerupFromScene();
            }
        }

        private void RemovePowerupFromScene()
        {
            var pooledObject = GetComponent<IPoolableObject>();
            pooledObject?.ReturnToPool();
            gameObject.SetActive(false);
        }
    }
}

