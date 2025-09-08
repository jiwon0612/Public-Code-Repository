using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

public abstract class UIToolkitParents : MonoBehaviour
{
    private UIDocument _uiDocument;
    protected VisualElement  Root;


    protected virtual void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();
        Root = _uiDocument.rootVisualElement;
    }
}
