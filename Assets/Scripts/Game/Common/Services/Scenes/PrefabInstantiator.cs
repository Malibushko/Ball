using UnityEngine;
using Zenject;

namespace Game.Common.Services.Scenes
{
    public class PrefabInstantiator : IPrefabInstantiator
    {
        private DiContainer _container;

        public PrefabInstantiator(DiContainer container)
        {
            _container = container;
        }
        
        public GameObject Instantiate(Object prefab)
        {
            return _container.InstantiatePrefab(prefab);
        }

        public GameObject Instantiate(string prefabName)
        {
            return _container.InstantiatePrefabResource(prefabName);
        }
    }
}