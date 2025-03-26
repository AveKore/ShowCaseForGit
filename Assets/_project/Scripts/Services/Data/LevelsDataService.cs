using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.Core;
using Zenject;

namespace CodeBase.Servises.Data
{
    public class LevelsDataService : ILevelsDataService
    {
        private const string LEVELS_CONFIGS_PATH = "Configs/LevelsConfigs";
        
        [Inject] private IAssetProvider _assetProvider { get; }
        
        private Dictionary<string, LevelConfig> _levelsConfigs;

        public void LoadLevelsConfigs()
        {
            _levelsConfigs = _assetProvider.LoadAllAssets<LevelConfig>(LEVELS_CONFIGS_PATH)?.ToDictionary(x => x.LevelId, x =>x);
        }

        public LevelConfig GetNextSceneConfig(string sceneId)
        {
            if (_levelsConfigs != null && _levelsConfigs.TryGetValue(sceneId, out var levelConfig))
            {
                return levelConfig.NextLevelConfig;
            }

            return default;
        }
        
        public string GetSceneName(string sceneId)
        {
            if (_levelsConfigs != null && _levelsConfigs.TryGetValue(sceneId, out var levelConfig))
            {
                return levelConfig.Name;
            }

            return default;
        }

        public string GetFirstDataId()
        {
            return _levelsConfigs?.First().Key ?? string.Empty;
        }
    }
}