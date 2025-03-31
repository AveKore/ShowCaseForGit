using CodeBase.Configs;

namespace CodeBase.Hero
{
    public class HeroModel
    {
        public readonly HeroHealthModel HealthModel = new();
        public readonly HeroDamageModel DamageModel = new();
        public readonly HeroSpeedModel SpeedModel = new();
        public readonly HeroSkillPointsModel SkillPointsModel = new();
        public HeroModel(PlayerProgressData playerProgress)
        {
            HealthModel.LoadProgress(playerProgress);
            DamageModel.LoadProgress(playerProgress);
            SpeedModel.LoadProgress(playerProgress);
            SkillPointsModel.LoadProgress(playerProgress);
        }

        public void UpgradeCharacteristic(CharacteristicType characteristicType, StatLevel statLevel)
        {
            switch (characteristicType)
            {
                case CharacteristicType.Damage:
                    DamageModel.PowerUpDamage((int)statLevel.Value);
                    break;
                case CharacteristicType.Speed:
                    SpeedModel.BoostSpeed(statLevel.Value);
                    break;
                case CharacteristicType.MaxHealth:
                    HealthModel.UpdatePlayerMaxHealth((int)statLevel.Value);
                    break;
                case CharacteristicType.AttackRadius:
                case CharacteristicType.AttackCooldown:
                default:
                    break;
            }
        }
    }
}