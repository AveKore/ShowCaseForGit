using CodeBase.Configs;
using CodeBase.Entity;

namespace CodeBase.Enemy
{
    public class EnemyHealthModel : EntityHealthModel
    {
        public void Init(EnemyConfig config)
        {
            MaxHealth.Value = config.Health;
            Health.Value = MaxHealth.Value;
        }
    }
}