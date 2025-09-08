using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TitleUI : UIToolkitParents
{
    [SerializeField] UISoundSO uiSoundSO;
    [SerializeField] private TransitionUI transitionUI;
    [SerializeField] private SettingUI settingUi;
    [SerializeField] private LoadFileUI loadFileUI;
    private Button _startButton;
    private Button _settingButton;
    private Button _exitButton;

    private VisualElement _container;
    protected override void OnEnable()
    {
        base.OnEnable();
        
        _startButton = Root.Q<Button>("StartButton");
        _settingButton = Root.Q<Button>("SettingButton");
        _exitButton = Root.Q<Button>("ExitButton");

        _startButton.RegisterCallback<ClickEvent>(StartButtonClicked);
        _settingButton.RegisterCallback<ClickEvent>(SettingButtonClicked);
        _exitButton.RegisterCallback<ClickEvent>(ExitButtonClicked);

        // foreach (var item in Root.Query<Button>().ToList())
        // {
        //     item.RegisterCallback<MouseOverEvent>((e)=> EazySoundManager.PlayUISound(uiSoundSO.hover));
        // }
        _container = Root.Q<VisualElement>("Container");
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(StartButtonClicked);
        _settingButton.UnregisterCallback<ClickEvent>(SettingButtonClicked);
        _exitButton.UnregisterCallback<ClickEvent>(ExitButtonClicked);
    }

    void StartButtonClicked(ClickEvent clickEvent)
    {
        TitleOpen(false);
        loadFileUI.EnableUI();
    }

    void SettingButtonClicked(ClickEvent clickEvent)
    {
        settingUi.SettingOpen(true);
    }

    void ExitButtonClicked(ClickEvent clickEvent)
    {
        transitionUI.TransitionStart(Application.Quit);
    }
    
    public void TitleOpen(bool isOpen)
    {
        Debug.Log("TitleOpen");
        if (isOpen)
        {
            _container.RemoveFromClassList("hide");
        }
        else
        {
            _container.AddToClassList("hide");
        }
    }
}