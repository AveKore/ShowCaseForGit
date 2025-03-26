using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Project/Configs/PlayerConfig")]
    public class PlayerBaseCharacteristicsConfig : ScriptableObject
    {
        public CharacteristicConfig[] characteristics;
        public int StartSkillPoints;
    }
}