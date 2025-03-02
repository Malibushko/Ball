using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Loading
{
    public class TimedLoadingProgressProvider : ILoadingProgressProvider, ITickable
    {
        private ReactiveProperty<float> _progress = new();
        private ReactiveProperty<string> _displayStatus = new();
        
        public IReadOnlyReactiveProperty<float> NormalizedProgress => _progress;
        public IReadOnlyReactiveProperty<string> DisplayStatus => _displayStatus;

        private readonly float _showTime;
        private readonly float _statusChangeDelay;

        private float _startTime;
        
        public TimedLoadingProgressProvider(float showTime, float statusChangeDelay)
        {
            _showTime = showTime;
            _statusChangeDelay = statusChangeDelay;
        }

        public UniTask Start()
        {
            _progress.Value = 0;
            _displayStatus.Value = string.Empty;
            _startTime = Time.time;
            return UniTask.CompletedTask;
        }

        public async UniTask Finish()
        {
            var showingTime = Time.time - _startTime;
            var remainingTime = _showTime - showingTime;
            if (remainingTime > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(remainingTime));
        }

        public async UniTask SetDisplayStatus(string status)
        {
            if (_displayStatus.Value != status && _displayStatus.Value != string.Empty)
                await UniTask.Delay(TimeSpan.FromSeconds(_statusChangeDelay));
            _displayStatus.Value = status;
        }

        public void Tick()
        {
            float elapsedTime = Time.time - _startTime;
            float progress = Mathf.Clamp01(elapsedTime / _showTime);
            _progress.Value = progress;
        }
    }
}