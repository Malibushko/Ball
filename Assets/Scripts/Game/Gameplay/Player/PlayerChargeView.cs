using System;
using System.Collections.Generic;
using Game.Gameplay.Mesh;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Player
{
    public class PlayerChargeView : MonoBehaviour
    {
        [SerializeField] private ChargeMeshRenderer _chargeMeshRenderer;
        
        private IPlayerCharge _playerChargeController;
        private bool _isCharging;
        
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        
        private List<IDisposable> _disposables = new();
        
        [Inject]
        public void Construct(IPlayerCharge playerChargeController)
        {
            _playerChargeController = playerChargeController;
        }

        private void OnEnable()
        {
            if (_playerChargeController != null)
            {
                _playerChargeController.ChargeStartPosition.Subscribe(OnChargeStartChange).AddTo(_disposables);
                _playerChargeController.ChargeEndPosition.Subscribe(OnChargeEndChange).AddTo(_disposables);
                _playerChargeController.IsCharging.Subscribe(OnChargingChange).AddTo(_disposables);
            }
        }
        
        private void OnDisable()
        {
            _disposables.ForEach(x => x.Dispose());
            _disposables.Clear(); 
        }
        
        private void OnChargingChange(bool isCharging)
        {
            _isCharging = isCharging;
            
            if (!isCharging)
                OnChargeEnd();
        }

        private void OnChargeEnd()
        {
            _isCharging = false;
            _chargeMeshRenderer.Reset();
        }
        
        private void OnChargeStartChange(Vector3 position)
        {
            if (_isCharging)
            {
                _isCharging = true;
                _startPosition = position;

                UpdateChargeMesh();
            }
        }
        
        private void OnChargeEndChange(Vector3 position)
        {
            if (_isCharging)
            {
                _endPosition = position;
                UpdateChargeMesh();
            }
        }

        private void UpdateChargeMesh()
        {
            _chargeMeshRenderer.OnChargeChange(_startPosition, _endPosition);
        }
    }
}