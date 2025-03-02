using UnityEngine;

namespace Game.Gameplay.Services.Enemy
{
    public interface IEnemyFactory
    {
        public GameObject Create(GameObject enemyPrefab, string configPath, Vector3 position, Quaternion rotation);
    }
}