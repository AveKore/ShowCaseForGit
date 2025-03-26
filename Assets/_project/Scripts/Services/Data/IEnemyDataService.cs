using CodeBase.Configs;
using CodeBase.Enemy;
using CodeBase.Services;

namespace CodeBase.Servises.Data
{
    public interface IEnemyDataService : IService
    {
        public void LoadEnemiesConfigs();
        
        public EnemyConfig GetMonsterConfig(EnemyType enemyType);
            
    }
}