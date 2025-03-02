using UnityEngine;

namespace Game.Common.Components
{
    public class DestroyWithDelay : MonoBehaviour
    {
        [SerializeField] private float _delay = 3f;
        
        private void Start()
        {
            Destroy(gameObject, _delay);
        }
    }
}