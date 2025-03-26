using CodeBase.Configs;
using CodeBase.Entity;

namespace CodeBase.Enemy
{
    public class EnemySpeedModel : EntitySpeedModel
    {
        public void Init(EnemyConfig enemyConfig)
        {
            Speed.Value = enemyConfig.Speed;
        }
    }
}