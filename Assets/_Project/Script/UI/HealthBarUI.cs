using NF.Main.Gameplay.Stats;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NF.Main.UI
{
    public class HealthBarUI : MonoBehaviour
    {
        [TabGroup("References")][SerializeField] private HealthStat _healthStat;

        [TabGroup("UI")][SerializeField] private TextMeshProUGUI _healthText;
        [TabGroup("UI")][SerializeField] private Image _healthFill;
        private float _healthFillValue;
        private float _lerpSpeed = 10f;

        private void OnEnable()
        {
            _healthFillValue = _healthFill.fillAmount;
        }

        void Update()
        {
            SetText((int)_healthStat.CurrentValue, (int)_healthStat.DefaultValue);
            UpdateHealthFillValue(_healthStat.CurrentValue, _healthStat.DefaultValue);
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            if (_healthFill.fillAmount != _healthFillValue)
            {
                _healthFill.fillAmount = Mathf.Lerp(_healthFill.fillAmount, _healthFillValue, _lerpSpeed * Time.deltaTime);
            }
        }

        void UpdateHealthFillValue(float currentHp, float maxHp)
        {
            _healthFillValue = Mathf.Clamp01(currentHp / maxHp);
        }

        void SetText(int currentHp, int maxHp)
        {
            _healthText.text = $"{currentHp} / {maxHp}";
        }
    }
}

