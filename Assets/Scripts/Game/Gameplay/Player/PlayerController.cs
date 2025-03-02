using Game.Common.Services.Physics;
using Game.Gameplay.Input;
using Game.Gameplay.Player.Configs;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Player
{
    public class PlayerController : IPlayerController, IPlayerCharge
    {
        public IReadOnlyReactiveProperty<Vector3> ChargeStartPosition => _chargeStartPosition;
        public IReadOnlyReactiveProperty<Vector3> ChargeEndPosition =>_chargeEndPosition;
        public IReadOnlyReactiveProperty<bool> IsCharging => _isCharching;

        private ReactiveProperty<Vector3> _chargeStartPosition = new();
        private ReactiveProperty<Vector3> _chargeEndPosition = new();
        private ReactiveProperty<bool> _isCharching = new();
        
        private IPlayerInputService _input;
        private IPhysicsObject _physicsObject;
        private PlayerControllerConfig _config;

        [Inject]
        public void Construct(IPlayerInputService input, IPhysicsObject physicsObject, PlayerControllerConfig config)
        {
            _input = input;
            _physicsObject = physicsObject;
            _config = config;
        }

        public void Activate()
        {
            _input.OnInteractionBegin += OnInteractionBegin;
            _input.OnInteractionEnd += OnInteractionEnd;
        }
        
        public void Deactivate()
        {
            _input.OnInteractionBegin -= OnInteractionBegin;
            _input.OnInteractionEnd -= OnInteractionEnd;
        }
        
        private void OnInteractionBegin(Vector3 position)
        {
            OnChargeBegin();
        }

        private void OnChargeBegin()
        {
            _isCharching.Value = true;
            _chargeStartPosition.Value = _physicsObject.Position;
            _chargeEndPosition.Value = _physicsObject.Position;
            
            _input.OnInteractionChange += OnInteractionChange;
            _input.OnInteractionEnd += OnInteractionEnd;
        }
        
        private void OnChargeEnd()
        {
            _isCharching.Value = false;
            
            _input.OnInteractionChange -= OnInteractionChange;
            _input.OnInteractionEnd -= OnInteractionEnd;
        }
        
        private void OnInteractionChange(Vector3 position)
        {
            _chargeEndPosition.Value = position;
            _chargeStartPosition.Value = _physicsObject.Position;
        }
        
        private void OnInteractionEnd(Vector3 position)
        {
            var movement = (_chargeEndPosition.Value - _chargeStartPosition.Value) * _config.Speed;
            
            _physicsObject.ApplyForce(movement);
            
            OnChargeEnd();
        }
    }
}