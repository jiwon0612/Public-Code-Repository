using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodKingDead : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dissolveParticle;
    private Material _bloodKingMat;
    private int _dissolveHash = Shader.PropertyToID("_DissolveValue");
    [SerializeField] private float _dissolveSpeed = 1;
    [SerializeField] private float _dissolveDelay = 0.01f;
    public UnityEvent BossRoomClear;


    private void Awake()
    {
        _bloodKingMat = transform.Find("Visual").GetComponent<SpriteRenderer>().material;
    }

    public void StartDissolve()
    {
        StartCoroutine(DissolveCorouitine());
    }

    private IEnumerator DissolveCorouitine()
    {
        float dissolveValue = 1;
        yield return new WaitForSeconds(2f);
        _dissolveParticle.Play();
        while (dissolveValue > 0)
        {
            dissolveValue -= 0.01f * _dissolveSpeed;
            _bloodKingMat.SetFloat(_dissolveHash,dissolveValue);
            yield return new WaitForSeconds(_dissolveDelay);
        }

        yield return new WaitForSeconds(1f);
        BossRoomClear?.Invoke();
        
    }
}
