using System;
using System.Collections.Generic;
using CodeBase.Core;
using CodeBase.Core.Factory;
using CodeBase.Core.Ui;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using CodeBase.Servises.Data;
using Zenject;

namespace CodeBase.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        private void Construct(SceneLoader sceneLoader,
            PrefabFactory prefabFactory,
            UiWindows uiWindows,
            IPersistentProgressService progressService,
            IEnemyDataService enemyDataService,
            ISaveLoadService saveLoadService,
            ILevelsDataService levelsDataService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                { typeof(BootstrapState), new BootstrapState(this, sceneLoader) },
                {
                    typeof(LoadProgressState),
                    new LoadProgressState(this, progressService, saveLoadService, levelsDataService)
                },
                {
                    typeof(LoadLevelState),
                    new LoadLevelState(this, sceneLoader, uiWindows, prefabFactory, progressService, enemyDataService,
                        levelsDataService)
                },
                { typeof(GameLoopState), new GameLoopState(this, uiWindows, saveLoadService, progressService) },
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            IState state = ChangeState<TState>();
            _activeState = state;
            state.Enter();
        }


        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}
