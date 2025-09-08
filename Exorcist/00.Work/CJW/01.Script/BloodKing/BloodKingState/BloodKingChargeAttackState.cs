using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingChargeAttackState : BloodKingAttackState
{
    public BloodKingChargeAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(false);
        _triggerCount = 0;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled)
        {
            _triggerCount++;
            _triggerCalled = false;
        }

        if (_triggerCount == 1f)
        {
            _bloodKing.BloodKingAnimationComp.Blink(true);
            _triggerCount++;
        }

        if (_triggerCount == 3f)
        {
            float dir = _enemyBase.FacingDirection;
            
            _bloodKing.SetVelocity(dir * 50, 0);
            _triggerCount++;
        }

        if (_triggerCount == 5f)
        {
            _bloodKing.StopImmediately(false);
            _triggerCount++;
        }

        if (_triggerCount == 7f)
        {
            _bloodKing.BloodKingAnimationComp.Blink(false);
            _triggerCount++;
        }
        
        if (_triggerCount == 9f)
        {
            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
            _triggerCount++;
        }
    }
}
