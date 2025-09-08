using System;
using Code.Core.EventSystem;
using UnityEngine;

namespace Code.ETC
{
    public class BoosRing : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private float speed;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                playerChannel.RaiseEvent(PlayerEvents.Boost.Init(speed));
            }
        }
    }
}