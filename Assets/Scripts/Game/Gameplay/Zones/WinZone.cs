using Cysharp.Threading.Tasks;
using Game.Gameplay.Player;
using Game.Gameplay.Services.Level;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Zones
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Animator))]
    public class WinZone : MonoBehaviour
    {
        [SerializeField] private string _activateAnimationId = "Activate";

        private ILevelProgressWatcherService _levelProgressWatcherService;
        private Animator _animator;
        private bool _isActivated;
        private bool _wasTriggered;
        
        [Inject]
        public void Construct(ILevelProgressWatcherService levelProgressWatcherService)
        {
            _levelProgressWatcherService = levelProgressWatcherService;
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public async UniTask Activate()
        {
            _animator.Play(_activateAnimationId);
            
            await UniTask.WaitUntil(() => {
                var state = _animator.GetCurrentAnimatorStateInfo(0);
                return state.IsName(_activateAnimationId) && state.normalizedTime >= 1.0f;
            });
            
            _isActivated = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isActivated && other.TryGetComponent<PlayerView>(out _) && !_wasTriggered)
            {
                _wasTriggered = true;
                _levelProgressWatcherService.OnWinZoneReached();
            }
        }
    }
}