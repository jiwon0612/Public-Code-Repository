using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulMoveState : EnemyState<GoulStateEnum>
{
    public GoulMoveState(Enemy enemyBase, EnemyStateMachine<GoulStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemyBase.SetVelocity(_enemyBase.moveSpeed * _enemyBase.FacingDirection, _rigidbody.velocity.y);

        if (_enemyBase.IsWallDetected() || !_enemyBase.IsGroundDetected())
        {
            //_stateMachine.ChangeState(GoulStateEnum.); 아이들ㄷ이나 갈데로 간다.
        }
    }
}
