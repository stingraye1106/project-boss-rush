using NF.Main.Gameplay.Character;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NF.Main.UI
{
    public class BuffIndicatorUI : MonoBehaviour
    {
        [TabGroup("UI References")][SerializeField] private GameObject _attackBuffIndicator;
        [TabGroup("UI References")][SerializeField] private GameObject _defenseBuffIndicator;
        [TabGroup("Character")][SerializeField] private PlayerCharacter _playerCharacter;

        void Update()
        {
            _attackBuffIndicator.SetActive(_playerCharacter.HasAttackBuff);
            _defenseBuffIndicator.SetActive(_playerCharacter.HasDefenseBuff);
        }
    }
}
