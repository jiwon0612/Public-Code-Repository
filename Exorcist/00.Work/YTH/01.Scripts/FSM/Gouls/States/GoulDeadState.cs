using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulDeadState : EnemyState<GoulStateEnum>
{
    public GoulDeadState(Enemy enemyBase, EnemyStateMachine<GoulStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.isDead = true;
        _enemyBase.FreezeTime(false);
        _enemyBase.StartDelayCallback(3f, () =>
        {
            _rigidbody.gravityScale = 0;
            _enemyBase.ColliderCompo.enabled = false;
            //페이드 
        });
    }
}
