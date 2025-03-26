using System;
using System.Collections.Generic;
using CodeBase.Enemy;
using CodeBase.Hero;
using UniRx;

namespace CodeBase.States
{
    public class LevelSnapshot : IDisposable
    {
        private readonly string _levelId;
        private readonly string _nextLevelId;
        private HeroEntityView _player;
        private HeroModel _playerModel;
        public HeroEntityView PlayerGO => _player;
        public HeroModel PlayerModel => _playerModel;
        public string LevelId => _levelId;
        public string NextLevelId => _nextLevelId;

        public LevelState LevelState { get; private set; }
        public bool IsLast { get; private set; }

        private readonly Dictionary<string, EnemyEntityView> _enemiesObjects = new();
        private readonly Dictionary<string, EnemyModel> _enemiesModels = new();
        private readonly List<IDisposable> _disposables = new();

        private int _killedCount;


        public event Action LevelFinishedAction;

        public LevelSnapshot(string levelId, string nextSceneName, string nextLevelId)
        {
            _levelId = levelId;
            _nextLevelId = nextLevelId;
            IsLast = string.IsNullOrEmpty(nextSceneName);
        }

        public void AddNewHeroData(HeroEntityView player, HeroModel playerModel)
        {
            _player = player;
            _playerModel = playerModel;
            _playerModel.HealthModel.IsDead.Subscribe(RemovePlayerData).AddTo(_disposables);
        }

        public void AddNewEnemyData(string enemyId, EnemyEntityView enemyObject, EnemyModel enemyModel)
        {
            enemyModel.HealthModel.IsDead.Subscribe((isDead) => RemoveEnemyData(enemyModel.EnemyId, isDead))
                .AddTo(_disposables);
            _enemiesObjects.Add(enemyId, enemyObject);
            _enemiesModels.Add(enemyId, enemyModel);
        }

        public int GetKilledCount()
        {
            return _killedCount;
        }

        private void RemoveEnemyData(string enemyId, bool isDead)
        {
            if (isDead == false)
            {
                return;
            }

            if (_enemiesObjects.ContainsKey(enemyId))
            {
                _enemiesObjects.Remove(enemyId);
            }

            if (_enemiesModels.ContainsKey(enemyId))
            {
                _enemiesModels.Remove(enemyId);
            }

            _killedCount++;
            if (_enemiesModels.Count == 0)
            {
                LevelState = LevelState.Win;
                LevelFinishedAction?.Invoke();
                _player.StopHero();
            }
        }

        private void RemovePlayerData(bool isDead)
        {
            if (isDead == false && _player != null)
            {
                return;
            }

            _player = null;
            LevelState = LevelState.Lose;
            LevelFinishedAction?.Invoke();
        }

        private EnemyEntityView GetEnemyObjectById(string enemyId) =>
            _enemiesObjects.GetValueOrDefault(enemyId);

        private EnemyModel GetEnemyEntityById(string enemyId) =>
            _enemiesModels.GetValueOrDefault(enemyId);

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