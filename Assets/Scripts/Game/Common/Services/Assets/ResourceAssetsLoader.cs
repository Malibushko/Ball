using System.Collections.Concurrent;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Common.Services.Assets
{
    public class ResourceAssetsLoader : IAssetsService
    {
        private ConcurrentDictionary<string, Object> _assetsCache = new();
        
        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public void Preload<TType>(string assetName) where TType : Object
        {
            _assetsCache.TryAdd(assetName, Load<TType>(assetName));
        }

        public UniTask PreloadAsync<TType>(string assetName) where TType : Object
        {
            return LoadAsync<TType>(assetName)
                .ContinueWith(o => _assetsCache.TryAdd(assetName, o));
        }

        public TType Load<TType>(string assetName) where TType : Object
        {
            if (_assetsCache.TryGetValue(assetName, out var asset))
                return asset as TType;
            
            return Resources.Load(assetName) as TType;
        }

        public UniTask<TType> LoadAsync<TType>(string assetName) where TType : Object
        {
            if (_assetsCache.TryGetValue(assetName, out var asset))
                return new UniTask<TType>(asset as TType);
            
            return Resources.LoadAsync(assetName, typeof(TType)).ToUniTask().ContinueWith(o => o as TType);
        }
    }
}