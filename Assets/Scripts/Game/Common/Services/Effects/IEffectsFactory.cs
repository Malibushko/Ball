using UnityEngine;

namespace Game.Common.Services.Effects
{
    public interface IEffectsFactory
    {
        GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
    }
}