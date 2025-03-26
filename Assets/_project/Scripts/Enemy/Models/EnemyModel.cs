using CodeBase.Configs;

namespace CodeBase.Enemy
{
    public class EnemyModel
    {
        public string EnemyId { get; set; }
        
        public readonly EnemyHealthModel HealthModel = new();
        public readonly EnemyDamageModel DamageModel = new();
        public readonly EnemySpeedModel SpeedModel = new();
        
        public readonly int Reward;
        
        public EnemyModel(EnemyConfig enemyConfig, string newEnemyId)
        {
            Reward = enemyConfig.SkillPointsReward;
            EnemyId = newEnemyId;
            HealthModel.Init(enemyConfig);
            DamageModel.Init(enemyConfig);
            SpeedModel.Init(enemyConfig);
        }
    }
}