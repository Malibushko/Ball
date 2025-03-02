using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Effects;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Collectable
{
    [RequireComponent(typeof(Animator))]
    public class CollectableView : MonoBehaviour
    {
        [SerializeField] private LayerMask _collectorLayer;
        [SerializeField] private string _collectTriggerId;
        [SerializeField] private GameObject _collectEffect;
        
        private Collectable _collectable;
        private IEffectsFactory _effectsFactory;
        private Animator _animator;
        private CancellationTokenSource _cancellationTokenSource = new();
        private bool _isCollected;
        
        [Inject]
        public void Construct(Collectable collectable, IEffectsFactory effectsFactory)
        {
            _collectable = collectable;
            _effectsFactory = effectsFactory;
        }

        public void Configure(Resource.Resource resource, int amount)
        {
            _collectable.Configure(resource, amount);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (!_isCollected && Math.Math.HasLayer(_collectorLayer, collision.gameObject.layer))
            {
                _isCollected = true;
                Collect();
            }
        }

        private void Collect()
        {
            _collectable.Collect();
            
            if (_collectEffect != null)
                _effectsFactory.Spawn(_collectEffect, transform.position, Quaternion.identity);

            StartCollectAnimation();
        }

        private void StartCollectAnimation()
        {
            _animator.SetTrigger(_collectTriggerId);
            AnimationClip clip = _animator.runtimeAnimatorController.animationClips[0];

            UniTask
                .Delay(TimeSpan.FromSeconds(clip.length), cancellationToken: _cancellationTokenSource.Token)
                .ContinueWith(OnCollectionAnimationEnd);
        }

        private void OnCollectionAnimationEnd()
        {
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}