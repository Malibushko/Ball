using System;
using Core.Infrastructure.Interfaces;
using UnityEngine;

namespace Game.Gameplay.Input
{
    public interface IPlayerInputService : IActivatableService
    {
        public Action<Vector3> OnInteractionBegin { get; set; }
        public Action<Vector3> OnInteractionEnd { get; set; }
        public Action<Vector3> OnInteractionChange { get; set; }
        
        public void SetPlayerTransform(Transform transform);
    }
}