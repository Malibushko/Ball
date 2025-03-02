using UnityEngine;

namespace Game.Common.Services.Scenes
{
    public interface IPrefabInstantiator
    {
        public GameObject Instantiate(Object prefab);
        public GameObject Instantiate(string resourcePath);
    }
}