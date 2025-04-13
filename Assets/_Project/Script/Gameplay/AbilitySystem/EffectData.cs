
using System;
using System.Collections.Generic;

namespace NF.Main.Gameplay.AbilitySystem
{
    [Serializable]
    public class EffectData
    {
        public float NormalizedTime;
        public List<AEffect> Effect;
    }
}