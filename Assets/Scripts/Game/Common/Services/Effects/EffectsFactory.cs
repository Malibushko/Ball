using System;
using System.Collections.Generic;
using System.Linq;
using Core.Infrastructure.Pool;
using Game.GameConfigs.Memory;
using UnityEngine;
using Zenject;

namespace Game.Common.Services.Effects
{
    public class EffectsFactory : IEffectsFactory, IDisposable
    {
        private readonly DiContainer _container;
        private readonly Dictionary<GameObject, ObjectsPool> _pools = new();
    
        private readonly EffectsMemoryConfig _config;
        
        public EffectsFactory(
            DiContainer container,
            EffectsMemoryConfig config)
        {
            _container = container;
            _config = config;
        }
    
        public void Dispose()
        {
            var pools = _pools.Values.ToList();
            
            foreach (var pool in pools)
                pool.Clear();
            
            _pools.Clear();
        }
    
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (!_pools.TryGetValue(prefab, out var pool))
            {
                pool = new ObjectsPool(prefab, _container, _config.DefaultPoolSize);
                _pools[prefab] = pool;
            }
        
            return pool.Get(position, rotation, parent);
        }
    }
}