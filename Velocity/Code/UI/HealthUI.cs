using System;
using Code.Core.EventSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private Image healthImage;
        [SerializeField] private float changeTime;

        private Tween _healthChangeTween;

        private void Awake()
        {
            playerChannel.AddListener<HealthChangeEvent>(HandleHealthChangeEvent);
        }

        private void OnDestroy()
        {
            playerChannel.AddListener<HealthChangeEvent>(HandleHealthChangeEvent);
        }

        private void HandleHealthChangeEvent(HealthChangeEvent evt)
        {
            if (_healthChangeTween.IsActive())
                _healthChangeTween.Complete();

            _healthChangeTween = DOTween.To(() => healthImage.fillAmount, x => healthImage.fillAmount = x,
                evt.currentHealth / evt.maxHealth, changeTime);
        }
    }
}