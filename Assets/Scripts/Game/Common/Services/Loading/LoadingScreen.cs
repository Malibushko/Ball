using Cysharp.Threading.Tasks;
using Game.Common.Services.Scenes;
using Game.Common.Services.UI;
using Game.Loading;
using UnityEngine;

namespace Game.Common.Services.Loading
{
    public class LoadingScreen : ILoadingScreen
    {
        private readonly UIFactory _uiFactory;
        private readonly IPrefabInstantiator _assetsPrefabInstantiator;
        
        private GameObject _curtainAsset;
        private ILoadingProgressProvider _progressProvider;
        
        public LoadingScreen(UIFactory uiFactory, IPrefabInstantiator assetsAssetsPrefabInstantiator, ILoadingProgressProvider progressProvider)
        {
            _uiFactory = uiFactory;
            _assetsPrefabInstantiator = assetsAssetsPrefabInstantiator;
            _progressProvider = progressProvider;
        }

        public async UniTask Show()
        {
            if (!_curtainAsset.activeSelf)
                _curtainAsset.SetActive(true);
            
            await _progressProvider.Start();
        }
        
        public async UniTask Hide()
        {
            await _progressProvider.Finish();
            
            if (_curtainAsset.activeSelf)
                _curtainAsset.SetActive(false);
        }

        public void SetDisplayStatus(string status)
        {
            _progressProvider.SetDisplayStatus(status);   
        }
        
        public UniTask InitializeAsync()
        {
            _curtainAsset = _assetsPrefabInstantiator.Instantiate(_uiFactory.CreateLoadingScreen());
            _curtainAsset.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}