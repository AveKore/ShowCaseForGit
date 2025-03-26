using CodeBase.Configs;
using CodeBase.Entity;
using CodeBase.Extencions;

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
            if (playerProgress.TryGetCharacteristicByType(CharacteristicType.MaxHealth, out var characteristic))
            {
                MaxHealth.Value = characteristic.IntValue;
            }
          
            Health.Value = MaxHealth.Value;
        }
    }
}