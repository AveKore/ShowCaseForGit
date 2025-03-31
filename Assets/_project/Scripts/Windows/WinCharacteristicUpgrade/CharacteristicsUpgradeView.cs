using System.Collections.Generic;
using System.Globalization;
using CodeBase.Configs;
using CodeBase.Extencions;
using CodeBase.Hero;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Windows
{
    public class CharacteristicsUpgradeView: MonoBehaviour
    {
        [SerializeField] private TMP_Text _characteristicsNameText;
        [SerializeField] private TMP_Text _characteristicLevelText;
        [SerializeField] private TMP_Text _characteristicCostText;
        [SerializeField] private TMP_Text _characteristicValueText;
        [SerializeField] private GameObject _characteristicValueBlock;
        [SerializeField] private TMP_Text _characteristicNextValueText;
        [SerializeField] private Button _upgradeButton;
       
        private HeroModel _heroModel;
        private Dictionary<CharacteristicType, CharacterStat> _statConfigs;
        private StatLevel _curStatProgress;
        private CharacteristicType _characteristicType;
        
        [Inject] private IPersistentProgressService _persistentProgressService;
        [Inject] private IInterfaceLocalizationService _localizationService;

        public void Init(HeroModel heroModel, CharacteristicType characteristicType)
        {
            _curStatProgress = _persistentProgressService.PlayerProgress.StatsProgress[characteristicType];
            _characteristicType = characteristicType;
            _statConfigs = _persistentProgressService.StatsConfigs;
            _heroModel = heroModel;
            
            _upgradeButton.onClick.RemoveAllListeners();
            _upgradeButton.onClick.AddListener(UpgradeCharacteristics);
            _heroModel.SkillPointsModel.SkillPoints.Subscribe(CheckUpgradeAvailability);
            return;

            void CheckUpgradeAvailability(int skillPoints)
            {
                var nextLevel = _curStatProgress.Level + 1;
                var nameTextKey = $"win-characteristic/{_characteristicType.ToString().ToKebabCase()}/title";
                
                _characteristicsNameText.text =
                    _localizationService.Localize(nameTextKey);
                
                if (_statConfigs[_characteristicType].Levels.Count <= nextLevel)
                {
                    SetMaxText();
                    return;
                }
                var newLevelConfig = _statConfigs[_characteristicType].Levels[nextLevel];

                
                _upgradeButton.interactable = true;
                _characteristicValueBlock.SetActive(true);
                _characteristicLevelText.text = (nextLevel).ToString();
                
                _characteristicValueText.text = _curStatProgress.Value.ToString(CultureInfo.InvariantCulture);
                _characteristicNextValueText.text =  newLevelConfig.Value.ToString(CultureInfo.InvariantCulture);
                _upgradeButton.interactable = skillPoints >= _curStatProgress.Cost;
                _characteristicCostText.text = _curStatProgress.Cost.ToString();
            }
        }

        private void SetMaxText()
        {
            _upgradeButton.interactable = false;
            _characteristicValueBlock.SetActive(false);
            _characteristicNextValueText.text = _curStatProgress.Value.ToString(CultureInfo.InvariantCulture);
            _characteristicLevelText.text = _localizationService.Localize("win-characteristic/max-value/text");
            _characteristicCostText.text = _localizationService.Localize("win-characteristic/max-value/text");
        }

        private void UpgradeCharacteristics()
        {
            var nextLevel = _curStatProgress.Level + 1;
            var newLevelConfig = _statConfigs[_characteristicType].Levels[nextLevel];
            var cost = _curStatProgress.Cost;
            _curStatProgress = newLevelConfig;
            _heroModel.UpgradeCharacteristic(_characteristicType, newLevelConfig);
            _persistentProgressService.Upgrade(_characteristicType);
            _heroModel.SkillPointsModel.RemoveSkillPoints(cost);
     
        }
        
    }
}