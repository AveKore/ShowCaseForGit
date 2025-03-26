using CodeBase.Core.Ui;
using CodeBase.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Inject] private GameStateMachine StateMachine { get; }
        [Inject] private UiWindows UIWindows { get; }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            UIWindows.Initialize(StartBootstrapCallback);
        }

        private void StartBootstrapCallback()
        {
            StateMachine.Enter<BootstrapState>();
        }
    }
}