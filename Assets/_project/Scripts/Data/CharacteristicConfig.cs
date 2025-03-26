using System.Collections.Generic;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Project/Configs/CharacteristicConfig")]
    public class CharacteristicConfig: ScriptableObject
    {
        public CharacteristicType Type;
        public bool IsUpgradable;
        public List<CharacteristicLevelConfig> Levels;
    }

    public enum ValueType
    {
        StringValue = 0,
        IntValue = 1,
        FloatValue = 2
    }
}