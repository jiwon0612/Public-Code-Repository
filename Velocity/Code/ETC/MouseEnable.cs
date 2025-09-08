using System;
using Code.Core.EventSystem;
using UnityEngine;

namespace Code.ETC
{
    public class MouseEnable : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO uiChannel;

        private void Start()
        {
            uiChannel.RaiseEvent(UIEvents.CurserEnable.Init(true));
        }
    }
}