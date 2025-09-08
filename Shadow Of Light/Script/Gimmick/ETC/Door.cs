using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : GimmickETC
{
    private SpriteRenderer sprite;
    private BoxCollider2D box;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }
    public override void OperationGimmick()
    {
        
        base.OperationGimmick();
        box.enabled = false;
        sprite.DOFade(0, 0.5f);
    }
}
