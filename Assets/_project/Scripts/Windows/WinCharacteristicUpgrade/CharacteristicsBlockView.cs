using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Core.Factory;
using CodeBase.Hero;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Windows
{
    public class CharacteristicsBlockView : MonoBehaviour
    {
        [SerializeField] private Button _saveChanges;
        [SerializeField] private CharacteristicsUpgradeView _characteristicsUpgradeViewPrefab;
        [SerializeField] private Transform _pool;
        
        private CharacteristicsUpgradeViewFactory _characteristicsUpgradeViewFactory;
        private bool _isInitialized;
        private HeroModel _heroModel;
        private int _startSkillPoints;
        private readonly ReactiveDictionary<CharacteristicType, StatLevel> _tempUpgradedStats = new();

        
        [Inject] private IPersistentProgressService _persistentProgressService;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private DiContainer _diContainer;
   

        public void Init(HeroModel heroModel)
        {
            _heroModel = heroModel;
            _startSkillPoints = _heroModel.SkillPointsModel.SkillPoints.Value;
            _tempUpgradedStats.Clear();
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
            RevertUnsavedChanges();

            _characteristicsUpgradeViewFactory?.ReleasePool();
        }

        private void RevertUnsavedChanges()
        {
            if (_tempUpgradedStats.Count == 0)
            {
                return;
            }
            _heroModel?.SkillPointsModel.SetSkillPoints(_startSkillPoints);
            _tempUpgradedStats.Clear();
        }

        private void SingleInitialize()
        {
            if (_isInitialized)
            {
                return;
            }
            _saveChanges.onClick.RemoveAllListeners();
            _saveChanges.onClick.AddListener(SaveCharges);
            _tempUpgradedStats.ObserveCountChanged().Subscribe(UpdateInteractableButtonStatus);
            _characteristicsUpgradeViewFactory = new(_characteristicsUpgradeViewPrefab, _pool, _diContainer, AddStatToTempUpdated);
            _isInitialized = true;
        }

        private void UpdateInteractableButtonStatus(int count)
        {
            _saveChanges.interactable = count != 0;
        }

        private void SaveCharges()
        {
            foreach (var stat in _tempUpgradedStats)
            {
                _persistentProgressService.Upgrade(stat.Key, stat.Value);
                _heroModel.UpgradeCharacteristic(stat.Key, stat.Value);
            }
            _saveLoadService.Save(_persistentProgressService.PlayerProgress);
            _tempUpgradedStats.Clear();
        }

        private void AddStatToTempUpdated(StatLevel statLevel, CharacteristicType characteristicType)
        {
            if (!_tempUpgradedStats.TryAdd(characteristicType, statLevel))
            {
                _tempUpgradedStats[characteristicType] = statLevel;
            }
        }
    }
}