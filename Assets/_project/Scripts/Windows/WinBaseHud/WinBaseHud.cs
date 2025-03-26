using System;
using System.Collections.Generic;
using CodeBase.Core.Ui;
using CodeBase.Hero;
using CodeBase.States;
using CodeBase.Windows.Common;
using UniRx;
using UnityEngine;

namespace CodeBase.Windows
{
    public class WinBaseHud: UiWindow<WinBaseHud.Data> , IDisposable
    {
        public class Data : UiWindowData
        {
            public LevelSnapshot levelSnapshot;
        }
        
        [SerializeField] private HealthBarView _healthBarView;
        [SerializeField] private SkillPointsCounterView _skillPointsCounterView;
        
        private readonly List<IDisposable> _disposables = new();
        private HeroModel _heroModel;

        public void Reload(LevelSnapshot levelSnapshot)
        {
            Dispose();
            data.levelSnapshot = levelSnapshot;
            InitWindow();
        }

        private void InitWindow()
        {
            _skillPointsCounterView.Init(data.levelSnapshot.PlayerModel.SkillPointsModel);
            data.levelSnapshot.PlayerModel.HealthModel.Health.Subscribe(UpdateHealth).AddTo(_disposables);
            data.levelSnapshot.PlayerModel.HealthModel.MaxHealth.Subscribe(UpdateMaxHealth).AddTo(_disposables);
        }

        protected override void OnStateChanged()
        {
            base.OnStateChanged();
            if (State == UiPanelState.Opened)
            {
                InitWindow();
            }
            else
            {
                Dispose();
            }
        }
        
        private void UpdateHealth(int health)
        {
            _healthBarView.UpdateHealth(health);
        }

        private void UpdateMaxHealth(int maxHealth)
        {
            _healthBarView.UpdateHealth(data.levelSnapshot.PlayerModel.HealthModel.Health.Value, maxHealth);
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }
    }
}