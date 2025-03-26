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

        public void UpgradeCharacteristic(
            CharacteristicType characteristicType,
            CharacteristicLevelConfig characteristicConfigLevel)
        {
            switch (characteristicType)
            {
                case CharacteristicType.Damage:
                    DamageModel.PowerUpDamage(characteristicConfigLevel.IntValue);
                    break;
                case CharacteristicType.Speed:
                    SpeedModel.BoostSpeed(characteristicConfigLevel.FloatValue);
                    break;
                case CharacteristicType.MaxHealth:
                    HealthModel.UpdatePlayerMaxHealth(characteristicConfigLevel.IntValue);
                    break;
                case CharacteristicType.AttackRadius:
                case CharacteristicType.AttackCooldown:
                default:
                    break;
            }
        }
    }
}