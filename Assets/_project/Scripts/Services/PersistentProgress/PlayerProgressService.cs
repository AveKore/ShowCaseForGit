using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Hero;
using CodeBase.Services.SaveLoad;
using Zenject;

namespace CodeBase.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private Dictionary<CharacteristicType, CharacterStat> _statsConfigs = new();
        public Dictionary<CharacteristicType, CharacterStat> StatsConfigs => _statsConfigs;
        public PlayerProgressData  PlayerProgress { get; set; }
        
        [Inject] private ISaveLoadService _saveLoadService;


        public void LoadPlayerConfigs()
        {
            _statsConfigs = _saveLoadService.LoadStatsFromExcel();
        }
        
        public PlayerProgressData CreatePlayerProgress()
        {
            PlayerProgress = new PlayerProgressData
            {
                SkillPoints = 0,
                StatsProgress = new Dictionary<CharacteristicType, StatLevel>(),
            };

            foreach (var configs in _statsConfigs)
            {
                PlayerProgress.StatsProgress.Add(configs.Key, configs.Value.Levels[0]);
            }
            return PlayerProgress;
        }
        
        public void Upgrade(CharacteristicType characteristicType, StatLevel statValue)
        {
            PlayerProgress.StatsProgress[characteristicType] = statValue;
        }
    }
}