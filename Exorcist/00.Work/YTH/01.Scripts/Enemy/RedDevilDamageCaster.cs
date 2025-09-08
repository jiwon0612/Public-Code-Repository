using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDevilDamageCaster : DamageCaster
{
    public int[] AttackDamage = { 15, 25, 35 };

    public bool CastDamageRectangle(int combo, Transform attackCheckerTrm)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(whatIsEnemy);

        Vector2 boxSize = new Vector2(attackCheckerWidth, attackCheckerHeight);

        int cnt = Physics2D.OverlapBox(
            attackCheckerTrm.position,
            boxSize,
            attackCheckerTrm.rotation.eulerAngles.z,
            filter,
            _hitResult);

        for (int i = 0; i < cnt; ++i)
        {
            Vector2 direction = (_hitResult[i].transform.position - transform.position).normalized;

            if (_hitResult[i].TryGetComponent<IDamageable>(out IDamageable target))
            {
                int damage = AttackDamage[1];
                damage = CalculateDamage(damage);

                target.ApplyDamage(damage, direction, knockbackPower[combo]);
            }
        }

        SetDamageMultiplier(1);

        return cnt > 0;
    }

    public bool CastDamageTwoSwing(int combo, Transform attackCheckerTrm, int damageNumber)
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
                int damage = AttackDamage[damageNumber];

                damage = CalculateDamage(damage);

                target.ApplyDamage(damage, direction, knockbackPower[combo]);
            }

        }
        SetDamageMultiplier(1);

        return cnt > 0;
    }

    public bool CastDamageTilledRectangle(int combo, Transform attackCheckerTrm, bool isRight)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(whatIsEnemy);

        Vector2 boxSize = new Vector2(attackCheckerWidth, attackCheckerHeight / 2);

        int cnt = Physics2D.OverlapBox(
            attackCheckerTrm.position,
            boxSize,
            isRight ? 45:-45,
            filter,
            _hitResult);

        for (int i = 0; i < cnt; ++i)
        {
            Vector2 direction = (_hitResult[i].transform.position - transform.position).normalized;

            if (_hitResult[i].TryGetComponent<IDamageable>(out IDamageable target))
            {
                int damage = AttackDamage[1];
                damage = CalculateDamage(damage);

                target.ApplyDamage(damage, direction, knockbackPower[combo]);
            }
        }

        SetDamageMultiplier(1);


        return cnt > 0;
    }

}
