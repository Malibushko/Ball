using Core.Infrastructure.Interfaces;
using Cysharp.Threading.Tasks;

namespace Game.Common.Services.Loading
{
    public interface ILoadingScreen : IInitializableAsyncService
    {
        public UniTask Show();
        
        public UniTask Hide();
        
        public void SetDisplayStatus(string status);
    }
}