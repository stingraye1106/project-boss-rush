
using System.Collections.Generic;

namespace NF.Main.Gameplay.AbilitySystem
{
    // Convert input effect list into usable dictionary
    public class EffectListToDictionaryConverter
    {
        public Dictionary<float, List<AEffect>> GetEffectDictionary(List<EffectData> effectList)
        {
            var dictionary = new Dictionary<float, List<AEffect>>();

            foreach (var effectEntry in effectList)
            {
                dictionary[effectEntry.NormalizedTime] = effectEntry.Effect;
            }

            return dictionary;
        }
    }
}

