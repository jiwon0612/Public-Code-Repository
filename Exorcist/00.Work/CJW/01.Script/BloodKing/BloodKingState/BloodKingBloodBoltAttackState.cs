using UnityEngine;

public class BloodKingBloodBoltAttackState : BloodKingRangeAttackState
{
    private Transform _targetTrm;

    public BloodKingBloodBoltAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine,
        string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _triggerCount = 0;
        _targetTrm = null;
        _enemyBase.StopImmediately(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled)
        {
            _triggerCount++;
        }


        if (_triggerCount == 1)
        {
            Collider2D target = _bloodKing.GetPlayerInRange();
            if (target == null) _stateMachine.ChangeState(BloodKingStateEnum.Idle);
            
            _targetTrm = target.transform;

            _triggerCount++;
            _triggerCalled = false;
        }

        if (_triggerCount == 3)
        {
            if (_targetTrm != null)
                _bloodKing.UseBloodBolt(new Vector2(_targetTrm.position.x, _targetTrm.position.y - 1f));

            _triggerCount++;
            _triggerCalled = false;
        }

        if (_triggerCount == 5)
        {
            _triggerCalled = false;
            _triggerCount = 0;

            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
        }
    }
}