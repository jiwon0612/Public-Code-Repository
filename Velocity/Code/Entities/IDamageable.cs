using UnityEngine;

namespace Code.Entities
{
    public interface IDamageable
    {
        public bool TakeDamage(float damage, Vector3 hitPoint);
    }
}