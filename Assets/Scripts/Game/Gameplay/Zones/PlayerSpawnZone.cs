using Game.Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Zones
{
    public class PlayerSpawnZone : MonoBehaviour
    {
        [SerializeField] private Transform _playerSpawnPoint;
        
        private PlayerView _playerView;
        
        [Inject]
        public void Construct(PlayerView playerView)
        {
            _playerView = playerView;    
        }

        private void Start()
        {
            _playerView.transform.position = _playerSpawnPoint.position;
        }
    }
}