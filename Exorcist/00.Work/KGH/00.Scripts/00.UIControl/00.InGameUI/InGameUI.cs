using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : UIToolkitParents
{
    private VisualElement _contents;
    protected override void OnEnable()
    {
        base.OnEnable();
        _contents = Root.Q<VisualElement>("Contents");
    }
    public void HideUI()
    {
        _contents.AddToClassList("hide");
    }
    public void ShowUI()
    {
        _contents.RemoveFromClassList("hide");
    }
}
