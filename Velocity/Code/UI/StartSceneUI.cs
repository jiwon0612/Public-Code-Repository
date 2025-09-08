using System;
using Code.Core.EventSystem;
using DG.Tweening;
using UnityEngine;

namespace Code.UI
{
    public class StartSceneUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private SoundUI soundUI;
        [SerializeField] private Transform soundUIMovePoint;
        [SerializeField] private float soundUIMoveTime;

        private Tween _soundUIMoveTween;
        private Vector3 _soundUIEnablePoint;
        
        private bool _isSoundUIEnable;

        private void Awake()
        {
            _soundUIEnablePoint = soundUI.transform.position;
            soundUI.transform.position = soundUIMovePoint.position;
            _isSoundUIEnable = false;
        }

        private void Start()
        {
            soundUI.LoadSound();
            soundUI.SaveSound();
        }

        public void StartClick()
        {
            sceneChannel.RaiseEvent(SceneEvents.SceneChange.Init(1));
        }

        public void SettingClick()
        {
            if (_isSoundUIEnable)
            {
                _isSoundUIEnable = false;
                if (_soundUIMoveTween.IsActive())
                    _soundUIMoveTween.Kill();
                _soundUIMoveTween = soundUI.transform.DOMove(soundUIMovePoint.position, soundUIMoveTime);
            }
            else
            {
                _isSoundUIEnable = true;
                if (_soundUIMoveTween.IsActive())
                    _soundUIMoveTween.Kill();
                _soundUIMoveTween = soundUI.transform.DOMove(_soundUIEnablePoint, soundUIMoveTime);
            }
        }

        public void QuitClick()
        {
            Debug.Log("나가기");
            Application.Quit();
        }
    }
}