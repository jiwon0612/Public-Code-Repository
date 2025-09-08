using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mirror : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Coroutine lineCoroutine;

    private BoxCollider2D box;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
        box.enabled = true;

    }

    public void Reflecting(RaycastHit2D rayHit , Vector2 dir, float distance, LayerMask mask)
    {
        box.enabled = false;

        //Vector2 incidenceVector = pointDir - posDir;
        //float incidenceAngle = Mathf.Atan2(incidenceVector.y, incidenceVector.x) * Mathf.Rad2Deg;
        //Debug.Log($"{incidenceAngle} 입사각");
        //float reflectAngle = 180 - incidenceAngle;
        //Debug.Log($"{reflectAngle} 반사각");
        //Vector2 reflectVector = new Vector2(Mathf.Cos(reflectAngle), Mathf.Sin(reflectAngle));
        //Vector2 reflectVector = new Vector2(Mathf.Cos(incidenceAngle), Mathf.Sin(incidenceAngle));

        Vector2 reflectVector = Vector2.Reflect(dir, rayHit.normal).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, reflectVector, distance, mask);

        if (hit)
        {
            if (hit.transform.TryGetComponent(out Mirror mirror) == true)
            {
                mirror.Reflecting(hit, reflectVector, distance, mask);
            }

            if (hit.transform.TryGetComponent(out Sensor sensor) == true)
            {
                sensor.StartGimmick();
            }

        }
        box.enabled = true;
        StartCoroutine(DrawLine(hit, reflectVector, distance, rayHit.point));

    }

    private IEnumerator DrawLine(RaycastHit2D hit , Vector2 dir, float distance, Vector2 pointDir)
    {
        lineRenderer.SetPosition(0, pointDir);
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, (pointDir + dir) * distance);
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
