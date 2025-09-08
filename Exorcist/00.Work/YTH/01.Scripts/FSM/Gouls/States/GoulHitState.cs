using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulHitState : EnemyState<GoulStateEnum>
{
    private readonly int _hitClipNameHash = Animator.StringToHash("hit");
    public GoulHitState(Enemy enemyBase, EnemyStateMachine<GoulStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemyBase.AnimatorCompo.Play(_hitClipNameHash, layer: -1, 0);
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_triggerCalled)
        {
            _stateMachine.ChangeState(GoulStateEnum.Battle);        }
        }
}
