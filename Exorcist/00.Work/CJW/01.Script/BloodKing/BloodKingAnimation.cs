using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class BloodKingAnimation : MonoBehaviour
{
    public BloodKing BloodKingComp { get; private set; }
    public SpriteRenderer SpriteRendererComp { get; private set; }
    
    private Material _material;

    private readonly int _blinkHash = Shader.PropertyToID("_IsBlink");

    private ParticleSystem _dissolveParticle;
    private CinemachineImpulseSource _impulse;

    private void Awake()
    {
        BloodKingComp = transform.parent.GetComponent<BloodKing>();
        SpriteRendererComp = GetComponent<SpriteRenderer>();
        _impulse = GetComponent<CinemachineImpulseSource>();
        _material = SpriteRendererComp.material;
        _dissolveParticle = BloodKingComp.transform.Find("DessolveParticle").GetComponent<ParticleSystem>();
    }

    public void Blink(bool isBlink)
    {
        int num = Convert.ToInt32(isBlink);
        //Debug.Log(num);
        _material.SetInt(_blinkHash,num);
    }

    public void DeadCamImpuse(Action callback)
    {
        _impulse.GenerateImpulse();
        DOVirtual.DelayedCall(0.8f,() => callback?.Invoke());
    }
}
