using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingTeleponrtJumpAttackState : BloodKingAttackState
{
    public BloodKingTeleponrtJumpAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
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
            _triggerCount += 1;
        }

        if (_triggerCount == 1)
        {
            _bloodKing.SetActiveCollider(false);
            _triggerCalled = false;
        }

        if (_triggerCount == 2 && _triggerCalled)
        {
            Collider2D target = _bloodKing.GetPlayerInRange();
            if (target != null)
                _bloodKing.transform.position = target.transform.position;
            //_bloodKing.transform.position = new Vector3(target.transform.position.x, _bloodKing.transform.position.y
            //    , _bloodKing.transform.position.z);

            _triggerCalled = false;
        }
        
        if (_triggerCount == 3)
        {
            _triggerCount++;
            _bloodKing.CamImpulse(new Vector3(0,-0.5f,0));
            _bloodKing.SetActiveCollider(true);
            _bloodKing.groundParticles.Play();
            _triggerCalled = false;
        }

        if (_triggerCount == 5)
        {
            _stateMachine.ChangeState(BloodKingStateEnum.Idle);
            _triggerCalled = false;
        }
    }

}
