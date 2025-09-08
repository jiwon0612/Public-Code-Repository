using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAction : Action
{
    public SharedEnemy enemy;
    float time;

    public override void OnStart()
    {
        time = 0;
        enemy.Value.spriteRender.DOFade(0, 1f);
        EazySoundManager.PlaySound(enemy.Value.DeadSfx);
    }

    public override TaskStatus OnUpdate()
    {

        time += Time.deltaTime;

        if(time < 1) {
            return TaskStatus.Running;
        }
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        SoundManager.Instance.GameClearSFX();
        enemy.Value.Portal.OpenPotalToRobby();
        enemy.Value.BT.DisableBehavior();
    }
}
