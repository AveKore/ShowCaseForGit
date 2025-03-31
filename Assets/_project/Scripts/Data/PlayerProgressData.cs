using System.Collections.Generic;
using CodeBase.Hero;

namespace CodeBase.Configs
{
    public class PlayerProgressData
    {
        public Dictionary<CharacteristicType, StatLevel> StatsProgress = new();
        public int SkillPoints;
    }
}