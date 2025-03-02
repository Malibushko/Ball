using UnityEngine;

namespace Game.Gameplay.Services.Collectable
{
    public interface ICollectableFactory
    {
        public GameObject Create(GameObject prefab, Resource.Resource resource, int amount, Vector3 position);
    }
}