using System;
using CodeBase.Core.Ui;
using CodeBase.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Windows
{
    public class WinGameResults : UiWindow<WinGameResults.Data>
    {
        public class Data : UiWindowData
        {
            public LevelSnapshot LevelSnapshot;
            public bool IsLast;
            public LevelState LevelState;
            public int KilledCount;
            public Action OnRestart;
            public Action OnContinue;
            public Action OnExit;
        }

        [SerializeField] private TMP_Text _killedCountText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _characteristicUpgradeButton;

        [Inject] private UiWindows _uiWindows;

        protected override void OnStateChanged()
        {
            base.OnStateChanged();

            if (State == UiPanelState.Opened)
            {
                CloseCharacteristicWindow();
                Init();
            }
        }

        private void Init()
        {
            _killedCountText.text = data.KilledCount.ToString();
            _restartButton.gameObject.SetActive(data.LevelState == LevelState.Lose);
            _restartButton.onClick.AddListener(Restart);
            _continueButton.gameObject.SetActive(data.LevelState == LevelState.Win && data.IsLast == false);
            _continueButton.onClick.AddListener(Continue);
            _exitButton.gameObject.SetActive(true);
            _exitButton.onClick.AddListener(Exit);
            _characteristicUpgradeButton.onClick.RemoveAllListeners();
            _characteristicUpgradeButton.onClick.AddListener(OpenSkillsUpgradeWindow);
        }

        private void OpenSkillsUpgradeWindow()
        {
            _uiWindows.Open<WinCharacteristicUpgrade>(new WinCharacteristicUpgrade.Data
            {
                HeroModel = data.LevelSnapshot.PlayerModel,
            });
        }

        private void CloseCharacteristicWindow()
        {
            var window = _uiWindows.GetWindow<WinCharacteristicUpgrade>();
            if (window.IsOpened)
            {
                window.Close();
            }
        }

        private void Continue()
        {
            Close();
            data.OnContinue?.Invoke();
        }

        private void Restart()
        {
            Close();
            data.OnRestart?.Invoke();
        }

        private void Exit()
        {
            Close();
            data.OnExit?.Invoke();
        }
    }
}