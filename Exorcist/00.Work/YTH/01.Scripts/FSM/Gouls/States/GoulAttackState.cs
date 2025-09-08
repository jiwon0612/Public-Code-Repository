using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulAttackState : EnemyState<GoulStateEnum>
{
    public GoulAttackState(Enemy enemyBase, EnemyStateMachine<GoulStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(false);
    }

    public override void Exit()
    {
        _enemyBase.lastTimeAttacked = Time.time;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled)
        {
            _stateMachine.ChangeState(GoulStateEnum.Battle);//애니메이션 이벤트 넣어야됨
        }
    }
}
