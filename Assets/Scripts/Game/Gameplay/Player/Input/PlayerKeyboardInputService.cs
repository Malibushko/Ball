using System;
using System.Linq;
using Core.Input;
using Game.Gameplay.Input;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Player.Input
{
    public class PlayerKeyboardInputService : IPlayerInputService, ITickable
    {
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
        public Action<Vector3> OnInteractionChange { get; set; }
        
        private IKeyboardInputService _input;
        private PlayerKeyboardInputConfig _playerKeyboardInputConfig;
        private bool _isMoving;
        private bool _isInteracting;
        private Vector3 _currentDirection;
        private Vector2 _currentMovement;
        private Transform _playerTransform;
        
        [Inject]
        public void Construct(IKeyboardInputService input, PlayerKeyboardInputConfig playerKeyboardInputConfig)
        {
            _input = input; 
            _playerKeyboardInputConfig = playerKeyboardInputConfig;
        }
        
        public void SetPlayerTransform(Transform transform)
        {
            _playerTransform = transform;
        }
        
        public void Activate()
        {
            if (_input != null)
            {
                _input.OnKeyPressed += OnKeyPressed;
                _input.OnKeyReleased += OnKeyReleased;

                foreach (var key in _playerKeyboardInputConfig.Keys)
                    _input.TrackKey(key);
            }
        }
        
        public void Deactivate()
        {
            if (_input != null)
            {
                _input.OnKeyPressed -= OnKeyPressed;
                _input.OnKeyReleased -= OnKeyReleased;
                
                foreach (var key in _playerKeyboardInputConfig.Keys)
                    _input.UntrackKey(key);
            }
        }
        
        private void OnKeyPressed(KeyCode key)
        {
            var isMoving = IsMovingKey(key);
            
            if (isMoving && !_isMoving)
            {
                _isMoving = true;
                _currentDirection = _playerTransform.forward;
                OnInteractionBegin?.Invoke(_playerTransform.position);
            }
            
            OnKeyStateChanged(key, true);
        }

        private void OnKeyReleased(KeyCode key)
        {
            OnKeyStateChanged(key, false);
            
            if (_currentMovement == Vector2.zero)
            {
                _isMoving = false;
                OnInteractionEnd?.Invoke(_playerTransform.position + _currentDirection);
            }
        }

        private void OnKeyStateChanged(KeyCode key, bool isDown)
        {
            Vector2 movement = Vector2.zero;
            
            if (key == _playerKeyboardInputConfig.Left)
                movement += Vector2.left;
            else if (key == _playerKeyboardInputConfig.Right)
                movement += Vector2.right;
            else if (key == _playerKeyboardInputConfig.Up)
                movement += Vector2.up;
            else if (key == _playerKeyboardInputConfig.Down)
                movement += Vector2.down;
            
            _currentMovement += movement * (isDown ? 1 : -1);
        }
        
        private bool IsMovingKey(KeyCode key)
        {
            return _playerKeyboardInputConfig.Keys.Contains(key);
        }

        public void Tick()
        {
            if (_isMoving)
            {
                _currentDirection += _playerKeyboardInputConfig.Speed * Time.deltaTime * new Vector3(_currentMovement.x, 0, _currentMovement.y);
                OnInteractionChange?.Invoke(_currentDirection + _playerTransform.position);
            }
        }
    }
}