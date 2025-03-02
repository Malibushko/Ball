using Game.Common.Services.Effects;
using Game.Gameplay.Input;
using UnityEngine;
using Zenject;
using static Game.Gameplay.Math.Math;

namespace Game.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private GameObject _hitPrefab;
        [SerializeField] private GameObject _killPrefab;
        
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _obstacleLayerMask;
        
        private IPlayer _player;
        private IPlayerInputService _playerInputService;
        private IEffectsFactory _effectsFactory;
        
        [Inject]
        public void Construct(IPlayer player, IPlayerInputService playerInput, IEffectsFactory effectsFactory)
        {
            _player = player;
            _playerInputService = playerInput;
            _effectsFactory = effectsFactory;
        }

        private void OnEnable()
        {
            _player.Activate();
            _playerInputService.SetPlayerTransform(transform);
            _player.OnHit += OnPlayerHit;
        }

        private void OnDisable()
        {
            if (_player != null)
            {
                _player.OnHit -= OnPlayerHit;
                _player.Deactivate();
            }
        }
        
        private void OnPlayerHit(Collision collision)
        {
            if (collision.contacts.Length == 0)
                return;
            
            if (_hitPrefab != null && HasLayer(_obstacleLayerMask, collision.gameObject.layer))
            {
                _effectsFactory.Spawn(_hitPrefab, collision.contacts[0].point, Quaternion.identity);
            }

            if (_killPrefab != null && HasLayer(_enemyLayerMask, collision.gameObject.layer))
            {
                _effectsFactory.Spawn(_killPrefab, collision.contacts[0].point, Quaternion.identity);
            }
        }
    }
}