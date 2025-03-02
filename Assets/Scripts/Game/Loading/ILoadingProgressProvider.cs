using Core.Infrastructure.Interfaces;
using Cysharp.Threading.Tasks;

namespace Game.Loading
{
    public interface ILoadingProgressProvider : IProgressProvider
    {
        public UniTask Start();
        
        public UniTask Finish();
        
        public UniTask SetDisplayStatus(string status);
    }
}