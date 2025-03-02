using System;
using Core.Infrastructure.Interfaces;
using UnityEngine;

namespace Game.Common.Services.Physics
{
    public interface IPhysicsObject : IActivatableService
    {
        public Vector3 Velocity { get; set;  }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Restitution { get; set; }
        public Rigidbody Rigidbody { get; }
        public bool IsStatic { get; set; }
        
        public Action<Collision> CollisionEnter { get; set; }
        public Action<Collision> CollisionExit { get; set; }
        
        public void ApplyForce(Vector3 force);

        public void SetTarget(Rigidbody target);
    }
}