using UnityEngine;

public class BloodKingIdleState : BloodKingState
{
    public BloodKingIdleState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _bloodKing.StopImmediately(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Collider2D target = _bloodKing.GetPlayerInRange();
        if (target != null)
        {
            _bloodKing.targetTrm = target.transform;            
            _stateMachine.ChangeState(BloodKingStateEnum.Chase);
        }
    }
}
