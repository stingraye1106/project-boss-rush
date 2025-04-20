using NF.Main.Gameplay.Character;
using UnityEngine;

namespace NF.Main.Gameplay.Powerups
{
    public class AttackPowerup : APowerup
    {
        public override void ApplyPowerup(GameObject picker)
        {
            if (picker.TryGetComponent<IAttacker>(out var attacker))
            {
                attacker.HasAttackBuff = true;
                StartRemoveTimer(picker);
            }
        }

        public override void RemovePowerup(GameObject picker)
        {
            if (picker.TryGetComponent<IAttacker>(out var attacker))
            {
                attacker.HasAttackBuff = false;
            }
        }
    }
}

