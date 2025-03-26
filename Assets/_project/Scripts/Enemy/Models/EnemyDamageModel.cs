using CodeBase.Configs;
using CodeBase.Entity;

namespace CodeBase.Enemy
{
    public class EnemyDamageModel : EntityDamageModel
    {
        public void Init(EnemyConfig enemyConfig)
        {
            Damage.Value = enemyConfig.Damage;
            AttackCooldown.Value = enemyConfig.AttackCooldown;
            AttackRadius.Value = enemyConfig.AttackRadius;
        }
    }
}