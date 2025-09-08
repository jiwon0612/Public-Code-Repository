using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blink : MonoBehaviour
{
    public UnityEvent TimeLineStart;
    [SerializeField] private float _blinkDelay;
    private AudioSource _source;

    private SpriteRenderer _targetRanderer;
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;
    private void Awake()
    {
        _targetRanderer = GetComponent<SpriteRenderer>();
        _source = GetComponent<AudioSource>();
    }

    private void Start()
    {
    }

    public void StartBlink()
    {
        StartCoroutine(BlinkCoroutine());
    }

    public IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < 14; i++)
        {
            _targetRanderer.sprite = _onSprite;
            _source.Play();
            yield return new WaitForSeconds(_blinkDelay);
            _targetRanderer.sprite = _offSprite;
            yield return new WaitForSeconds((_blinkDelay *= 0.8f));
        }

        yield return new WaitForSeconds(1f);
        TimeLineStart?.Invoke();
    }
}
