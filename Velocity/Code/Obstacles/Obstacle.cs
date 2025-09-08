using System;
using Code.Entities;
using Code.Player;
using UnityEngine;

namespace Code.Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] protected LayerMask notHitCameraRayMask;
        [SerializeField] protected float damage = 20;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth damageable))
            {
                bool hit = damageable.TakeDamage(damage); 
                if (hit)
                {
                    gameObject.layer = notHitCameraRayMask.value;
                }
            }
        }
    }
}