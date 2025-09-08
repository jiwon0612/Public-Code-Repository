using System;
using Code.Core.EventSystem;
using UnityEngine;

namespace Code.Core.Managers
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private GameEventChannelSO uiChannel;

        [SerializeField] private GameEventChannelSO[] events;

        private bool _isChangeScene;
        private int _nextScene;
        
        private void Awake()
        {
            _isChangeScene = false;
            sceneChannel.AddListener<SceneChangeEvent>(HandleSceneChangeEvent);
        }
        
        private void HandleSceneChangeEvent(SceneChangeEvent evt)
        {
            sceneChannel.RemoveListener<SceneChangeEvent>(HandleSceneChangeEvent);
            uiChannel.AddListener<FadeCompleteEvent>(HandleFadeCompleteEvent);
            _nextScene = evt.sceneIndex;
            _isChangeScene = true;
        }

        private void HandleFadeCompleteEvent(FadeCompleteEvent evt)
        {
            uiChannel.RemoveListener<FadeCompleteEvent>(HandleFadeCompleteEvent);

            foreach (var item in events)
            {
                item.Clear();
            }
            uiChannel.RaiseEvent(UIEvents.CurserEnable.Init(true));
            if (_isChangeScene)
                UnityEngine.SceneManagement.SceneManager.LoadScene(_nextScene);
        }

        private void Start()
        {
            sceneChannel.RaiseEvent(SceneEvents.SceneStart);
        }

        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.T))
        //         sceneChannel.RaiseEvent(SceneEvents.SceneStart);
        //     if (Input.GetKeyDown(KeyCode.Y))
        //         sceneChannel.RaiseEvent(SceneEvents.SceneChange.Init(1));
        // }
    }
}