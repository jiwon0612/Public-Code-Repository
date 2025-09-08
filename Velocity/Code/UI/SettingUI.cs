using System;
using Code.Core.EventSystem;
using Code.Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI
{
    public class SettingUI : MonoBehaviour
    {
        public UnityEvent OnDisableEvent;

        [SerializeField] private GameEventChannelSO uiChannel;
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private PlayerInputSO input;
        
        private CanvasGroup _canvas;

        private bool _isShowing;
        
        private void Awake()
        {
            _canvas = GetComponent<CanvasGroup>();
            _isShowing = false;
            SetEnableUI(false);
            input.OnShowSettingEvent += HandleShowSettingEvent;
        }

        private void OnDestroy()
        {
            input.OnShowSettingEvent -= HandleShowSettingEvent;
        }

        private void HandleShowSettingEvent()
        {
            _isShowing = !_isShowing;
            SetEnableUI(_isShowing);
        }

        private void SetEnableUI(bool isEnable)
        {
            if (isEnable)
            {
                Time.timeScale = 0;
                _canvas.alpha = 1;
                _canvas.interactable = true;
                _canvas.blocksRaycasts = true;
                uiChannel.RaiseEvent(UIEvents.CurserEnable.Init(true));
            }
            else
            {
                uiChannel.RaiseEvent(UIEvents.CurserEnable.Init(false));
                OnDisableEvent?.Invoke();
                Time.timeScale = 1;
                _canvas.alpha = 0;
                _canvas.interactable = false;
                _canvas.blocksRaycasts = false;
            }
        }

        public void ContinueClick()
        {
            _isShowing = !_isShowing;
            SetEnableUI(_isShowing);
        }

        public void ExitClick()
        {
            Time.timeScale = 1;
            sceneChannel.RaiseEvent(SceneEvents.SceneChange.Init(0));
        }
    }
}