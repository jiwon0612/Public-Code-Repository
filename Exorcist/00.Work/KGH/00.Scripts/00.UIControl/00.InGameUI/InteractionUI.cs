using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionUI : UIToolkitParents
{
    private Label _interactionLabel;
    protected override void OnEnable()
    {
        base.OnEnable();
        _interactionLabel = Root.Q<Label>("InteractionLabel");
    }

    public void ShowLabel()=>_interactionLabel.AddToClassList("show");
    public void HideLabel()=>_interactionLabel.RemoveFromClassList("show");
}
