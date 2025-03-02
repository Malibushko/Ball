using UnityEngine;
using Zenject;

namespace Game.Common.Services.Physics
{
    [RequireComponent(typeof(Rigidbody))]
    public class PhysicsComponent : MonoBehaviour
    {
        [SerializeField] private bool _activateOnStart;
        [SerializeField] private bool _isStatic;

        [Tooltip("Defines bounciness for object; [0 is unelastic, 1 is fully elastic object]")]
        [Range(0, 1)]
        [SerializeField] private float _restitution;
        
        private IPhysicsObject _physicsObject;
        
        [Inject]
        public void Construct(IPhysicsObject physicsObject)
        {
            _physicsObject = physicsObject;
            _physicsObject.IsStatic = _isStatic;
            _physicsObject.Restitution = _restitution;
        }

        private void Awake()
        {
            _physicsObject.SetTarget(GetComponent<Rigidbody>());
        }

        private void Start()
        {
            if (_activateOnStart)
                _physicsObject.Activate();
        }

        private void OnCollisionEnter(Collision other)
        {
            _physicsObject.CollisionEnter?.Invoke(other);
        }

        private void OnCollisionExit(Collision other)
        {
            _physicsObject.CollisionExit?.Invoke(other);
        }
    }
}