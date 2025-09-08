using System;
using Code.Core.EventSystem;
using UnityEngine;

namespace Code.Core.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO uiChannel;
        [SerializeField] private GameEventChannelSO sceneChannel;

        private void Awake()
        {
            uiChannel.AddListener<CurserEnableEvent>(HandleCurserEnableEvent);
            sceneChannel.AddListener<SceneChangeEvent>(HandleSceneChangeEvent);
        }

        private void OnDestroy()
        {
            uiChannel.RemoveListener<CurserEnableEvent>(HandleCurserEnableEvent);
            sceneChannel.RemoveListener<SceneChangeEvent>(HandleSceneChangeEvent);
        }

        private void HandleSceneChangeEvent(SceneChangeEvent evt)
        {
            if (0 == evt.sceneIndex && 1 == evt.sceneIndex)
            {
                Debug.Log("dd");
                uiChannel.RaiseEvent(UIEvents.CurserEnable.Init(true));
            }
            else
            {
                uiChannel.RaiseEvent(UIEvents.CurserEnable.Init(false));
            }
        }

        private void HandleCurserEnableEvent(CurserEnableEvent evt)
        {
            if (evt.enable)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}