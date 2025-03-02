using System;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class PointerInputService : IPointerInputService, ITickable
    {
        private const int MainMouseButtonIndex = 0;
        
        public Action<Vector2> OnPointerPressed { get; set; }
        public Action<Vector2> OnPointerReleased { get; set; }
        public Action<Vector2> OnPointerMoved { get; set; }
        public Vector2? PointerPosition => _lastPointerPosition;
        
        private Vector2? _lastPointerPosition;
        
        public void Tick()
        {
            ProcessInput();
        }
        
        private void ProcessInput()
        {
            if (UnityEngine.Input.GetMouseButton(MainMouseButtonIndex))
            {
                var currentMousePosition = UnityEngine.Input.mousePosition;
                
                if (!_lastPointerPosition.HasValue)
                {
                    _lastPointerPosition = currentMousePosition;
                    if (_lastPointerPosition != null) 
                        OnPointerPressed?.Invoke(currentMousePosition);
                } else
                {
                    if (currentMousePosition != _lastPointerPosition)
                    {
                        _lastPointerPosition = currentMousePosition;
                        OnPointerMoved?.Invoke(currentMousePosition);
                    }
                }
            }
            else
            {
                if (_lastPointerPosition.HasValue)
                {
                    OnPointerReleased?.Invoke(_lastPointerPosition.Value);
                    _lastPointerPosition = null;
                }
            }
        }
    }
}