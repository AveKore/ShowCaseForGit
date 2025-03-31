using CodeBase.Core.Ui;
using CodeBase.Hero;
using CodeBase.Windows.Common;
using UnityEngine;
using UnityEngine.UI;

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
        
        protected override void OnStateChanged()
        {
            base.OnStateChanged();
            if (State == UiPanelState.Opened)
            {
                _skillPointsCounterView.Init(data.HeroModel.SkillPointsModel);
                _characteristicsBlockView.Init(data.HeroModel);
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