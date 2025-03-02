using UnityEngine;

namespace Game.Gameplay.Resource
{
    [CreateAssetMenu(fileName = "Data", menuName = "Resources/GameResource", order = 1)]
    public class Resource : ScriptableObject
    {
        [SerializeField] private string _resourceName;
        
        public string ResourceName => _resourceName;
    }
}