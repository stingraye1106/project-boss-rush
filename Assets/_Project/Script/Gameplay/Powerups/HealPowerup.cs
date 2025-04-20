using NF.Main.Gameplay.Character;
using UnityEngine;

namespace NF.Main.Gameplay.Powerups
{
    public class HealPowerup : APowerup
    {
        [SerializeField] private float _healAmount = 15f;

        public override void ApplyPowerup(GameObject picker)
        {
            if (picker.TryGetComponent<IHealable>(out var healTarget))
            {
                healTarget.Heal(_healAmount);
            }
        }

        public override void RemovePowerup(GameObject picker)
        {
        }
    }

}
