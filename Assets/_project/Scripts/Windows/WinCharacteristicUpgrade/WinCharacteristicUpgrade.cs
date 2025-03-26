using CodeBase.Configs;
using CodeBase.Core.Ui;
using CodeBase.Hero;
using CodeBase.Services.Localization;
using CodeBase.Services.PersistentProgress;
using CodeBase.Windows.Common;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Windows
{
    public class WinCharacteristicUpgrade : UiWindow<WinCharacteristicUpgrade.Data>
    {
        [SerializeField] private SkillPointsCounterView _skillPointsCounterView;
        [SerializeField] private Button _closeButton;
        [SerializeField] private CharacteristicsBlockView _characteristicsBlockView;
        
        public class Data : UiWindowData
        {
            public HeroModel HeroModel;
        }

        [Inject] private PlayerBaseCharacteristicsConfig _playerBaseCharacteristicsConfig;
        [Inject] private IPersistentProgressService _persistentProgressService;
        [Inject] private IInterfaceLocalizationService _localizationService;
        
        protected override void OnStateChanged()
        {
            base.OnStateChanged();
            if (State == UiPanelState.Opened)
            {
                _skillPointsCounterView.Init(data.HeroModel.SkillPointsModel);
                _characteristicsBlockView.Init(
                    data.HeroModel, 
                    _playerBaseCharacteristicsConfig, 
                    _persistentProgressService, 
                    _localizationService);
                _closeButton.onClick.RemoveAllListeners();
                _closeButton.onClick.AddListener(Close);
            }
            else
            {
                _characteristicsBlockView.Dispose();
            }
        }
    }
}