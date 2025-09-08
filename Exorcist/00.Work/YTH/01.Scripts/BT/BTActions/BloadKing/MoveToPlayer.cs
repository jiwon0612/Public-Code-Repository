using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveToPlayer : Action
{
    public SharedEnemy enemy;

    private Vector2 _playerPos;
    private Vector2 _enemyPos;

    public float speed = 2f;
    public override void OnStart()
    {

    }


    public override TaskStatus OnUpdate()
    {
        _playerPos = enemy.Value.Player.transform.position;//(0,0)
        _enemyPos = enemy.Value.transform.position;//(10,10)

        if (_playerPos.x < _enemyPos.x)
        {
            enemy.Value.transform.localScale = new Vector3(-1, 1, 1);
        }
        else 
        {
            enemy.Value.transform.localScale = new Vector3(1, 1, 1);
        }

        transform.position = Vector2.MoveTowards(_enemyPos, _playerPos, speed * Time.deltaTime);
        return TaskStatus.Running;
    }

}
