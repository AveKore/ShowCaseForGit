using System;
using System.Collections.Generic;
using CodeBase.CameraLogic;
using CodeBase.Core;
using CodeBase.Core.Factory;
using CodeBase.Core.Ui;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Services.PersistentProgress;
using CodeBase.Servises.Data;
using CodeBase.Windows;
using UnityEngine;
using Camera = UnityEngine.Camera;
using Zenject;

namespace CodeBase.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string ENEMY_SPAWNER_POINT_TAG = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly DiContainer _diContainer;
        private readonly UiWindows _uiWindows;
        private WinLoadingCurtain _curtain;
        private readonly IPrefabFactory _prefabFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IEnemyDataService _enemyDataService;

        private LevelSnapshot _levelSnapshot;
        private readonly ILevelsDataService _levelsDataService;

        public LoadLevelState(GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            UiWindows uiWindows,
            IPrefabFactory prefabFactory,
            IPersistentProgressService persistentProgressService,
            IEnemyDataService enemyDataService,
            ILevelsDataService levelsDataService
        )
        {
            _enemyDataService = enemyDataService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiWindows = uiWindows;
            _prefabFactory = prefabFactory;
            _levelsDataService = levelsDataService;
            _persistentProgressService = persistentProgressService;
        }

        public void Enter(string levelId)
        {
            _curtain = _uiWindows.Open<WinLoadingCurtain>();
            var levelName = _levelsDataService.GetSceneName(levelId);
            var nextLevelConfig = _levelsDataService.GetNextSceneConfig(levelId);
            _levelSnapshot = new LevelSnapshot(levelId, nextLevelConfig?.Name, nextLevelConfig?.LevelId);
            _sceneLoader.Load(levelName, ShowcaseWaitingCallback);
        }

        private void ShowcaseWaitingCallback()
        {
            InitPlayer();
            InitEnemies();
            var winHud = _uiWindows.GetWindow<WinBaseHud>();
            if (winHud.IsOpened)
            {
                winHud.Reload(_levelSnapshot);
            }
            else
            {
                _uiWindows.Open<WinBaseHud>(new WinBaseHud.Data() { levelSnapshot = _levelSnapshot });
            }

            _stateMachine.Enter<GameLoopState, LevelSnapshot>(_levelSnapshot);
        }

        private void InitPlayer()
        {
            var player = _prefabFactory.LoadPlayer();
            var heroModel = new HeroModel(_persistentProgressService.PlayerProgress);
            player.Init(heroModel);
            _levelSnapshot.AddNewHeroData(player, heroModel);
            CameraFollow(player.gameObject);
        }

        private void InitEnemies()
        {
            List<EnemyEntityView> enemies = new List<EnemyEntityView>();
            var enemiesSpawners = GameObject.FindGameObjectsWithTag(ENEMY_SPAWNER_POINT_TAG);

            foreach (var enemySpawner in enemiesSpawners)
            {
                var enemyType = enemySpawner.GetComponent<EnemySpawner>().EnemyType;
                var enemyConfig = _enemyDataService.GetMonsterConfig(enemyType);
                if (!enemyConfig)
                {
                    continue;
                }

                var newEnemyId = Guid.NewGuid().ToString();
                var enemy = _prefabFactory.CreateEnemy(enemyConfig, enemySpawner.transform);
                var enemyModel = new EnemyModel(enemyConfig, newEnemyId);
                enemy.Init(enemyModel, _levelSnapshot.PlayerGO.transform,
                    _levelSnapshot.PlayerGO.HeroModel.HealthModel);
                _levelSnapshot.AddNewEnemyData(newEnemyId, enemy, enemyModel);
            }
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main?.GetComponent<CameraFollower>().Follow(hero);
        }

        public void Exit()
        {
            _curtain.Close();
        }
    }
}