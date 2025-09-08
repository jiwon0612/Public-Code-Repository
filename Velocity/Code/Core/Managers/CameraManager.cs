using System;
using Code.Core.EventSystem;
using Code.Player;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Code.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private float verdictShakePower;
        [SerializeField] private float hitEffectTime;
        [SerializeField] private float hitShakePower;

        private CinemachineImpulseSource _impulseSource;
        private Volume _volume;
        
        private void Awake()
        {
            playerChannel.AddListener<DashEvent>(HandleDashEvent);
            playerChannel.AddListener<GroundEvent>(HandleGroundEvent);
            playerChannel.AddListener<HealthChangeEvent>(HandleHealthChangeEvent);
            _volume = GetComponent<Volume>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnDestroy()
        {
            playerChannel.RemoveListener<DashEvent>(HandleDashEvent);
            playerChannel.RemoveListener<GroundEvent>(HandleGroundEvent);
            playerChannel.RemoveListener<HealthChangeEvent>(HandleHealthChangeEvent);
        }

        private void HandleHealthChangeEvent(HealthChangeEvent evt)
        {
            if (evt.afterHeath > evt.currentHealth)
            {
                if (_volume.profile.TryGet(out Vignette vignette))
                {
                    vignette.intensity.value = 0.5f;
                    DOTween.To(() => vignette.intensity.value,x => vignette.intensity.value = x, 0,hitEffectTime);
                }

                _impulseSource.DefaultVelocity = new Vector3(1, 0, 0);
                _impulseSource.ImpulseDefinition.ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Explosion;
                _impulseSource.GenerateImpulse(hitShakePower);
            }
        }

        private void HandleGroundEvent(GroundEvent obj)
        {
            _impulseSource.ImpulseDefinition.ImpulseShape = CinemachineImpulseDefinition.ImpulseShapes.Bump;
            _impulseSource.DefaultVelocity = new Vector3(0, -1, 0);
            
            switch (obj.verdict)
            {
                case GroundVerdict.Perfect:
                    break;
                case GroundVerdict.Good:
                    _impulseSource.GenerateImpulse(verdictShakePower);
                    break;
                case GroundVerdict.Ok:
                    _impulseSource.GenerateImpulse(verdictShakePower * 2);
                    break;
                case GroundVerdict.Bad:
                    _impulseSource.GenerateImpulse(verdictShakePower * 3);
                    break;
            }
        }

        private void HandleDashEvent(DashEvent obj)
        {
            // if (_volume.profile.TryGet(out MotionBlur motionBlur))
            //     motionBlur.active = obj.isEnable;

            if (_volume.profile.TryGet(out ChromaticAberration chromaticAberration))
                chromaticAberration.active = obj.isEnable;

            if (_volume.profile.TryGet(out DepthOfField depthOfField))
                depthOfField.active = obj.isEnable;
        }
    }
}