using CodeBase.Configs;
using CodeBase.Entity;
using CodeBase.Extencions;

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
            if (playerProgress.TryGetCharacteristicByType(CharacteristicType.Damage, out var characteristic))
            {
                Damage.Value = characteristic.IntValue;
            }
            if (playerProgress.TryGetCharacteristicByType(CharacteristicType.AttackRadius, out characteristic))
            {
                AttackRadius.Value = characteristic.FloatValue;
            }
            if (playerProgress.TryGetCharacteristicByType(CharacteristicType.AttackCooldown, out characteristic))
            {
                AttackCooldown.Value = characteristic.FloatValue;
            }
        }
    }
}