using Core.Infrastructure.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Common.Services.Assets
{
    public interface IAssetsService : IInitializableAsyncService
    {
        public void Preload<TType>(string assetName) where TType : Object;
        public UniTask PreloadAsync<TType>(string assetName) where TType : Object;

        public TType Load<TType>(string assetName) where TType : Object;
        public UniTask<TType> LoadAsync<TType>(string assetName) where TType : Object;
    }
}