using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Analytics;
using Game.Gameplay.Configs.Level;
using Game.Gameplay.States;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Game.Gameplay.Services.Level
{
    public class LevelService : ILevelService, IAdsWatchListener, IInitializable
    {
        const string LevelStartEventId  = "level_start";
        const string LevelStopEventId  = "level_end";
        const string LevelPauseEventId  = "level_pause";
        
        private LevelStateMachine _stateMachine;
        private LevelConfig _levelConfig;
        private GameObject _levelGameObject;
        private IAnalyticsService _analyticsService;
        private ILevelDataService _levelDataService;
        
        private List<IDisposable> _disposables = new();
        
        [Inject]
        public void Construct(
            LevelConfig config, 
            LevelStateMachine stateMachine,
            IAnalyticsService analyticsService,
            ILevelDataService levelDataService)
        {
            _levelConfig = config;
            _stateMachine = stateMachine;
            _analyticsService = analyticsService;
            _levelDataService = levelDataService;
        }

        public void Configure(GameObject level)
        {
            _levelGameObject = level;
        }

        public void Start()
        {
            _analyticsService.LogEvent(LevelStartEventId);
            
            _levelDataService.PlayerHealth
                .Subscribe(OnPlayerHealthChanged)
                .AddTo(_disposables);
            _levelDataService.Resources
                .ObserveReplace()
                .Subscribe(replaceEvent => OnResourceCollected(replaceEvent.Key, replaceEvent.NewValue))
                .AddTo(_disposables);
            
            Pause(false);
        }

        public void Finish(LevelResult result)
        {
            switch (result)
            {
                case LevelResult.Win:
                    _stateMachine.GotoState<LevelWinState>().Forget();
                    break;
                case LevelResult.Lose:
                    _stateMachine.GotoState<LevelLoseState>().Forget();
                    break;
            }
            
            _analyticsService.LogEvent(LevelStopEventId);
            Pause(true);
        }

        public void Pause(bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
            
            _analyticsService.LogEvent(LevelPauseEventId);
        }
        
        public void Initialize()
        {
            Pause(true);
        }
        
        public TComponent FindComponentOnLevel<TComponent>(bool includeInactive = false) where TComponent : Object
        {
            if (_levelGameObject == null)
                return null;
            
            return _levelGameObject.GetComponentInChildren<TComponent>(includeInactive);
        }

        public void OnPlayerHealthChanged(int playerHealth)
        {
            if (playerHealth <= 0)
                OnPlayerDied();
        }
        
        public void OnResourceCollected(Resource.Resource resource, int amount)
        {
            if (resource.ResourceName == _levelConfig.Collectables.MainCollectableResourceId)
            {
                if (amount == _levelConfig.Collectables.TargetCollectAmount)
                    OnTargetResourcesCollected();
            }
        }
        
        public void OnAdsShowed()
        {
            _levelDataService.PlayerHealth.Value += _levelConfig.LevelAds.HealthReward;
            
            _stateMachine.GotoState<LevelPlayState>().Forget();
        }

        private void OnPlayerDied()
        {
            Finish(LevelResult.Lose);
        }

        private void OnTargetResourcesCollected()
        {
            _stateMachine.GotoState<LevelCompletedState>().Forget();
        }

        public void OnAdShowed()
        {
            _levelDataService.PlayerHealth.Value += _levelConfig.LevelAds.HealthReward;
            
            _stateMachine.GotoState<LevelPlayState>().Forget();
        }
    }
}