using System.Collections.Generic;

namespace CodeBase.Configs
{
    public class CharacterStat
    {
        public string Name { get; set; }
        public List<StatLevel> Levels { get; set; }
    }

    public class StatLevel
    {
        public int Level { get; set; }
        public int Cost { get; set; }
        public float Value { get; set; }
    }
}