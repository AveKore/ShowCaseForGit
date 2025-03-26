using UnityEngine;
using Zenject;

namespace CodeBase.Core.Factory
{
    public class BaseFactory
    {
        [Inject] private DiContainer _diContainer { get; }
        [Inject] protected IAssetProvider assetProvider { get; }

        protected T InstantiateWithInjection<T>(string path, Vector3 at, Transform parent) where T : MonoBehaviour
        {
            var prefab = assetProvider.LoadAsset<T>(path);
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, parent).GetComponent<T>();
        }

        protected T InstantiateWithInjection<T>(GameObject prefab, Vector3 at, Transform parent) where T : MonoBehaviour
        {
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, parent).GetComponent<T>();
        }

        protected GameObject InstantiateWithInjection(string path, Vector3 at, Transform parent)
        {
            var prefab = assetProvider.LoadAsset(path);
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, parent);
        }

        protected GameObject InstantiateWithInjection(GameObject prefab, Vector3 at, Transform parent)
        {
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, parent);
        }

        protected GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = assetProvider.LoadAsset(path);
            return _diContainer.InstantiatePrefab(prefab, at, Quaternion.identity, null);
        }
    }
}