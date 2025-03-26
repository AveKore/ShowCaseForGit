using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Hero;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Windows;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Core.Factory
{
    public class CharacteristicsUpgradeViewFactory
    {
        private readonly CharacteristicsUpgradeView _characteristicsUpgradeViewPrefab;
        private readonly Transform _characteristicsParent;
        private readonly ObjectPool<CharacteristicsUpgradeView> _characteristicsUpgradeViewPool;
        
        private List<CharacteristicsUpgradeView> _characteristicViews = new ();

        public CharacteristicsUpgradeViewFactory(
            CharacteristicsUpgradeView characteristicsUpgradeViewPrefab,
            Transform characteristicsParent)
        {
            _characteristicsUpgradeViewPrefab = characteristicsUpgradeViewPrefab;
            _characteristicsParent = characteristicsParent;
            _characteristicsUpgradeViewPool =
                new ObjectPool<CharacteristicsUpgradeView>(CreateFunc, GetAction, ReleaseAction);
        }

        public void CreateElement(
            HeroModel heroModel,
            HeroCharacteristicModel heroCharacteristicsModel,
            CharacteristicConfig characteristicConfig,
            IPersistentProgressService persistentProgressService,
            IInterfaceLocalizationService localizationService)
        {
            var view = _characteristicsUpgradeViewPool.Get();
            view.Init(heroModel, heroCharacteristicsModel, characteristicConfig, persistentProgressService, localizationService);
            _characteristicViews.Add(view);
        }
        
        public void ReleasePool()
        {
            foreach (var view in _characteristicViews)
            {
                _characteristicsUpgradeViewPool.Release(view);
            }
            _characteristicViews.Clear();
        }
        
        private void ReleaseAction(CharacteristicsUpgradeView view)
        {
            view.gameObject.SetActive(false);
        }

        private void GetAction(CharacteristicsUpgradeView view)
        {
            view.gameObject.SetActive(true);
        }

        private CharacteristicsUpgradeView CreateFunc()
        {
            return GameObject.Instantiate(_characteristicsUpgradeViewPrefab, _characteristicsParent);
        }
    }
}