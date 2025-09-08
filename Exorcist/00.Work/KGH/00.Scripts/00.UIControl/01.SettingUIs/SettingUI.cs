using System;
using System.Collections.Generic;
using Hellmade.Sound;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class SettingUI : UIToolkitParents
{
    [SerializeField] private InputReader playerInputReader;

    // [SerializeField] UISoundSO uiSoundSO;
    [SerializeField] private UIInputReader uiInputReader;
    [SerializeField] private bool isTitle = false;

    [ShowIf("isTitle", true)] [SerializeField]
    private LoadFileUI loadFileUI;

    [ShowIf("isTitle", true)] [SerializeField]
    private TitleUI titleUI;

    private VisualElement _background;

    private List<VisualElement> _elements;
    private List<Button> _buttons;

    private Button _backButton;

    public bool IsOpen = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        _background = Root.Q<VisualElement>("Contents");
        _backButton = Root.Q<Button>("BackButton");


        _backButton.RegisterCallback<ClickEvent>((ClickEvent clickEvent) => SettingClose());

        uiInputReader.OnUIOpenClose += UIOpenCloseHandler;

        playerInputReader.SetEnable(!isTitle);
    }

    private void OnDisable()
    {
        uiInputReader.OnUIOpenClose -= UIOpenCloseHandler;
    }

    void UIOpenCloseHandler()
    {
        if (loadFileUI != null && loadFileUI.IsOpen)
            return;

        if (IsOpen)
        {
            SettingClose();
        }
        else
        {
            SettingOpen(isTitle);
        }
    }

    public void SettingOpen(bool isTitle)
    {
        // EazySoundManager.PlayUISound(uiSoundSO.openUI);
        if (!isTitle)
        {
            playerInputReader.SetEnable(false);
        }

        IsOpen = true;
        _background.AddToClassList("on");

        _elements = Root.Query<VisualElement>().ToList();
        _buttons = Root.Query<Button>().ToList();

        foreach (var item in _elements) item.pickingMode = PickingMode.Position;
        foreach (var item in _buttons)
        {
            item.pickingMode = PickingMode.Position;
            // item.RegisterCallback<MouseOverEvent>((e)=>EazySoundManager.PlayUISound(uiSoundSO.hover));
        }

        if (isTitle)
        {
            Root.Q<Button>("ToTitleButton").pickingMode = PickingMode.Ignore;
            titleUI.TitleOpen(false);
        }
    }

    void SettingClose()
    {
        // EazySoundManager.PlayUISound(uiSoundSO.closeUI);
        if (!isTitle)
        {
            playerInputReader.SetEnable(true);
        }
        
        IsOpen = false;
        _background.RemoveFromClassList("on");

        foreach (var item in _elements) item.pickingMode = PickingMode.Ignore;
        foreach (var item in _buttons) item.pickingMode = PickingMode.Ignore;
        foreach (var slider in Root.Query<Slider>("VolumeSlider").ToList()) slider.pickingMode = PickingMode.Ignore;

        if (isTitle)
        {
            titleUI.TitleOpen(true);
        }
    }
}