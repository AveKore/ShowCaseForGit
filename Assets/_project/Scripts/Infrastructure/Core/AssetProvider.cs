using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Core
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject LoadAsset(string path) => 
            LoadAsset<GameObject>(path);
        
        public T LoadAsset<T>(string path) where T : Object => 
            Resources.Load<T>(path);

        public List<T> LoadAllAssets<T>(string path) where T : Object => 
            Resources.LoadAll<T>(path).ToList();
    }

    public interface IAssetProvider
    {
        public GameObject LoadAsset(string path);
        public T LoadAsset<T>(string path) where T : Object;
        public List<T> LoadAllAssets<T>(string path) where T : Object;
    }
}