using System;
using System.Collections.Generic;
using Core.Infrastructure.Misc;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class KeyboardInputService : IKeyboardInputService, ITickable
    {
        public Action<KeyCode> OnKeyPressed { get; set; }
        public Action<KeyCode> OnKeyReleased { get; set; }
        
        private Vector2? _lastPointerPosition;
        
        private List<MutableKeyValuePair<KeyCode, bool>> _trackedKeysStates = new();
        private HashSet<KeyCode> _trackedKeys = new();
        
        public void TrackKey(KeyCode keyCode)
        {
            if (_trackedKeys.Add(keyCode))
                _trackedKeysStates.Add(new MutableKeyValuePair<KeyCode, bool>(keyCode, false));
        }

        public void UntrackKey(KeyCode keyCode)
        {
            if (_trackedKeys.Remove(keyCode))
                _trackedKeysStates.RemoveAll(x => x.Key == keyCode);
        }
        
        private void ProcessInput()
        {
            foreach (var key in _trackedKeysStates)
            {
                var wasDown = key.Value;
                var isDown = UnityEngine.Input.GetKey(key.Key);

                if (wasDown != isDown)
                {
                    if (isDown)
                        OnKeyPressed?.Invoke(key.Key);
                    else
                        OnKeyReleased?.Invoke(key.Key);
                }
                
                key.Value = isDown;
            }
        }

        public void Tick()
        {
            ProcessInput();
        }
    }
}