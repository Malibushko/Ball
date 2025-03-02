using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Infrastructure.Pool
{
    public class PoolableObject : MonoBehaviour
    {
        public ObjectsPool Pool { get; set; }
        public Transform OriginalParent { get; set; }
    
        private void OnDestroy()
        {
            if (Pool != null && gameObject.activeInHierarchy == false)
            {
                ReturnToPoolNextFrame().Forget();
            }
        }
    
        private async UniTaskVoid ReturnToPoolNextFrame()
        {
            await UniTask.NextFrame();
            if (this != null && Pool != null)
            {
                Pool.Return(gameObject);
            }
        }
    }
}