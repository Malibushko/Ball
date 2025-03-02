using Core.Infrastructure.Interfaces;

namespace Game.Common.Services.Text
{
    public interface IGameStringsService : IInitializableAsyncService
    {
        public string GetString(string key);
    }
}