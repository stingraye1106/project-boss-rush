using UnityEngine;

namespace NF.Main.Gameplay.AbilitySystem
{
    public class CooldownTracker 
    {
        private Ability _ability;
        private float _lastUsedTime;

        public CooldownTracker(Ability ability)
        {
            _ability = ability;
            _lastUsedTime = -Mathf.Infinity;
        }

        public bool CanUseAbility()
        {
            return Time.time >= _lastUsedTime + _ability.Cooldown.DefaultValue;
        }

        public void EnterCooldown()
        {
            _lastUsedTime = Time.time;
        }

        public float GetTimeRemaining()
        {
            var timeRemaining = (_lastUsedTime + _ability.Cooldown.DefaultValue) - Time.time;
            return Mathf.Max(0, timeRemaining);
        }

        public float GetCooldownPercent()
        {
            float elapsed = Time.time - _lastUsedTime;
            return Mathf.Clamp01(elapsed / _ability.Cooldown.DefaultValue);
        }

        public void ResetTracker()
        {
            _lastUsedTime = -Mathf.Infinity;
        }
    }
}

