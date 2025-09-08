using System;
using System.Reflection;
using UnityEngine;

public class BloodKingDamageCaster : DamageCaster
{
    public int lightAtkDamage;
    public int atkDamage;
    public int strongAtkDamage;

    public bool CastSetDamage(int combo, int damage, Transform attackCheckerTrm)
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
                damage = CalculateDamage(damage);

                target.ApplyDamage(damage, direction, knockbackPower[combo]);
            }

        }
        SetDamageMultiplier(1);

        return cnt > 0;
    }
}
