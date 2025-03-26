using CodeBase.Configs;
using CodeBase.Hero;

namespace CodeBase.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgressData  PlayerProgress { get; set; }
        
        public PlayerProgressData CreatePlayerProgress(PlayerBaseCharacteristicsConfig playerConfig)
        {
            PlayerProgress = new PlayerProgressData();
            for (var index = 0; index < playerConfig.characteristics.Length; index++)
            {
                var characteristicConfig = playerConfig.characteristics[index];
                var characteristicModel = CreateNewCharacteristicModel(characteristicConfig);
                PlayerProgress.CharacteristicModels.Add(characteristicConfig.Type, characteristicModel);
            }
            
            PlayerProgress.SkillPoints = playerConfig.StartSkillPoints;
            return PlayerProgress;
        }
        
        private HeroCharacteristicModel CreateNewCharacteristicModel(CharacteristicConfig characteristicConfig)
        {
            var model = new HeroCharacteristicModel();
            model.Level = 0;
            model.Type = characteristicConfig.Type;
                
            SetValue(characteristicConfig, model, model.Level);
            model.UpgradeCost = characteristicConfig.Levels[model.Level].UpgradeCost;
            return model;
        }

        private void SetValue(CharacteristicConfig characteristicConfig, HeroCharacteristicModel characteristicModel, int level)
        {
            characteristicModel.FloatValue = characteristicConfig.Levels[level].FloatValue;
            characteristicModel.IntValue = characteristicConfig.Levels[level].IntValue;
            characteristicModel.StringValue = characteristicConfig.Levels[level].StringValue;
        }

        public void Upgrade(CharacteristicConfig characteristicConfig, HeroCharacteristicModel characteristicModel)
        {
            characteristicModel.Level += 1;
            var newLevel = characteristicModel.Level;
            SetValue(characteristicConfig, characteristicModel, newLevel);
            characteristicModel.UpgradeCost = characteristicConfig.Levels[newLevel].UpgradeCost;
        }
    }
}