using UnityEngine;

public class BloodKingChaseState : BloodKingState
{
    public BloodKingChaseState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine,
        string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
       
        Vector2 dir = _bloodKing.targetTrm.position - _bloodKing.transform.position;
        float distance = dir.magnitude;

        if (CheckChangeToIdle(distance)) return;

        _enemyBase.SetVelocity(Mathf.Sign(dir.x) * _enemyBase.moveSpeed, 0);

        if (CheckChageToAttack(distance)) return;
        
        if (CheckChageToLoogRangeAttack(distance)) return;
        
        if (CheckChageToAtkReady(distance)) return;        
    }

    private bool CheckChageToAtkReady(float distance)
    {
        if (distance < _bloodKing.attackDistance - 0.5f)
        {
            _stateMachine.ChangeState(BloodKingStateEnum.AttackReady);
            return true;
        }
        return false;
    }

    private bool CheckChangeToIdle(float distance)
    {
        if (distance > _bloodKing._detectRange + 2)
        {
            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
            return true;
        }

        return false;
    }

    protected bool CheckChageToAttack(float distance)
    {
        if (!_bloodKing.IsCanAttack) return false;
        
        if (distance < _enemyBase.attackDistance)
        {
            BloodKingStateEnum type = _bloodKing.GetRandomAttackState();
            
            if (type == BloodKingStateEnum.Idle) return false;
            
            _stateMachine.ChangeState(type);
            return true;
        }
        return false;
    }

    protected bool CheckChageToLoogRangeAttack(float distance)
    {
        if (!_bloodKing.IsCanRangeAttack) return false;
        
        if (distance < _bloodKing._rangeAttakRange)
        {
            BloodKingStateEnum type = _bloodKing.GetRandomLoogRangeAtkState();
            
            if (type == BloodKingStateEnum.Idle) return false;
            
            _stateMachine.ChangeState(type);
            return true;
        }
        return false;
    }
}