using System.Collections.Generic;
using CodeBase.Hero;

namespace CodeBase.Configs
{
    public class PlayerProgressData
    {
        public readonly Dictionary<CharacteristicType, HeroCharacteristicModel> CharacteristicModels = new();
        public int SkillPoints;
    }
}