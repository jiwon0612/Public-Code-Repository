using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDistance : Conditional
{
    public SharedEnemy enemy;

    public override TaskStatus OnUpdate()
    {
        if(Vector2.Distance(enemy.Value.Player.transform.position, enemy.Value.transform.position) < 1f)
        {
            return TaskStatus.Failure;
        }
            return TaskStatus.Success;
    }
}
