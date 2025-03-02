using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Common.Services.Physics;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Player
{
    public class PlayerPhysicsDeformer : MonoBehaviour
    {
        [SerializeField] private Transform _playerModel;
        [SerializeField] private float _rotationSpeed = 10;
        [SerializeField] private float _squishSpeedFactor = 0.05f;
        
        public float _squishDuration = 0.7f;
        private Vector3 _originalScale;
        private UniTask _squishTask;
        private CancellationTokenSource _cts = new();
        
        private IPhysicsObject _player;
        
        [Inject]
        public void Construct(IPhysicsObject physicsObject)
        {
            _player = physicsObject;
            _player.CollisionEnter += OnCollision;
            _originalScale = _playerModel.localScale;
        }
        
        private void FixedUpdate()
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, _player.Velocity.normalized);
            float speed = _player.Velocity.magnitude;

            _playerModel.transform.Rotate(rotationAxis, speed * _rotationSpeed * Time.fixedDeltaTime, Space.World); 
        }
        
        private void OnCollision(Collision obj)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = new CancellationTokenSource();
            _squishTask = SquishAsync(_cts.Token);
            _squishTask.Forget();
        }
        
        void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
        
        private async UniTask SquishAsync(CancellationToken token)
        {
            Vector3 stretchAxis = _playerModel.transform.forward.normalized;
            stretchAxis *= _player.Velocity.magnitude * _squishSpeedFactor;

            var stretchScale = _originalScale + stretchAxis;

            float deformDuration = _squishDuration / 3f;
            await ScaleAsync(_originalScale, stretchScale, deformDuration, token);
            
            var squishScale = new Vector3(stretchScale.z, stretchScale.y, stretchScale.x);
            await ScaleAsync(stretchScale, squishScale, deformDuration, token);
            
            await ScaleAsync(squishScale, _originalScale, deformDuration, token);
            
            if (!token.IsCancellationRequested)
            {
                _playerModel.transform.localScale = _originalScale;
            }
        }

        private async UniTask ScaleAsync(Vector3 startScale, Vector3 targetScale, float duration, CancellationToken token)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration && !token.IsCancellationRequested)
            {
                elapsedTime += Time.deltaTime;
                float lerp = elapsedTime / duration;
                _playerModel.transform.localScale = Vector3.Lerp(startScale, targetScale, lerp);
                await UniTask.Yield(token);
            }
        }
    }
}