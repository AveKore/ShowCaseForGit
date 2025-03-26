using System;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Project/Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public string LevelId = Guid.NewGuid().ToString();
        public string Name;
        public LevelConfig NextLevelConfig;
    }
}