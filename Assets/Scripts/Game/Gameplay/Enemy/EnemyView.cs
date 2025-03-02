using System;
using Core.Infrastructure.Interfaces;
using Game.Common.Services.Configs;
using Game.Gameplay.Enemy.Common;
using Game.Gameplay.Health;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Enemy
{
    [RequireComponent(typeof(Collider))]
    public class EnemyView : MonoBehaviour, IConfigLoadable
    {
        [SerializeField] private LayerMask _hitLayerMask;
        
        private IConfigsService _configService;
        private EnemyConfig _config;
        private EnemyState _state;
        private IEnemyController _controller;
        private IEnemyHitHandler _hitHandler;
        
        private IDisposable _enemyStateDisposable;
        
        [Inject]
        public void Construct(
            IConfigsService configService, 
            EnemyState state, 
            IEnemyController controller, 
            IEnemyHitHandler handler)
        {
            _configService = configService;
            _state = state;
            _controller = controller;
            _hitHandler = handler;
            
            _enemyStateDisposable = _state.CurrentState.Subscribe(OnEnemyStateChange);
        }

        public void LoadFromConfig(object config)
        {
            _config = _configService.Load<EnemyConfig>(config);
            
            _controller.LoadFromConfig(_config.Controller);
            _hitHandler.LoadFromConfig(_config.HitHandler);
            
            _controller.SetTarget(transform);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var otherObject = collision.gameObject;

            if (Math.Math.HasLayer(_hitLayerMask, otherObject.layer))
            {
                if (_hitHandler.WasHit(transform, collision))
                {
                    _state.CurrentState.Value = EnemyState.State.Dying;
                }
                else  {
                    if (collision.gameObject.TryGetComponent(out HealthComponent health))
                        health.TakeDamage(_config.Damage);
                }
            }
        }

        private void OnEnable()
        {
            _state.CurrentState.Value = EnemyState.State.Active;
            _controller.Activate();
        }

        private void OnDisable()
        {
            _controller.Deactivate();
            _enemyStateDisposable.Dispose();
        }

        private void OnEnemyStateChange(EnemyState.State state)
        {
            if (state == EnemyState.State.Dying)
                _controller.Deactivate();
            
            if (state == EnemyState.State.Dead)
                Destroy(gameObject);
        }
    }
}