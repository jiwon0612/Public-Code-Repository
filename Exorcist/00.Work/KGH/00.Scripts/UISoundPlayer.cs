using System.Collections.Generic;
using System.Linq;
using Hellmade.Sound;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class UISoundPlayer : Sirenix.OdinInspector.SerializedMonoBehaviour
{
    [SerializeField] private UISoundSO soundSO;
    [OdinSerialize] private Dictionary<string, AudioClip> exceptions;

    private void OnEnable()
    {
        var uiDocs = GetComponentsInChildren<UIDocument>().ToList();
        foreach (var uiDoc in uiDocs)
        {
            var root = uiDoc.rootVisualElement;
            var buttons = root.Query<Button>().ToList();
            foreach (var button in buttons)
            {
                if (exceptions.ContainsKey(button.name))
                {
                    button.RegisterCallback<ClickEvent>(evt => EazySoundManager.PlayUISound(exceptions[button.name]));
                    continue;
                }
                button.RegisterCallback<MouseEnterEvent>(evt => EazySoundManager.PlayUISound(soundSO.hover));
                button.RegisterCallback<ClickEvent>(evt => EazySoundManager.PlayUISound(soundSO.click));
            }
        }
    }
}