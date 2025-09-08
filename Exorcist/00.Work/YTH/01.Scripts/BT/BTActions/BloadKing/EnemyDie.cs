using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : Conditional
{
    public SharedEnemy enemy;

    public override TaskStatus OnUpdate()
    {
        if (!enemy.Value.isDead)
        {
            return TaskStatus.Failure;
        }
        return TaskStatus.Success;
    }
}
