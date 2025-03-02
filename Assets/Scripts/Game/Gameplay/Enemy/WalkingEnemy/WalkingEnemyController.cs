using Game.Common.Services.Configs;
using Game.Gameplay.Enemy.Common;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Enemy.WalkingEnemy
{
    public class WalkingEnemyController : ITickable, IEnemyController
    {
        private enum Direction
        { 
            Forward = 1,
            Backward = -1
        }

        private Direction _direction;
        private float _movedDistance;

        private Transform _targetTransform;
        private IConfigsService _configsService;
        private WalkingEnemyControllerConfig _config;
        private bool _isEnabled;
        
        public WalkingEnemyController(IConfigsService configsService)
        {
            _configsService = configsService;    
            _direction = Direction.Forward;
        }
        
        private void FlipDirection()
        {
            _direction = (_direction == Direction.Forward) ? Direction.Backward : Direction.Forward;
            _targetTransform.rotation = Quaternion.LookRotation(-_targetTransform.forward);
        }
        
        public void Tick()
        {
            if (_config != null && _isEnabled)
                UpdatePosition();
        }

        private void UpdatePosition()
        {
            float moveStep = _config.Speed * Time.deltaTime;
            Vector3 movement = _targetTransform.forward * moveStep;

            _targetTransform.position += movement;
            _movedDistance += Mathf.Abs(moveStep);

            if (_movedDistance >= _config.Distance)
            {
                FlipDirection();
                _movedDistance = 0f;
            }
        }

        public void SetTarget(Transform transform)
        {
            _targetTransform = transform;
            _movedDistance = 0f;
        }

        public void LoadFromConfig(object config)
        {
            _config = _configsService.Load<WalkingEnemyControllerConfig>(config);
        }

        public void Activate()
        {
            _isEnabled = true;
        }

        public void Deactivate()
        {
            _isEnabled = false;
        }
    }
}