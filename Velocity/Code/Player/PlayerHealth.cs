using System;
using Code.Core.EventSystem;
using Code.Entities;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Player
{
    public class PlayerHealth : MonoBehaviour, IEntityCompo
    {
        public UnityEvent<float, float> OnHitEvent;

        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private float maxHealth;
        [SerializeField] private AudioClip hitSound;

        private Entity _entity;

        private float _currentHealth;


        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        private void Start()
        {
            _currentHealth = maxHealth;
            playerChannel.RaiseEvent(PlayerEvents.HealthChange.Init(_currentHealth, _currentHealth, maxHealth));
        }

        public bool TakeDamage(float damage)
        {
            if (!_entity.IsDead && _entity.IsCanHit)
            {
                float afterHealth = _currentHealth;
                _currentHealth -= damage;

                if (_currentHealth <= 0f)
                {
                    _entity.Dead();
                }
                else
                {
                    EazySoundManager.PlaySound(hitSound);
                    playerChannel.RaiseEvent(PlayerEvents.HealthChange.Init(afterHealth, _currentHealth, maxHealth));
                    OnHitEvent?.Invoke(afterHealth, _currentHealth);
                }

                return true;
            }
            else
                return false;
        }
    }
}