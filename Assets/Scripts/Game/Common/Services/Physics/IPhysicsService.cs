using System;
using UnityEngine;
using Zenject;

namespace Game.Common.Services.Physics
{
    public interface IPhysicsService : IFixedTickable, IDisposable
    {
        public void Register(IPhysicsObject physicsObject);
        public void Unregister(IPhysicsObject physicsObject);
        
        public void ApplyForce(IPhysicsObject physicsObject, Vector3 force);
    }
}