using System;
using UnityEngine;

namespace Core.Input
{
    public interface IKeyboardInputService
    {
        public Action<KeyCode> OnKeyPressed { get; set; }
        public Action<KeyCode> OnKeyReleased { get; set; }
    
        public void TrackKey(KeyCode keyCode);
        public void UntrackKey(KeyCode keyCode);
    }
}