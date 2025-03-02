using System;
using Core.Infrastructure.Interfaces;
using UnityEngine;

namespace Game.Common.Services.Camera
{
    public interface ICameraService : IConfigLoadable
    {
        Action CameraPositionChanged { get; set; }
        
        void Follow(Transform target);
        
        Ray ScreenPointToRay(Vector2 screenPoint);
    }
}