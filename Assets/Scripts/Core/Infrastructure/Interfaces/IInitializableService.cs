using Cysharp.Threading.Tasks;

namespace Core.Infrastructure.Interfaces
{
    public interface IInitializableAsyncService
    {
        public UniTask InitializeAsync();
    }
}