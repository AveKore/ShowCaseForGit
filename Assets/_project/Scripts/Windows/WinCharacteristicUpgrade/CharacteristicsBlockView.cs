using CodeBase.Core.Factory;
using CodeBase.Hero;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace CodeBase.Windows
{
    public class CharacteristicsBlockView : MonoBehaviour
    {
        [SerializeField] private CharacteristicsUpgradeView _characteristicsUpgradeViewPrefab;
        [SerializeField] private Transform _pool;
        
        private CharacteristicsUpgradeViewFactory _characteristicsUpgradeViewFactory;
        private bool _isInitialized;
        private HeroModel _heroModel;

        [Inject] private IPersistentProgressService _persistentProgressService;
        [Inject] private DiContainer _diContainer;
        
        public void Init(HeroModel heroModel)
        {
            _heroModel = heroModel;
            SingleInitialize();
            foreach (var characteristic in _persistentProgressService.StatsConfigs)
            {
                if (characteristic.Value.Levels[0].Cost == 0)
                {
                    continue;
                }
                _characteristicsUpgradeViewFactory.CreateElement(_heroModel, characteristic.Key);
            }
        }

        public void Dispose()
        {
            _characteristicsUpgradeViewFactory?.ReleasePool();
        }

        private void SingleInitialize()
        {
            if (_isInitialized)
            {
                return;
            }
            _characteristicsUpgradeViewFactory = new(_characteristicsUpgradeViewPrefab, _pool, _diContainer);
            _isInitialized = true;
        }
    }
}