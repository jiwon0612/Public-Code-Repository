using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingRangeAttackState : BloodKingState
{
    public BloodKingRangeAttackState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

}
