using UnityEditor;
using UnityEngine;

namespace Game.Gameplay.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] private float _gizmoRadius = 0.5f;
        [SerializeField] private Color _pointColor = Color.green;
        [SerializeField] private Color _directionColor = Color.white;
        [SerializeField] private string _label;
        
        protected virtual string Label => _label;
        protected virtual Color PointColor => _pointColor;
        protected virtual Color DirectionColor => _directionColor;
        protected virtual float GizmoRadius => _gizmoRadius;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = PointColor;
            Gizmos.DrawSphere(transform.position, GizmoRadius);
            Gizmos.color = DirectionColor;
            var lineLength = transform.position + Vector3.up * 2;
            Gizmos.DrawLine(transform.position, lineLength);
            Handles.Label(lineLength, Label);
        }
#endif
    }
}