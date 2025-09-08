using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeManager : Action
{
    public SharedEnemy enemy;
    public bool active;

    public override void OnStart()
    {
        if (active)
        {
            enemy.Value.spriteRender.DOFade(1, 1);
        }
        else
        {
            enemy.Value.spriteRender.DOFade(0, 0);
        }
    }
}
