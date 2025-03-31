using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Windows;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace CodeBase.Core.Factory
{
    public class CharacteristicsUpgradeViewFactory
    {
        private readonly CharacteristicsUpgradeView _characteristicsUpgradeViewPrefab;
        private readonly Transform _characteristicsParent;
        private readonly ObjectPool<CharacteristicsUpgradeView> _characteristicsUpgradeViewPool;
        
        private List<CharacteristicsUpgradeView> _characteristicViews = new ();
        private readonly DiContainer _diContainer;

        public CharacteristicsUpgradeViewFactory(
            CharacteristicsUpgradeView characteristicsUpgradeViewPrefab,
            Transform characteristicsParent, DiContainer diContainer)
        {
            _characteristicsUpgradeViewPrefab = characteristicsUpgradeViewPrefab;
            _characteristicsParent = characteristicsParent;
            _diContainer = diContainer;
            _characteristicsUpgradeViewPool =
                new ObjectPool<CharacteristicsUpgradeView>(CreateFunc, GetAction, ReleaseAction);
        }

        public void CreateElement(HeroModel heroModel, CharacteristicType characteristicType)
        {
            var view = _characteristicsUpgradeViewPool.Get();
            view.Init(heroModel, characteristicType);
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
            return _diContainer.InstantiatePrefabForComponent<CharacteristicsUpgradeView>(_characteristicsUpgradeViewPrefab, _characteristicsParent);
        }
    }
}