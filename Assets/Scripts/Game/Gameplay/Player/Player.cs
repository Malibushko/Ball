using System;
using Game.Common.Services.Physics;
using Game.Gameplay.Input;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Player
{
    public class Player : IPlayer
    {
        public Action<Collision> OnHit { get; set; }
        
        private IPhysicsObject _physicsObject;
        private IPlayerInputService _playerInputService;
        private IPlayerController _playerController;
        
        [Inject]
        public void Construct(
            IPhysicsObject physicsObject, 
            IPlayerInputService playerInputService,
            IPlayerController playerController)
        {
            _physicsObject = physicsObject;
            _playerInputService = playerInputService;
            _playerController = playerController;
            _physicsObject.CollisionEnter += OnCollisionEnter;
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke(collision);
        }

        public void Activate()
        {
            _physicsObject.Activate();
            _playerInputService.Activate();
            _playerController.Activate();
        }

        public void Deactivate()
        {
            _physicsObject.Deactivate();
            _playerInputService.Deactivate();
            _playerController.Deactivate();
        }
    }
}