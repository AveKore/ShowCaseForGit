using CodeBase.Configs;
using CodeBase.Hero;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgressData PlayerProgress { get; set; }
        
        public PlayerProgressData CreatePlayerProgress(PlayerBaseCharacteristicsConfig playerConfig);

        public void Upgrade(CharacteristicConfig characteristicConfig, HeroCharacteristicModel characteristicModel);
    }
}