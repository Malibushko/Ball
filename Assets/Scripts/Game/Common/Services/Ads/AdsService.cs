using System;
using Cysharp.Threading.Tasks;
using Game.Configs;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Game.Common.Services.Ads
{
   public class AdsService : 
        IAdsService,
        IUnityAdsInitializationListener, 
        IUnityAdsLoadListener, 
        IUnityAdsShowListener
    {
        private ILoggingService _loggingService;
        private AdsConfig _config;
        private PlatformSpecificAdsConfig _currentAdsConfig;
        private bool _adLoaded;
        
        public Action<string> OnAdShowed {get; set; }
        private int _adsShowedCount;

        public AdsService(ILoggingService loggingService, AdsConfig config)
        {
            _loggingService = loggingService;
            _config = config;
            _currentAdsConfig = _config.GetPlatformConfig(GetPlatformString());
        }

        public UniTask InitializeAsync()
        {
            InitializeAds();
            LoadAd();
            return UniTask.CompletedTask;
        }
        
        public void OnInitializationComplete()
        {
            _loggingService.Log("Unity Ads initialization complete.");
            LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            _loggingService.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public bool CanShowAd()
        {
            return _adsShowedCount < _config.MaxAdsPerSession && _adLoaded;
        }
        
        public void LoadAd()
        {
            Debug.Log($"Loading Ad: {_currentAdsConfig.AdsUnit}");
            Advertisement.Load(_currentAdsConfig.AdsUnit, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            _adLoaded = true;
            _loggingService.Log($"Ad loaded: {adUnitId}");
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            _loggingService.LogError($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }
        
        private void InitializeAds()
        {
            _loggingService.Log("Initializing Unity Ads...");
            
            Advertisement.Initialize(_currentAdsConfig.GameId, _config.IsTestMode, this);
        }
        
        public bool ShowAd()
        {
            if (CanShowAd())
            {
                _loggingService.Log($"Showing Ad: {_currentAdsConfig.AdsUnit}");
                Advertisement.Show(_currentAdsConfig.AdsUnit, this);
                return true;
            }

            return false;
        }

        #region Ad Showing Callbacks

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            _loggingService.LogError($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            LoadAd();
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
            _loggingService.Log($"Ad started: {adUnitId}");
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
            _loggingService.Log($"Ad clicked: {adUnitId}");
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            _loggingService.Log($"Ad completed: {adUnitId} - {showCompletionState.ToString()}");

            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                _adsShowedCount++;
                OnAdShowed?.Invoke(adUnitId);
            }

            LoadAd();
        }

        private string GetPlatformString()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    return "android";
                case RuntimePlatform.IPhonePlayer:
                    return "ios";
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxEditor:
                    return "editor";
                default:
                    return "default";
            }
        }
        
        #endregion
    }
}