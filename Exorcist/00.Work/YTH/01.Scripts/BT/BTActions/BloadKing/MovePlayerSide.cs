using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerSide : Action
{
    public SharedEnemy enemy;
    public float minDistance = 0.1f;
    public float maxDistance = 1f;

    public override void OnStart()
    {
        Teleport();
    }

    private void Teleport()
    {
        Vector2 playerPos = enemy.Value.Player.transform.position;

        int direction = Random.Range(0,2) == 0 ? 1: -1;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector2 movePos = new Vector2(playerPos.x + (direction * randomDistance), transform.position.y);

        transform.position = movePos;   
    }
}
