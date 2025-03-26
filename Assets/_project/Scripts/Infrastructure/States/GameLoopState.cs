﻿using CodeBase.Core.Ui;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Windows;
using UnityEngine;

namespace CodeBase.States
{
    public class GameLoopState : IPayLoadedState<LevelSnapshot>
    {
        private readonly GameStateMachine _statemachine;
        private readonly UiWindows _uiWindows;
        private readonly ISaveLoadService _saveLoadService;

        private LevelSnapshot _levelSnapshot;
        private readonly IPersistentProgressService _persistentProgressService;

        public GameLoopState(
            GameStateMachine statemachine,
            UiWindows uiWindows,
            ISaveLoadService saveLoadService,
            IPersistentProgressService persistentProgressService)
        {
            _statemachine = statemachine;
            _saveLoadService = saveLoadService;
            _uiWindows = uiWindows;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(LevelSnapshot levelSnapshot)
        {
            _levelSnapshot = levelSnapshot;

            _levelSnapshot.LevelFinishedAction += OnLevelFinished;
        }

        private void OnLevelFinished()
        {
            var data = new WinGameResults.Data
            {
                IsLast = _levelSnapshot.IsLast,
                LevelSnapshot = _levelSnapshot,
                LevelState = _levelSnapshot.LevelState,
                KilledCount = _levelSnapshot.GetKilledCount(),
                OnRestart = () => _statemachine.Enter<LoadLevelState, string>(_levelSnapshot.LevelId),
                OnContinue = () => _statemachine.Enter<LoadLevelState, string>(_levelSnapshot.NextLevelId),
                OnExit = Application.Quit,
            };
            _uiWindows.Open<WinGameResults>(data);
#if !UNITY_STANDALONE && !UNITY_EDITOR
            _uiWindows.GetWindow<WinMobileInput>()?.Close(); //TODO: when standalone UI scene will appear, remove it
#endif
        }

        public void Exit()
        {
            _saveLoadService.Save(_persistentProgressService.PlayerProgress);
            _levelSnapshot.LevelFinishedAction -= OnLevelFinished;
        }
    }
}