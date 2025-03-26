using CodeBase.Configs;
using CodeBase.Extencions;
using CodeBase.Hero;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

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
        private HeroCharacteristicModel _heroCharacteristicsModel;
        private CharacteristicConfig _characteristicConfig;
        private IPersistentProgressService _persistentProgressService;
        private IInterfaceLocalizationService _localizationService;

        public void Init(HeroModel heroModel,
            HeroCharacteristicModel heroCharacteristicsModel,
            CharacteristicConfig characteristicConfig,
            IPersistentProgressService persistentProgressService, IInterfaceLocalizationService localizationService)
        {
            
            _heroModel = heroModel;
            _heroCharacteristicsModel = heroCharacteristicsModel;
            _characteristicConfig = characteristicConfig;
            _persistentProgressService = persistentProgressService;
            _localizationService = localizationService;
            
            _upgradeButton.onClick.RemoveAllListeners();
            _upgradeButton.onClick.AddListener(UpgradeCharacteristics);
            _heroModel.SkillPointsModel.SkillPoints.Subscribe(CheckUpgradeAvailability);
            return;

            void CheckUpgradeAvailability(int skillPoints)
            {
                var nextLevel = heroCharacteristicsModel.Level + 1;
                var nameTextKey = $"win-characteristic/{_characteristicConfig.Type.ToString().ToKebabCase()}/title";
                _characteristicsNameText.text =
                    _localizationService.Localize(nameTextKey);
                    
                if (_characteristicConfig.Levels.Count <= nextLevel)
                {
                    SetMaxText();
                    return;
                }
                
                _upgradeButton.interactable = true;
                _characteristicValueBlock.SetActive(true);
                _characteristicLevelText.text = (nextLevel).ToString();
                
                _characteristicValueText.text = _characteristicConfig.Levels[_heroCharacteristicsModel.Level].GetValueAsString();
                _characteristicNextValueText.text = _characteristicConfig.Levels[nextLevel].GetValueAsString();
                _upgradeButton.interactable = skillPoints >= _characteristicConfig.Levels[_heroCharacteristicsModel.Level].UpgradeCost;
                _characteristicCostText.text = _characteristicConfig.Levels[_heroCharacteristicsModel.Level].UpgradeCost.ToString();
            }
        }

        private void SetMaxText()
        {
            _upgradeButton.interactable = false;
            _characteristicValueBlock.SetActive(false);
            _characteristicNextValueText.text = _characteristicConfig.Levels[_heroCharacteristicsModel.Level].GetValueAsString();
            _characteristicLevelText.text = _localizationService.Localize("win-characteristic/max-value/text");
            _characteristicCostText.text = _localizationService.Localize("win-characteristic/max-value/text");
        }

        private void UpgradeCharacteristics()
        {
            var nextLevel = _heroCharacteristicsModel.Level + 1;

            _heroModel.UpgradeCharacteristic(_characteristicConfig.Type, _characteristicConfig.Levels[nextLevel]);
            _persistentProgressService.Upgrade(_characteristicConfig, _heroCharacteristicsModel);
            _heroModel.SkillPointsModel.RemoveSkillPoints(_heroCharacteristicsModel.UpgradeCost);
        }
        
    }
}