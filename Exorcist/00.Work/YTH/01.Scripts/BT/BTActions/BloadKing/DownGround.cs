using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGround : Action
{
    public SharedEnemy enemy;

    public override void OnStart()
    {
        enemy.Value.DownGround();
    }
}
