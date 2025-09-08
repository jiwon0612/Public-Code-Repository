using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;

public class BloodKingDeadState : BloodKingState
{
    public BloodKingDeadState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _bloodKing.SetActiveCollider(false);
        EazySoundManager.PlaySound(_bloodKing.sounds[4]);
        _bloodKing.BloodKingAnimationComp.DeadCamImpuse(() => _bloodKing.OnDeadEvent?.Invoke());
    }
}
