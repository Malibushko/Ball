using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.Services.Physics
{
    public class PhysicsService : IPhysicsService
    {
        private class PhysicsObjectData
        {
            public IPhysicsObject PhysicsObject;
            public bool IsGrounded;
            public Action<Collision> OnCollisionEnterHandler;
            public Action<Collision> OnCollisionExitHandler;
        }
        
        private List<PhysicsObjectData> _physicsObjects = new();
         
        public void FixedTick()
        {
            UpdatePhysicsWorld(Time.fixedDeltaTime);
        }
        
        public void Dispose()
        {
            _physicsObjects.Clear();
        }

        public void Register(IPhysicsObject physicsObject)
        {
            PhysicsObjectData physicsObjectData = new();
            
            physicsObjectData.PhysicsObject = physicsObject;
            
            physicsObjectData.OnCollisionEnterHandler = physicsObject.CollisionEnter += collision => OnPhysicsObjectCollisionEnter(physicsObject, collision);
            physicsObjectData.OnCollisionExitHandler = physicsObject.CollisionExit += collision => OnPhysicsObjectCollisionExit(physicsObject, collision);
            
            _physicsObjects.Add(physicsObjectData);
        }
        
        public void Unregister(IPhysicsObject physicsObject)
        {
            PhysicsObjectData physicsObjectData = _physicsObjects.Find(otherObject => otherObject.PhysicsObject.Equals(physicsObject));

            if (physicsObjectData != null)
            {
                physicsObject.CollisionEnter -= physicsObjectData.OnCollisionEnterHandler;
                physicsObject.CollisionExit -= physicsObjectData.OnCollisionExitHandler;
            }
        }

        public void ApplyForce(IPhysicsObject physicsObject, Vector3 force)
        {
            physicsObject.Velocity += force;
        }

        private void UpdatePhysicsWorld(float dt)
        {
            foreach (var objData in _physicsObjects)
            {
                var obj = objData.PhysicsObject;
                if (obj.IsStatic)
                    continue;
        
                Vector3 netForce = Vector3.zero;
        
                if (obj.Rigidbody.useGravity && !objData.IsGrounded)
                {
                    netForce += UnityEngine.Physics.gravity;
                }
                else
                {
                    obj.Velocity = new Vector3(obj.Velocity.x, 0, obj.Velocity.z);
                }
                
                Vector3 acceleration = netForce / obj.Rigidbody.mass;
        
                obj.Velocity += acceleration * dt;
                obj.Velocity -= obj.Velocity * (obj.Rigidbody.drag * dt);
                
                Vector3 moveForce = obj.Velocity * dt;
                obj.Position += moveForce;
            }
        }

        private void OnPhysicsObjectCollisionExit(IPhysicsObject physicsObject, Collision _)
        {
            PhysicsObjectData physicsObjectData = _physicsObjects.Find(otherObject => otherObject.PhysicsObject.Equals(physicsObject));
            physicsObjectData.IsGrounded = IsObjectGrounded(physicsObject);
        }

        private void OnPhysicsObjectCollisionEnter(IPhysicsObject physicsObject, Collision collision)
        {
            if (physicsObject.IsStatic || !physicsObject.Rigidbody)
                return;

            Vector3 netForce = Vector3.zero;
            
            foreach (ContactPoint contact in collision.contacts)
            {
                float velocityAlongNormal = Vector3.Dot(physicsObject.Velocity, contact.normal);
                
                if (velocityAlongNormal > 0)
                    continue;
                
                float invMassA = 1 / physicsObject.Rigidbody.mass;
                
                // $NOTE: currently only supports collision with static objects
                // float invMassB = 1 / otherPhysicsObject.Rigidbody.mass;
                
                float impulse = -(1 + physicsObject.Restitution) * velocityAlongNormal / (invMassA /* invMassB */);
                Vector3 impulseVector = impulse * contact.normal;
                
                netForce += impulseVector;
            }
            
            physicsObject.Velocity += netForce;
            
            PhysicsObjectData physicsObjectData = GetObjectData(physicsObject);
            physicsObjectData.IsGrounded = IsObjectGrounded(physicsObject);
        }

        private PhysicsObjectData GetObjectData(IPhysicsObject physicsObject)
        {
            return _physicsObjects.Find(otherObject => otherObject.PhysicsObject.Equals(physicsObject));
        }

        private bool IsObjectGrounded(IPhysicsObject physicsObject)
        {
            if (!physicsObject.Rigidbody)
                return true;
            
            if (!physicsObject.Rigidbody?.useGravity ?? false)
                return true;
            
            UnityEngine.Physics.Raycast(physicsObject.Position, UnityEngine.Physics.gravity.normalized, out RaycastHit hit);
            
            return hit.collider != null;
        }
    }
}