using System;
using System.Collections.Generic;
using Game.Gameplay.Services.Level;
using UniRx;
using Zenject;

namespace Game.Gameplay.UI
{
    public class LevelDataViewModel : ILevelDataViewModel, IInitializable, IDisposable
    {
        public IReadOnlyReactiveProperty<int> PlayerHealth => _playerHealth;

        private ILevelDataService _levelDataService;
        
        private ReactiveProperty<int> _playerHealth = new();
        private List<IDisposable> _disposables = new();
        
        [Inject]
        public void Construct(ILevelDataService levelService)
        {
            _levelDataService = levelService;
        }
        
        public void Initialize()
        {
            _levelDataService.PlayerHealth.Subscribe(x => _playerHealth.Value = x).AddTo(_disposables);
        }
        
        public void Dispose()
        {
            _disposables.ForEach(x => x.Dispose());
            _disposables.Clear();
        }
    }
}