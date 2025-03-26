using CodeBase.Core;

namespace CodeBase.States
{
    public class BootstrapState : IState
    {
        private const string BOOTSTRAP_SCENE_NAME = "BootstrapScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.Load(BOOTSTRAP_SCENE_NAME, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }
    }
}