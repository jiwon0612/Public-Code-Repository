using System;
using Code.Core.EventSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Code.ETC
{
    public class Portal : MonoBehaviour
    {
        public UnityEvent OnStageClear;
        [SerializeField] private GameEventChannelSO sceneChannel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                OnStageClear?.Invoke();
                PlayerPrefs.SetInt($"ClearScene{SceneManager.GetActiveScene().buildIndex - 1}", 1);
                sceneChannel.RaiseEvent(SceneEvents.SceneChange.Init(1));
            }
        }
    }
}