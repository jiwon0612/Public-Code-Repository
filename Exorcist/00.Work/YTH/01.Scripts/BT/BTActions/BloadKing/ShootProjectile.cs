using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootProjectile : Action
{
    public SharedEnemy enemy;

    public override void OnStart()
    {
        enemy.Value.Shooting(30);
    }
}
