using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Core.Ui
{
    public class UiWindows
    {
        private const string UI_SCENE_NAME = "GameUIMobile";

        private UiSceneWindowsProvider _currentSceneWindowsProvider;
        [Inject] private SceneLoader SceneLoader { get; }
        [Inject] private DiContainer DiContainer { get; }

        public void Initialize(Action onInitialized)
        {
            LoadUiWindow(onInitialized);
        }

        private void LoadUiWindow(Action onInitialized)
        {
            SceneLoader.LoadAdditiveUi(UI_SCENE_NAME, () => GetAllWindows(onInitialized));
        }

        private void GetAllWindows(Action onInitialized)
        {
            var scene = SceneManager.GetSceneByName(UI_SCENE_NAME);
            foreach (var go in scene.GetRootGameObjects())
            {
                _currentSceneWindowsProvider = go.GetComponentInChildren<UiSceneWindowsProvider>();
                if (_currentSceneWindowsProvider != null)
                {
                    _currentSceneWindowsProvider.InjectWindows(DiContainer);
                    onInitialized?.Invoke();
                    break;
                }
            }

            if (_currentSceneWindowsProvider == null)
            {
                Debug.LogError("UiSceneWindows not found");
            }
        }

        public T Open<T>() where T : UiWindow
        {
            return Open<T>(null, new UiWindow.UiWindowData());
        }

        public T Open<T>(UiWindow.UiWindowData winData) where T : UiWindow
        {
            return Open<T>(null, winData);
        }


        public T Open<T>(UiWindow parent) where T : UiWindow
        {
            return Open<T>(parent, new UiWindow.UiWindowData());
        }

        public T Open<T>(UiWindow parent, UiWindow.UiWindowData winData) where T : UiWindow
        {
            var w = GetWindow<T>();
            if (w == null)
            {
                return w;
            }

            if (parent)
            {
                parent.AddChild(w);
            }

            w.Init(winData);
            return w;
        }

        public T GetWindow<T>() where T : UiWindow
        {
            T wnd = null;

            if (_currentSceneWindowsProvider != null)
            {
                wnd = _currentSceneWindowsProvider.GetWindow<T>();

            }

            return wnd;
        }
    }
}