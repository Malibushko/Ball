using Game.Gameplay.Collectable;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Services.Collectable
{
    public class CollectableFactory : ICollectableFactory
    {
        private readonly DiContainer _container;

        public CollectableFactory(DiContainer container)
        {
            _container = container;
        }
        
        public GameObject Create(GameObject prefab, Resource.Resource resource, int amount, Vector3 position)
        {            
            CollectableView viewObject = _container.InstantiatePrefabForComponent<CollectableView>(prefab);
            
            viewObject.Configure(resource, amount);
            viewObject.transform.position = position;
            
            return viewObject.gameObject;
        }
    }
}