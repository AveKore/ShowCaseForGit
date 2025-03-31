using System.Collections.Generic;
using CodeBase.Configs;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void Save<T>(T data);
        T Load<T>();
        public Dictionary<CharacteristicType, CharacterStat> LoadStatsFromExcel();
    }
}