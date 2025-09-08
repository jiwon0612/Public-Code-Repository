using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOrigin : Action
{
    public SharedEnemy enemy;
    public float xOffset = 0;
    public override void OnStart()
    {
        Vector2 pos = enemy.Value.OriginPos.position;
        pos.x += xOffset;
        transform.position = pos;
    }
}
