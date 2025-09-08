using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FillSlider : MonoBehaviour
{
    private VisualElement _root;
    private List<VisualElement> _draggers;
    // private List<VisualElement> _bars;
    // private List<VisualElement> _newDraggers;

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _draggers = _root.Query<VisualElement>("unity-dragger").ToList();
        // _bars = new List<VisualElement>();
        // _newDraggers = new List<VisualElement>();

        AddElements();
    }

    void AddElements()
    {
        foreach (var dragger in _draggers)
        {
            VisualElement bar = new VisualElement();
            // _bars.Add(bar);
            dragger.Add(bar);
            bar.name = "Bar";
            bar.AddToClassList("bar");
            
            VisualElement newDragger = new VisualElement();
            // _newDraggers.Add(newDragger);
            dragger.Add(newDragger);
            newDragger.name = "NewDragger";
            newDragger.AddToClassList("newDragger");
            // newDragger.pickingMode = PickingMode.Ignore;
        }
    }
}
