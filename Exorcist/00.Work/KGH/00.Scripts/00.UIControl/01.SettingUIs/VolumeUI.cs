using System;
using System.Collections.Generic;
using System.Linq;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeUI : UIToolkitParents
{
    [SerializeField] private JsonManagerSO jsonManagerSO;

    private List<Slider> _volumeSliders;
    private List<Label> _valueLabels;

    private VolumeValues _volumeValues;

    private SettingUI settingUI;

    protected override void OnEnable()
    {
        base.OnEnable();

        settingUI = GetComponent<SettingUI>();

        _volumeSliders = Root.Query<Slider>("VolumeSlider").ToList();

        _valueLabels = new List<Label>();
        foreach (var slider in _volumeSliders)
        {
            Label sliderLabel = slider.parent.Children().ToList()[2] as Label;

            _valueLabels.Add(sliderLabel);

            int sliderNumber = _volumeSliders.IndexOf(slider);
            slider.RegisterCallback<ChangeEvent<float>>(changeEvent =>
                HandleSliderValueChange(100 - changeEvent.newValue, sliderNumber));
        }

        var defaultVolume = new VolumeValues()
        {
            BgmVolume = 1,
            MasterVolume = 1,
            SfxVolume = 1
        };

        _volumeValues = jsonManagerSO.LoadJson(PrefsKeyType.Volume, defaultVolume);

        _volumeSliders[0].value = (1 - _volumeValues.MasterVolume) * 100;
        _volumeSliders[1].value = (1 - _volumeValues.BgmVolume) * 100;
        _volumeSliders[2].value = (1 - _volumeValues.SfxVolume) * 100;

        settingUI.IsOpen = true;
        HandleSliderValueChange(_volumeValues.MasterVolume * 100, 0);
        HandleSliderValueChange(_volumeValues.BgmVolume * 100, 1);
        HandleSliderValueChange(_volumeValues.SfxVolume * 100, 2);
        settingUI.IsOpen = false;
    }

    private void HandleSliderValueChange(float newValue, int sliderNumber)
    {
        if (!settingUI.IsOpen) return;

        var label = _valueLabels[sliderNumber];
        
        var value = Mathf.RoundToInt(newValue);
        
        label.text = $"{value}%";
        switch (sliderNumber)
        {
            case 0:
                _volumeValues.MasterVolume = value / 100f;
                EazySoundManager.GlobalVolume = _volumeValues.MasterVolume;
                break;
            case 1:
                _volumeValues.BgmVolume = value / 100f;
                EazySoundManager.GlobalMusicVolume = _volumeValues.BgmVolume;
                break;
            case 2:
                _volumeValues.SfxVolume = value / 100f;
                EazySoundManager.GlobalSoundsVolume = _volumeValues.SfxVolume;
                EazySoundManager.GlobalUISoundsVolume = _volumeValues.SfxVolume;
                break;
        }

        jsonManagerSO.SaveJson(_volumeValues, PrefsKeyType.Volume);
    }
}

[Serializable]
public struct VolumeValues
{
    public float MasterVolume;
    public float BgmVolume;
    public float SfxVolume;
}