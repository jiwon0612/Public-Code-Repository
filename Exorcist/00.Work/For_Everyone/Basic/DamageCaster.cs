using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    public int attackDamage;
    public int skillDamage;

    public float attackCheckerRadius;
    public float attackCheckerWidth;
    public float attackCheckerHeight;

    public Vector2[] knockbackPower;

    [SerializeField] protected int _maxHitCount = 5;
    public LayerMask whatIsEnemy;
    protected Collider2D[] _hitResult;

    protected Entity _owner;

    [SerializeField] protected float _damageMuliplier = 1f;

    protected virtual void Awake()
    {
        _hitResult = new Collider2D[_maxHitCount];
    }

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }


    public virtual void GunShoot(Vector2 dir)
    {
        if (Physics2D.Raycast(transform.position, dir, 10, whatIsEnemy))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10, whatIsEnemy);

            Vector2 direction = (hit.transform.position - transform.position).normalized;
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                int damage = skillDamage;
                //CastDamageWithStun(target, 3, dir, Vector2.zero, 3f);
                SetDamageMultiplier(3f);
                target.ApplyDamage(damage, direction, Vector2.zero);
                CamManager.Instance.CamShake(.2f, 5);
            }
        }
    }
    
    public bool CastDamage(int combo, Transform attackCheckerTrm)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(whatIsEnemy);

        int cnt = Physics2D.OverlapCircle(
            attackCheckerTrm.position,
            attackCheckerRadius,
            filter,
            _hitResult);

        for (int i = 0; i < cnt; ++i)
        {
            //피격 방향을 구해야하고
            Vector2 direction = (_hitResult[i].transform.position - transform.position).normalized;

            if (_hitResult[i].TryGetComponent<IDamageable>(out IDamageable target))
            {
                int damage = attackDamage;

                damage = CalculateDamage(damage);

                target.ApplyDamage(damage, direction, knockbackPower[combo]);
            }

        }
        SetDamageMultiplier(1);

        return cnt > 0;
    }

    public void CastDamageWithStun(IDamageable target, float multiplier, Vector2 attackDirection,
        Vector2 stunPower, float stunTime)
    {
        int damage = 5;
        damage = Mathf.CeilToInt(damage * multiplier);
        damage = CalculateDamage(damage);
        target.ApplyDamage(damage, attackDirection, stunPower);
        target.Stun(stunTime);
    }

    public void SetDamageMultiplier(float multiplier)
    {
        _damageMuliplier = multiplier;
    }

    protected int CalculateDamage(int originalDamage)
    {
        return Mathf.CeilToInt(originalDamage * _damageMuliplier);
    }

#if UNITY_EDITOR
    //protected virtual void OnDrawGizmos()
    //{
    //    if (attackCheckerTrm != null || UpattackCheckerTrm != null || DownattackCheckerTrm != null)
    //    {
    //        Gizmos.DrawWireSphere(attackCheckerTrm.position, attackCheckerRadius);
    //        Gizmos.DrawWireSphere(UpattackCheckerTrm.position, attackCheckerRadius);
    //        Gizmos.DrawWireSphere(DownattackCheckerTrm.position, attackCheckerRadius);
    //    }
    //}
#endif
}