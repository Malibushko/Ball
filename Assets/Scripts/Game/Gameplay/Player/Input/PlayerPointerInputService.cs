using System;
using Core.Input;
using Game.Common.Services.Camera;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace Game.Gameplay.Input
{
    public class PlayerPointerInputConfig
    {
        [JsonProperty("max_interaction_distance")]
        public float MaxInteractionDistance { get; set; }
    }
    
    public class PlayerPointerInputService : IPlayerInputService
    {
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
        public Action<Vector3> OnInteractionChange { get; set; }
        
        private IPointerInputService _pointerInputService;
        private ICameraService _cameraService;
        
        private Vector3 _currentPosition = Vector3.zero;
        private bool _isInteracting;
        private Transform _playerTransform;
        private PlayerPointerInputConfig _config;
        
        [Inject]
        public void Construct(
            IPointerInputService pointerInputService, 
            ICameraService cameraService, 
            PlayerPointerInputConfig playerPointerInputConfig)
        {
            _pointerInputService = pointerInputService;    
            _cameraService = cameraService;
            _config = playerPointerInputConfig;
        }

        public void SetPlayerTransform(Transform transform)
        {
            _playerTransform = transform;
        }

        public void Activate()
        {
            if (_pointerInputService != null)
            {
                _pointerInputService.OnPointerPressed += OnPointerPressed;
                _pointerInputService.OnPointerReleased += OnPointerReleased;
                _pointerInputService.OnPointerMoved += OnPointerMoved;
                _cameraService.CameraPositionChanged += OnCameraPositionChanged;
            }
        }
        
        public void Deactivate()
        {
            if (_pointerInputService != null)
            {
                _pointerInputService.OnPointerPressed -= OnPointerPressed;
                _pointerInputService.OnPointerReleased -= OnPointerReleased;
                _pointerInputService.OnPointerMoved -= OnPointerMoved;
            }
        }
        
        private void OnPointerMoved(Vector2 position)
        {
            if (_isInteracting)
            {
                UpdateCurrentPosition(position);
                OnInteractionChange?.Invoke(_currentPosition);
            }
        }
        
        private void OnPointerReleased(Vector2 position)
        {
            UpdateCurrentPosition(position);
            
            if (_isInteracting)
                OnInteractionEnd?.Invoke(_currentPosition);
            
            _isInteracting = false;
        }

        private void OnPointerPressed(Vector2 position)
        {
            if (!_isInteracting)
            {
                UpdateCurrentPosition(position);

                if (Vector3.Distance(_currentPosition, _playerTransform.position) < _config.MaxInteractionDistance)
                {
                    _isInteracting = true;
                    OnInteractionBegin?.Invoke(_currentPosition);
                }
            }
        }
        
        private void UpdateCurrentPosition(Vector2 position)
        {
            if (TryGetWorldPosition(position, out Vector3 worldPos))
            {
                _currentPosition = worldPos;
            }
        }

        private void OnCameraPositionChanged()
        {
            if (_isInteracting && _pointerInputService.PointerPosition.HasValue)
            {
                UpdateCurrentPosition(_pointerInputService.PointerPosition.Value);
                OnInteractionChange?.Invoke(_currentPosition);
            }
        }
        
        private bool TryGetWorldPosition(Vector2 position, out Vector3 worldPosition)
        {
            var ray = _cameraService.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                worldPosition = Vector3.ProjectOnPlane(hit.point, Vector3.up);
                return true;
            }
            
            worldPosition = Vector3.zero;
            return false;
        }
    }
}