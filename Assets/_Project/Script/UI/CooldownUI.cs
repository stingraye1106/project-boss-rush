using NF.Main.Gameplay.AbilitySystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace NF.Main.UI
{
    public class CooldownUI : MonoBehaviour
    {
        [TabGroup("References")][SerializeField] private Ability _ability;

        [TabGroup("UI Components")][SerializeField] private Image _cooldownFill;

        private void OnEnable()
        {
            if (_ability.CooldownTracker.CanUseAbility())
            {
                _cooldownFill.fillAmount = 0f;
            }
        }

        private void Update()
        {
            float percent = _ability.CooldownTracker.GetCooldownPercent();
            _cooldownFill.fillAmount = 1f - percent;
        }
    }
}

