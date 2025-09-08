using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingContinuousAttackState : BloodKingAttackState
{
    public BloodKingContinuousAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(false);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            _triggerCalled = false;
            _triggerCount++;
        }
        
        if(_triggerCount == 1)
            _bloodKing.BloodKingAnimationComp.Blink(true);
        if (_triggerCount == 2)
        {
            _bloodKing.BloodKingAnimationComp.Blink(false);
            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
        }
    }

}
