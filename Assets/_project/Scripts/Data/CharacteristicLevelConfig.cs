using System.Globalization;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Project/Configs/CharacteristicLevelConfig")]
    public class CharacteristicLevelConfig : ScriptableObject
    {
        public int Level;
        public int UpgradeCost;
        public string StringValue;
        public int IntValue;
        public float FloatValue;
        
        public ValueType ValueType;
        
        public string GetValueAsString()
        {
            switch (ValueType)
            {
                case ValueType.StringValue:
                    return StringValue;
                case ValueType.IntValue:
                    return IntValue.ToString();
                case ValueType.FloatValue:
                    return FloatValue.ToString(CultureInfo.InvariantCulture);
                default:
                    return string.Empty;
            }
        }
    }
}