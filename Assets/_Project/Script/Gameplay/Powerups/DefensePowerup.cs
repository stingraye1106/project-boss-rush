using NF.Main.Gameplay.Character;
using UnityEngine;

namespace NF.Main.Gameplay.Powerups
{
    public class DefensePowerup : APowerup
    {
        public override void ApplyPowerup(GameObject picker)
        {
            if (picker.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.HasDefenseBuff = true;
                StartRemoveTimer(picker);
            }
        }

        public override void RemovePowerup(GameObject picker)
        {
            if (picker.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.HasDefenseBuff = false;
            }
        }
    }
}

