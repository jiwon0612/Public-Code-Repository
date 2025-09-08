using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    private RedDevilEnemy _enemyBase;
    private bool isHit;
    public RedDevilDamageCaster _damageCaster;

    private void Awake()
    {
        _enemyBase = GetComponentInParent<RedDevilEnemy>();
        //_damageCaster = gameObject.Find("DamageCaster").GetComponent<RedDevilDamageCaster>();
    }

    public void AnimationStart() => _enemyBase.OffAnimationTrigger();
    public void AnimationEnd() => _enemyBase.OnAnimationTrigger();
    public void FadeOn() => _enemyBase.FadeOn();
    public void FadeOff() => _enemyBase.FadeOff();

    private void LazerAttack()
    {
        isHit = _damageCaster.CastDamageTwoSwing(1, _enemyBase.sideTrmR, 2);
        isHit = _damageCaster.CastDamageTwoSwing(1, _enemyBase.sideTrmL, 2);
        if (isHit)
        {
            EazySoundManager.PlaySound(_enemyBase.HitSfx);
        }
    }
    private void LazerRoadAttack1()
    {
        isHit = _damageCaster.CastDamageTilledRectangle(0, _enemyBase.boxRoadR, true);
        isHit = _damageCaster.CastDamageTilledRectangle(0, _enemyBase.boxRoadL, false);
        if (isHit)
        {
            EazySoundManager.PlaySound(_enemyBase.HitSfx);
        }
    }
    private void LazerRoadAttack2()
    {
        isHit = _damageCaster.CastDamageTilledRectangle(0, _enemyBase.boxRoadR2, true);
        isHit = _damageCaster.CastDamageTilledRectangle(0, _enemyBase.boxRoadL2, false);
        if (isHit)
        {
            EazySoundManager.PlaySound(_enemyBase.HitSfx);
        }
    }

    private void SideSlash()
    {
        isHit = _damageCaster.CastDamageRectangle(1, _enemyBase.boxSideTrmR);
        isHit = _damageCaster.CastDamageRectangle(1, _enemyBase.boxSideTrmL);
        if (isHit)
        {
            EazySoundManager.PlaySound(_enemyBase.HitSfx);
        }
    }

    private void TwoSwing()
    {
        isHit = _damageCaster.CastDamageTwoSwing(0, _enemyBase.frontTrm2, 1);
        isHit = _damageCaster.CastDamageTwoSwing(0, _enemyBase.frontTrm, 1);
        if (isHit)
        {
            EazySoundManager.PlaySound(_enemyBase.HitSfx);
        }
    }


    private void LazerSound() => EazySoundManager.PlaySound(_enemyBase.LazerSfx);
    private void LazerTwoSound() => EazySoundManager.PlaySound(_enemyBase.LazerTwoSfx);
    private void SideSlashSound() => EazySoundManager.PlaySound(_enemyBase.SideSlashSfx);
    private void SlashSound() => EazySoundManager.PlaySound(_enemyBase.SlashSfx);
    private void MagicSound() => EazySoundManager.PlaySound(_enemyBase.MagicSfx);
    private void AppearSound() => EazySoundManager.PlaySound(_enemyBase.AppearSfx);
    private void FadeSound() => EazySoundManager.PlaySound(_enemyBase.FadeSfx);
    private void DeadSound() => EazySoundManager.PlaySound(_enemyBase.DeadSfx);
}

