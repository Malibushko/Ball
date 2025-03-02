namespace Game.Common.Services.Configs
{
    public interface IConfigsService
    {
        public T Load<T>(object config) where T : class;
        public T Load<T>(string path) where T : class;
    }
}