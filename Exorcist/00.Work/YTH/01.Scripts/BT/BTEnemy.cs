using BehaviorDesigner.Runtime;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTEnemy : Enemy
{
    [Space]
    [Header("BTSetting")]
    public GameObject Player;
    [SerializeField] private LayerMask _groundLayer;
     public Transform OriginPos;


    public bool _triggerCalled { get; private set; }
    public BehaviorTree BT;
    public SpriteRenderer spriteRender { get; private set; }
    public Transform FootPos { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        BT = GetComponent<BehaviorTree>();
        spriteRender = transform.GetChild(0).GetComponent<SpriteRenderer>();
        FootPos = transform.GetChild(1).transform;
    }



    //public override void Stun(float time)
    //{
    //    if (isDead) return;
    //    stunDuration = time;
    //    //스턴상태로 가도록
    //}

    #region AnimationManager
    public override void OnAnimationTrigger()//이건 애니메이터에서
    {
        _triggerCalled = true;
    }
    public void OffAnimationTrigger()
    {
        _triggerCalled = false;
    }
    public void FadeOn()
    {
        spriteRender.DOFade(0, 0);
    }
    public void FadeOff()
    {
        spriteRender.DOFade(1, 0);
    }
    public void DownGround()
    {
        Vector3 footPos = FootPos.position;
        RaycastHit2D hit = Physics2D.Raycast(footPos, Vector2.down, 30f, _groundLayer);
        if (hit.collider)
        {
            float distance = hit.distance;
            //if(distance < 1)
            //{
            //    return;
            //}
            Vector3 pos = transform.position;
            pos.y = transform.position.y - distance;
            transform.position = pos;
        }
    }
    #endregion

}

//public class SharedEnemy : SharedVariable<BTEnemy>
//{
//    public static implicit operator SharedEnemy(BTEnemy value)
//    {
//        return new SharedEnemy { Value = value };
//    }
//}


