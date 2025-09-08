using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class FlashlightTool : Tool
{
    public float distance;
    [SerializeField] private float _alpha;
    [SerializeField] private Gradient _sads;

    public LayerMask fliter;

    public LineRenderer lineRenderer;

    private Tween tween;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        //if(isChangeAlpha)
        //    lineRenderer.sharedMaterial.SetFloat("_Alpha", _alpha);
    }
    public override void Fire()
    {
        base.Fire();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, fliter);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out Mirror mirror) == true)
            {
                mirror.Reflecting(hit, dir, distance, fliter);
            }

            if (hit.transform.TryGetComponent(out Sensor sensor) == true)
            {
                sensor.StartGimmick();
            }

        }

        StartCoroutine(DrawLine(hit));

    }


    private IEnumerator DrawLine(RaycastHit2D hit)
    {
        lineRenderer.material.DOKill();
        lineRenderer.SetPosition(0, transform.position);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + dir * distance);
        }

        lineRenderer.enabled = true;

        Gradient c = lineRenderer.colorGradient;
        GradientAlphaKey[] alphaKeys = c.alphaKeys;
        alphaKeys[0].alpha = 1;
        alphaKeys[1].alpha = 1;
        c.alphaKeys = alphaKeys;
        lineRenderer.colorGradient = c;
        
        DOTween.To(() => alphaKeys[0].alpha, x => alphaKeys[0].alpha = x, 0, 0.5f).OnUpdate(() =>
        {
            c.alphaKeys = alphaKeys;
            lineRenderer.colorGradient = c;
        });
        DOTween.To(() => alphaKeys[0].alpha, x => alphaKeys[1].alpha = x, 0, 0.5f).OnUpdate(() =>
        {
            c.alphaKeys = alphaKeys;
            lineRenderer.colorGradient = c;
        });

        yield return new WaitForSeconds(0.5f);

        lineRenderer.enabled = false;
        
    }

}
