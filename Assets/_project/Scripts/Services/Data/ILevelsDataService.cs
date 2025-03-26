using CodeBase.Configs;
using CodeBase.Services;

namespace CodeBase.Servises.Data
{
    public interface ILevelsDataService : IService
    {
        void LoadLevelsConfigs();
        LevelConfig GetNextSceneConfig(string levelId);
        string GetFirstDataId();
        string GetSceneName(string levelId);
    }
}