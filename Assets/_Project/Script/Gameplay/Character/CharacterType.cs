using UnityEngine;

namespace NF.Main.Gameplay.Character
{
    [CreateAssetMenu(fileName = "CharacterType", menuName = "ScriptableObject/Characters/CharacterType")]
    public class CharacterType : ScriptableObject
    {
        [SerializeField] private string _characterId;

        public string CharacterId => _characterId;
    }

}
