using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Infrastructure.Pool
{
public class ObjectsPool
    {
        private readonly Stack<GameObject> _inactive = new();
        private readonly GameObject _prefab;
        private readonly Transform _poolParent;
        private readonly DiContainer _container;
        private readonly HashSet<GameObject> _active = new();
        private bool _isDisposed;
        
        public ObjectsPool(GameObject prefab, DiContainer container, int initialSize = 0)
        {
            _prefab = prefab;
            _container = container;
            
            _poolParent = new GameObject($"Pool_{prefab.name}").transform;
            Object.DontDestroyOnLoad(_poolParent.gameObject);
            
            for (int i = 0; i < initialSize; i++)
            {
                Return(CreateInstance());
            }
        }
        
        private GameObject CreateInstance()
        {
            GameObject instance = _container.InstantiatePrefab(_prefab, _poolParent);
            
            var poolable = instance.GetComponent<PoolableObject>();
            if (poolable == null)
            {
                poolable = instance.AddComponent<PoolableObject>();
            }
            poolable.Pool = this;
            
            instance.SetActive(false);
            return instance;
        }
        
        public void Return(GameObject instance)
        {
            if (instance == null || _isDisposed)
                return;
                
            instance.transform.SetParent(_poolParent);
            instance.SetActive(false);
            
            _active.Remove(instance);
            _inactive.Push(instance);
        }

        public GameObject Get(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (_isDisposed)
                return null;
                
            GameObject instance;
            
            if (_inactive.Count > 0 && false)
            {
                instance = _inactive.Pop();
                
                if (instance == null)
                {
                    return Get(position, rotation, parent);
                }
            }
            else
            {
                instance = CreateInstance();
            }
            
            var poolable = instance.GetComponent<PoolableObject>();
            if (poolable != null)
            {
                poolable.OriginalParent = _poolParent;
            }
            
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            
            if (parent != null)
            {
                instance.transform.SetParent(parent);
            }
            else
            {
                instance.transform.SetParent(null);
            }
            
            instance.SetActive(true);
            _active.Add(instance);
            
            return instance;
        }
        
        public void Clear()
        {
            _isDisposed = true;
            
            while (_inactive.Count > 0)
            {
                GameObject instance = _inactive.Pop();
                if (instance)
                {
                    Object.Destroy(instance);
                }
            }
            
            foreach (var instance in _active)
            {
                if (instance)
                {
                    Object.Destroy(instance);
                }
            }
            
            _active.Clear();
            
            if (_poolParent)
            {
                Object.Destroy(_poolParent.gameObject);
            }
        }
    }
}