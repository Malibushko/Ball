using Game.Common.Services.Configs;
using UnityEngine;

namespace Game.Gameplay.Enemy.Common
{
    public class EnemyPerpendicularHitHandler : IEnemyHitHandler
    {
        private const float PerpendicularAngle = 90f;
        
        private EnemyPerpendicularHitHandlerConfig _config;
        private IConfigsService _configs;

        public EnemyPerpendicularHitHandler(IConfigsService configs)
        {
            _configs = configs;
        }
        
        public bool WasHit(Transform transform, Collision collision)
        {
            foreach (var contact in collision.contacts)
            {
                var normal = new Vector3(contact.normal.x, 0f, contact.normal.z);
                var forward = new Vector3(transform.forward.x, 0f, transform.forward.z);
                
                float cosMin = Mathf.Cos(Mathf.Deg2Rad * PerpendicularAngle - _config.HitAngleThreshold);
                float cosMax = Mathf.Cos(Mathf.Deg2Rad * PerpendicularAngle + _config.HitAngleThreshold);
                
                float dot = Vector3.Dot(normal, forward);
                
                if (cosMin > dot && dot < cosMax)
                    return true;
            }

            return false;
        }

        public void LoadFromConfig(object config)
        {
            _config = _configs.Load<EnemyPerpendicularHitHandlerConfig>(config);
        }
    }
}