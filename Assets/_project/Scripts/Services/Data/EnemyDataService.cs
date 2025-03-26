using System.Collections.Generic;
using System.Linq;
using CodeBase.Configs;
using CodeBase.Core;
using CodeBase.Enemy;
using Zenject;

namespace CodeBase.Servises.Data
{
    public class EnemyDataService : IEnemyDataService
    {
        private const string ENEMIES_CONFIGS_PATH = "Configs/EnemiesConfigs";
        
        [Inject] private IAssetProvider _assetProvider { get; }
        
        private Dictionary<EnemyType, EnemyConfig> _enemyConfigs;

        public void LoadEnemiesConfigs()
        {
            _enemyConfigs = _assetProvider.LoadAllAssets<EnemyConfig>(ENEMIES_CONFIGS_PATH)?.ToDictionary(x => x.EnemyType, x =>x);

        }

        public EnemyConfig GetMonsterConfig(EnemyType enemyType)
        {
            if (_enemyConfigs != null && _enemyConfigs.TryGetValue(enemyType, out var enemyConfig))
            {
                return enemyConfig;
            }

            return default;
        }
    }
}