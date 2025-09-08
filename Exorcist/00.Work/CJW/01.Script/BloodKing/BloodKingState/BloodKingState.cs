using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingState : EnemyState<BloodKingStateEnum>
{
    public BloodKing _bloodKing;
    protected int _triggerCount;
    
    public BloodKingState(BloodKing enemyBase, EnemyStateMachine<BloodKingStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _bloodKing = enemyBase;
    }
}
