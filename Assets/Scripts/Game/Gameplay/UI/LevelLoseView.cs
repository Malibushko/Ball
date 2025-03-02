using Game.Common.Services.Ads;
using Game.Gameplay.Services.Level;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Gameplay.UI
{
    public class LevelLoseView : LevelMenuView
    {
        [SerializeField] private string _adsButtonName;
    
        private IAdsWatchListener _adsWatchListener;
        private IAdsService _adsService;
        private Button _adsButton;
        
        [Inject]
        public void Construct(IAdsService adsService, IAdsWatchListener adsWatchListener)
        {
            _adsService = adsService;    
            _adsWatchListener = adsWatchListener;
        }

        private new void Awake()
        {
            base.Awake();
            
            var root = _uiDocument.rootVisualElement;
            
            _adsButton = root.Q<Button>(_adsButtonName);
            _adsButton.clicked += OnAdsButtonClicked;
        }
        
        public void Start()
        {
            if (!_adsService.CanShowAd())
                _adsButton.visible = false;
        }
        
        private void OnAdsButtonClicked()
        {
            _adsService.OnAdShowed += OnAdShowed;
            if (!_adsService.ShowAd())
                _adsButton.visible = false;
        }

        private void OnAdShowed(string placementId)
        {
            _adsWatchListener.OnAdShowed();
            _adsButton.visible = false;
        }
    }
}