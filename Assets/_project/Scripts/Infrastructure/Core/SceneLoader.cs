using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CodeBase.Core
{
    public class SceneLoader
    {
        private const string BOOTSTRAP_SCENE_NAME = "BootstrapScene";
        private Scene _curMainScene;
        private Scene _curUiScene;

        public void Load(string name, Action onLoaded = null)
        {
            LoadScene(name, onLoaded).Forget();
        }

        public void LoadAdditiveUi(string name, Action onLoaded = null)
        {
            var loadedScene = SceneManager.GetSceneByName(name);
            if (loadedScene.isLoaded)
            {
                return;
            }

            LoadSceneAdditive(name, onLoaded).Forget();
        }

        private async UniTask LoadScene(string name, Action onLoaded = null)
        {
            var loadedScene = SceneManager.GetSceneByName(name);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                await SceneManager.UnloadSceneAsync(loadedScene);
                await SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).ToUniTask();
                _curMainScene = SceneManager.GetSceneByName(name);
                onLoaded?.Invoke();
                return;
            }

            await SceneManager.UnloadSceneAsync(_curMainScene.IsValid() ? _curMainScene.name : BOOTSTRAP_SCENE_NAME);
            await SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).ToUniTask();
            _curMainScene = SceneManager.GetSceneByName(name);
            onLoaded?.Invoke();
        }

        private async UniTask LoadSceneAdditive(string name, Action onLoaded = null)
        {
            await SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).ToUniTask();
            onLoaded?.Invoke();
        }
    }
}
