using System;
using Game.Common.Services.Configs;
using Game.GameConfigs;
using UnityEngine;
using Zenject;

namespace Game.Common.Services.Camera
{
    public class CameraService : IFixedTickable, ICameraService
    {
        private UnityEngine.Camera _camera;
        private CameraConfig _config;
        private IConfigsService _configsService;
        
        private Transform _target;
        private bool _isFollowing;
        
        public Action CameraPositionChanged { get; set; }
        
        public CameraService(IConfigsService service, UnityEngine.Camera camera)
        {
            _camera = camera;
            _isFollowing = false;
            _configsService = service;
        }
        
        public void LoadFromConfig(object config)
        {
            _config = _configsService.Load<CameraConfig>(config);
        }
        
        public void Follow(Transform target)
        {
            _target = target;
            _isFollowing = true;
        }
        
        public Ray ScreenPointToRay(Vector2 screenPoint)
        {
            return _camera.ScreenPointToRay(screenPoint);
        }
        
        public void FixedTick()
        {
            if (_target == null)
                return;
            
            if (_isFollowing)
                UpdateFollowCamera();
        }
        
        private void UpdateFollowCamera()
        {
            var lastPosition = _target.position;
            var lastRotation = _target.rotation;
            
            Vector3 targetPosition = _target.position + _target.TransformDirection(_config.PositionOffset);
            Vector3 smoothedPosition = Vector3.Lerp(_camera.transform.position, targetPosition, _config.Speed * Time.deltaTime);
            _camera.transform.position = smoothedPosition;
            
            Quaternion targetRotation = _target.rotation * Quaternion.Euler(_config.RotationOffset);
            Quaternion smoothedRotation = Quaternion.Slerp(_camera.transform.rotation, targetRotation, _config.Speed * Time.deltaTime);
            _camera.transform.rotation = smoothedRotation;
            
            if (lastPosition != targetPosition || lastRotation != targetRotation)
                CameraPositionChanged?.Invoke();
        }
    }
}