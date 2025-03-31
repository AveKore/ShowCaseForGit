using CodeBase.Configs;
using CodeBase.Entity;

namespace CodeBase.Hero
{
    public class HeroDamageModel : EntityDamageModel
    {
        
        public void PowerUpDamage(int damage)
        {
            Damage.Value += damage;
        }
        
        public void LoadProgress(PlayerProgressData playerProgress)
        {
            Damage.Value = (int)playerProgress.StatsProgress[CharacteristicType.Damage].Value;
            AttackRadius.Value = playerProgress.StatsProgress[CharacteristicType.AttackRadius].Value;
            AttackCooldown.Value = playerProgress.StatsProgress[CharacteristicType.AttackCooldown].Value;
        }
    }
}