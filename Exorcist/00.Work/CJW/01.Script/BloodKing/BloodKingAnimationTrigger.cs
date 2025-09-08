using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingAnimationTrigger : EnemyAnimationTrigger
{
    private BloodKing _bloodKing;

    protected override void Awake()
    {
        base.Awake();

        _bloodKing = _enemy as BloodKing;
    }

    public void DownAttackAnimaTrigger()
    {
        _bloodKing.DownAttack();
    }

    public void AtkTrigger() => _bloodKing.Attack();
    public void StrongAtkTrigger() => _bloodKing.StrongAtk();
    public void LightAtkTrigger() => _bloodKing.LightAtk();
    
    public void PlayAtkSound() => _bloodKing.PlayAttackSound();
    public void PlayDashSound() => _bloodKing.PlayDashSound();
    public void PlayDownAtkSound() => _bloodKing.PlayDownAttackSound();
}

