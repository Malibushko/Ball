namespace Core.Infrastructure.Interfaces
{
    public interface IConfigLoadable
    {
        public void LoadFromConfig(object config);
    }
}