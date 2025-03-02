using System;
using UnityEngine;

namespace Core.Input
{
    public interface IPointerInputService
    {
        public Action<Vector2> OnPointerPressed { get; set; }
        public Action<Vector2> OnPointerReleased {get; set; }
        public Action<Vector2> OnPointerMoved { get; set; }
    
        public Vector2? PointerPosition { get; }
    }
}
