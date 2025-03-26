namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void Save<T>(T data);
        T Load<T>();
    }
}