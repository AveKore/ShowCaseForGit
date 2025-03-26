using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        public void Save<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString(typeof(T).Name, json);
        }

        public T Load<T>()
        {
            if (!PlayerPrefs.HasKey(typeof(T).Name))
            {
                return default;
            }

            var json = PlayerPrefs.GetString(typeof(T).Name);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}