using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss1Dissolve : MonoBehaviour
{
    [FormerlySerializedAs("_dessolveParticle")] [SerializeField] private ParticleSystem _dissolveParticle;
    private Material _targetMaterial;
    private int _dissolveValue;
    private float dissolveValue = 1;

    private void Awake()
    {
        _targetMaterial = GetComponent<SpriteRenderer>().material;
        _dissolveValue = Shader.PropertyToID("_DissolveValue");
    }

    public void StartDissolve()
    {
        _dissolveParticle.Play();
        StartCoroutine(DissolveCoroutine());
    }

    private IEnumerator DissolveCoroutine()
    {
        while (dissolveValue > 0)
        {
            dissolveValue = dissolveValue - 0.05f;
            _targetMaterial.SetFloat(_dissolveValue,dissolveValue);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
