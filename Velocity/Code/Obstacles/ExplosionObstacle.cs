using Code.Player;
using Hellmade.Sound;
using UnityEngine;

namespace Code.Obstacles
{
    public class ExplosionObstacle : Obstacle
    {
        [SerializeField] private AudioClip _audioClip;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth damageable))
            {
                bool hit = damageable.TakeDamage(damage); 
                if (hit)
                {
                    EazySoundManager.PlaySound(_audioClip);
                    gameObject.layer = notHitCameraRayMask.value;
                    Destroy(gameObject);
                }
            }
        }
    }
}