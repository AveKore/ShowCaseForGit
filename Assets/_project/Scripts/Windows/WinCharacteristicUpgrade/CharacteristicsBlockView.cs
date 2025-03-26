using CodeBase.Configs;
using CodeBase.Core.Factory;
using CodeBase.Extencions;
using CodeBase.Hero;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Windows
{
    public class CharacteristicsBlockView : MonoBehaviour
    {
        [SerializeField] private CharacteristicsUpgradeView _characteristicsUpgradeViewPrefab;
        [SerializeField] private Transform _pool;
        
        private CharacteristicsUpgradeViewFactory _characteristicsUpgradeViewFactory;
        private bool _isInitialized;
        private HeroModel _heroModel;

        public void Init(
            HeroModel heroModel, 
            PlayerBaseCharacteristicsConfig playerBaseCharacteristicsConfig,
            IPersistentProgressService persistentProgressService,
            IInterfaceLocalizationService localizationService)
        {
            _heroModel = heroModel;
            SingleInitialize();
            foreach (var characteristic in playerBaseCharacteristicsConfig.characteristics)
            {
                TryCreateCharacteristic(persistentProgressService, characteristic, localizationService);
            }
        }

        public void Dispose()
        {
            _characteristicsUpgradeViewFactory?.ReleasePool();
        }

        private void TryCreateCharacteristic(
            IPersistentProgressService persistentProgressService,
            CharacteristicConfig characteristic, 
            IInterfaceLocalizationService localizationService)
        {
            if (CharacteristicExists(persistentProgressService, characteristic, out var characteristicModel) && characteristic.IsUpgradable)
            {
                _characteristicsUpgradeViewFactory.CreateElement(
                    _heroModel, 
                    characteristicModel, 
                    characteristic,
                    persistentProgressService,
                    localizationService);
            }
        }

        private static bool CharacteristicExists(
            IPersistentProgressService persistentProgressService,
            CharacteristicConfig characteristic, 
            out HeroCharacteristicModel characteristicModel)
        {
            return persistentProgressService.PlayerProgress.TryGetCharacteristicByType(characteristic.Type, out characteristicModel);
        }

        private void SingleInitialize()
        {
            if (_isInitialized)
            {
                return;
            }
            _characteristicsUpgradeViewFactory = new(_characteristicsUpgradeViewPrefab, _pool);
            _isInitialized = true;
        }
    }
}