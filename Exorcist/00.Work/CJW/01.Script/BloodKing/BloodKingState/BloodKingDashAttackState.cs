using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingDashAttackState : BloodKingAttackState
{
    public BloodKingDashAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _triggerCalled = false;
            _triggerCount++;
        }

        if(_triggerCount == 1)
        {
            float dir = _enemyBase.FacingDirection;
            _bloodKing.SetVelocity(dir * 50, 0);
            _triggerCount++;
            //_bloodKing.RigidbodyCompo.AddForce(new Vector2(dir * 20,0), ForceMode2D.Impulse);
        }

        if(_triggerCount == 3)
        {
            _triggerCount++;
            _bloodKing.StopImmediately(false);
        }

        if(_triggerCount == 5)
        {
            _triggerCount++;
            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
        }


    }

    public override void Exit()
    {
        base.Exit();

    }

}
