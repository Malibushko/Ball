using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class CollectableSpawnPoint : SpawnPoint
    {
        [SerializeField] private Resource.Resource _resource;
        [SerializeField] private int _amount;

        protected override string Label => $"{_resource.ResourceName} x{_amount}";
        
        public Resource.Resource Resource => _resource;
        public int Amount => _amount;
    }
}