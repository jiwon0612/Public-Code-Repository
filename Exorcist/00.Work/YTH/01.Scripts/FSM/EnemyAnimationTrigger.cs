using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    protected Enemy _enemy;

    protected virtual void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    private void AnimationFinishTrigger()
    {
        _enemy.OnAnimationTrigger();
    }

    private void AttackAnimationTrigger()
    {
        _enemy.Attack();
    }
}
