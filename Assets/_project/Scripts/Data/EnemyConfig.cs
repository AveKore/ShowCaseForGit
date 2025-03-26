using CodeBase.Enemy;
using UnityEngine;

namespace CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Project/Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public EnemyType EnemyType;
        public int Speed;
        public int Health;
        public GameObject Prefab;
        public int Damage;
        public int AttackCooldown;
        public float AttackRadius;
        public int SkillPointsReward;
    }
}