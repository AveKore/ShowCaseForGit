using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Hero;

namespace CodeBase.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgressData PlayerProgress { get; set; }
        public Dictionary<CharacteristicType, CharacterStat> StatsConfigs { get;  }

        public PlayerProgressData CreatePlayerProgress();

        public void Upgrade(CharacteristicType characteristicType);
    }
}