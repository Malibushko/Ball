using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Configs;
using Game.Common.Services.Physics;
using Game.GameHub.Configs;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.GameHub.Player
{
    public class PlayerRandomMover : IInitializable, IDisposable, IPlayerMover
    {
        private PlayerMoveConfig _playerMoveConfig;
        
        private IPhysicsObject _physicsObject;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        public void Construct(IPhysicsObject physicsObject, IConfigsService configs, PlayerMoveConfig playerMoveConfig)
        {
            _physicsObject = physicsObject;
            _playerMoveConfig = playerMoveConfig;
        }

        public void Initialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            StartRandomMovementLoop().Forget();
        }
        
        public void Dispose()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }
        
        private async UniTaskVoid StartRandomMovementLoop()
        {
            if (_playerMoveConfig == null)
                return;

            try
            {
                float delaySeconds = _playerMoveConfig.MoveFrequency;
        
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    ApplyRandomForce(_playerMoveConfig.MinSpeed, _playerMoveConfig.MaxSpeed);
                    await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken: _cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
        
        private void ApplyRandomForce(float min, float max)
        {
            float randomX = Random.Range(-1, 1);
            float randomZ = Random.Range(-1, 1);

            Vector3 randomDirection = new Vector3(randomX, 0f, randomZ);
            randomDirection.Normalize();
            
            float randomMagnitude = Random.Range(min, max);
    
            _physicsObject.ApplyForce(randomDirection * randomMagnitude);
        }
    }
}