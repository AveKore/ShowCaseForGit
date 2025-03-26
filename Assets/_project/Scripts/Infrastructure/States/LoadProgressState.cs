using CodeBase.Configs;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Servises.Data;

namespace CodeBase.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly PlayerBaseCharacteristicsConfig _playerConfig;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ILevelsDataService _levelsDataService;

        public LoadProgressState(GameStateMachine stateMachine,
            PlayerBaseCharacteristicsConfig playerConfig,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService,
            ILevelsDataService levelsDataService)
        {
            _stateMachine = stateMachine;
            _playerConfig = playerConfig;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _levelsDataService = levelsDataService;
        }

        public void Enter()
        {
            LoadProgressOrCreateNew();
            _stateMachine.Enter<LoadLevelState, string>(_levelsDataService.GetFirstDataId());
        }

        public void Exit()
        {
        }

        private void LoadProgressOrCreateNew()
        {
            _progressService.PlayerProgress =
                _saveLoadService.Load<PlayerProgressData>() ?? CreateNewPlayerProgressFromConfig();
        }

        private PlayerProgressData CreateNewPlayerProgressFromConfig() =>
            _progressService.CreatePlayerProgress(_playerConfig);
    }
}