using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TransitionUI : UIToolkitParents
{
    [SerializeField] private float transitionDuration;

    private List<VisualElement> _parts;

    protected override void OnEnable()
    {
        base.OnEnable();
        _parts = new List<VisualElement>();
        
        _parts.Add(Root.Q<VisualElement>("Black"));
        _parts.Add(Root.Q<VisualElement>("Gradient"));
    }

    private void Start()
    {
        foreach (var part in _parts)
        {
            part.AddToClassList("end");
        }

        StartCoroutine(FadeOut());
    }

    public void TransitionStart(Action callback)
    {
        StartCoroutine(FadeIn(callback));
    }
    
    IEnumerator FadeIn(Action callback)
    {
        foreach (var part in _parts)
        {
            part.AddToClassList("middle");
        }
        yield return new WaitForSeconds(transitionDuration);
        foreach (var part in _parts)
        {
            part.RemoveFromClassList("middle");
            part.AddToClassList("end");
        }
        yield return new WaitForSeconds(transitionDuration);
        Debug.Log("Transition End");
        callback?.Invoke();
    }

    IEnumerator FadeOut()
    {
        yield return null;
        foreach (var part in _parts)
        {
            part.RemoveFromClassList("end");
            part.AddToClassList("middle");
        }
        yield return new WaitForSeconds(transitionDuration);
        foreach (var part in _parts)
        {
            part.RemoveFromClassList("middle");
        }
    }
}