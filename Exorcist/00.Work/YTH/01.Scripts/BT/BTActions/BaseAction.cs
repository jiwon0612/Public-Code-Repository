using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class BaseAction : Action
{
    public SharedEnemy enemy;
    public SharedString animationName;

    public override void OnStart()
    {
        enemy.Value.OffAnimationTrigger();
        enemy.Value.AnimatorCompo.SetBool(animationName.Value, true);
    }

    public override TaskStatus OnUpdate()
    {
        if (enemy.Value._triggerCalled == true)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        enemy.Value.OffAnimationTrigger();
        enemy.Value.AnimatorCompo.SetBool(animationName.Value, false);
    }
}
