using CodeBase.Configs;
using CodeBase.Entity;

namespace CodeBase.Hero
{
    public class HeroHealthModel : EntityHealthModel
    {
        public void UpdatePlayerMaxHealth(int maxHealth)
        {
            Health.Value += maxHealth - MaxHealth.Value;
            MaxHealth.Value = maxHealth;
        }
        
        public void LoadProgress(PlayerProgressData playerProgress)
        {
            MaxHealth.Value = (int)playerProgress.StatsProgress[CharacteristicType.MaxHealth].Value;
            Health.Value = MaxHealth.Value;
        }
    }
}