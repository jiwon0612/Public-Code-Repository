using System;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public struct VolumeValue
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
    }

    [DefaultExecutionOrder(-10)]
    public class SoundUI : MonoBehaviour
    {
        [field: SerializeField] public Slider MasterVolumeSlider { get; private set; }
        [field: SerializeField] public Slider MusicVolumeSlider { get; private set; }
        [field: SerializeField] public Slider SFXVolumeSlider { get; private set; }

        private VolumeValue _volumeValue;


        private void Awake()
        {
            MasterVolumeSlider.onValueChanged.RemoveAllListeners();
            MusicVolumeSlider.onValueChanged.RemoveAllListeners();
            SFXVolumeSlider.onValueChanged.RemoveAllListeners();
            
            LoadSound();
            MasterVolumeSlider.onValueChanged.AddListener(HandleMasterVolumeChange);
            MusicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
            SFXVolumeSlider.onValueChanged.AddListener(HandleSFXVolumeChange);
        }

        public void LoadSound()
        {
            string jsonData = PlayerPrefs.GetString("VolumeValue");
            if (!string.IsNullOrEmpty(jsonData))
            {
                _volumeValue = JsonUtility.FromJson<VolumeValue>(jsonData);
                MasterVolumeSlider.value = _volumeValue.masterVolume;
                MusicVolumeSlider.value = _volumeValue.musicVolume;
                SFXVolumeSlider.value = _volumeValue.sfxVolume;

                EazySoundManager.GlobalVolume = _volumeValue.masterVolume;
                EazySoundManager.GlobalMusicVolume = _volumeValue.musicVolume;
                EazySoundManager.GlobalSoundsVolume = _volumeValue.sfxVolume;
                EazySoundManager.GlobalUISoundsVolume = _volumeValue.sfxVolume;
            }
            else
            {
                MasterVolumeSlider.value = 0.5f;
                MusicVolumeSlider.value = 0.5f;
                SFXVolumeSlider.value = 0.5f;
                _volumeValue = new VolumeValue();
                _volumeValue.masterVolume = 0.5f;
                _volumeValue.musicVolume = 0.5f;
                _volumeValue.sfxVolume = 0.5f;
                EazySoundManager.GlobalVolume = 0.5f;
                EazySoundManager.GlobalMusicVolume = 0.5f;
                EazySoundManager.GlobalSoundsVolume = 0.5f;
                EazySoundManager.GlobalUISoundsVolume = 0.5f;
            }
            
            SaveSound();
        }

        public void SaveSound()
        {
            string jsonData = JsonUtility.ToJson(_volumeValue);
            Debug.Log(jsonData);
            PlayerPrefs.SetString("VolumeValue", jsonData);
            PlayerPrefs.Save();
        }

        private void OnDestroy()
        {
            SaveSound();
            MasterVolumeSlider.onValueChanged.RemoveListener(HandleMasterVolumeChange);
            MusicVolumeSlider.onValueChanged.RemoveListener(HandleMusicVolumeChange);
            SFXVolumeSlider.onValueChanged.RemoveListener(HandleSFXVolumeChange);
        }

        private void HandleMasterVolumeChange(float value)
        {
            EazySoundManager.GlobalVolume = value;
            _volumeValue.masterVolume = value;
            SaveSound();
        }

        private void HandleMusicVolumeChange(float value)
        {
            EazySoundManager.GlobalMusicVolume = value;
            _volumeValue.musicVolume = value;
            SaveSound();
        }

        private void HandleSFXVolumeChange(float value)
        {
            EazySoundManager.GlobalSoundsVolume = value;
            EazySoundManager.GlobalUISoundsVolume = value;
            _volumeValue.sfxVolume = value;
            SaveSound();
        }
    }
}