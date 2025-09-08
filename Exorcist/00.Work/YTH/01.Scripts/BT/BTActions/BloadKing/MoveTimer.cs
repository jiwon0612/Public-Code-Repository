using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTimer : Action
{
    public int moveTime;
    private float _currentTime;

    public override TaskStatus OnUpdate()
    {
        if(_currentTime <= moveTime)
        {
            _currentTime += Time.deltaTime;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
