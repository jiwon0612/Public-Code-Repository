using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingAttackState : BloodKingState
{
    public BloodKingAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }
}
