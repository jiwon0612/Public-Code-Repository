using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    private List<SpriteRenderer> spriteRs = new List<SpriteRenderer>();
    [SerializeField] private float radius;

    [SerializeField] private Color32 sColor;
    [SerializeField] private Color32 eColor;

    private bool isInInterRadius;
    private bool isCanInteraction;
    [SerializeField] private LayerMask target;

    private void Awake()
    {

    }

    private void Start()
    {
        foreach (Transform item in transform)
        {
            if (item.TryGetComponent(out SpriteRenderer sprite))
            {
                sprite.color = eColor;
                spriteRs.Add(sprite);

            }
        }

    }

    private void Update()
    {
        ListFade();
    }

    private void ListFade()
    {
        isInInterRadius = Physics2D.OverlapCircle(transform.position, radius, target);

        if (isInInterRadius)
        {
            if (!isCanInteraction)
            {
                isCanInteraction = true;
                foreach (SpriteRenderer item in spriteRs)
                {
                    item.DOColor(sColor, 0.5f);
                }
            }
        }
        if (!isInInterRadius)
        {
            if (isCanInteraction)
            {
                isCanInteraction = false;
                foreach (SpriteRenderer item in spriteRs)
                {
                    item.DOColor(eColor, 0.5f);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.white;
    }
}
