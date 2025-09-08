using UnityEngine;

public class BloodKingAttackReadyState : BloodKingChaseState
{
    public BloodKingAttackReadyState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.StopImmediately(false);
    }

    public override void UpdateState()
    {
        Vector2 dir = _bloodKing.targetTrm.position - _bloodKing.transform.position;
        float distance = dir.magnitude;
        
        if (CheckChageToAttack(distance)) return;
        
        if (CheckChageToLoogRangeAttack(distance)) return;
        
        if (CheckChageToidle(distance)) return;
    }

    private bool CheckChageToidle(float distance)
    {
        if (distance > _bloodKing.attackDistance + 2)
        {
            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
            return true;
        }

        return false;
    }
}
