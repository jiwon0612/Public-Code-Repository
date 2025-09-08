using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class CamImpulseAndPlayBloodEffect : MonoBehaviour
{
    private CinemachineImpulseSource _source;
    [SerializeField] private float _impulsePower;
    [SerializeField] private ParticleSystem _effect;
    [FormerlySerializedAs("effectPos")] [SerializeField] private Transform _effectPos;

    private void Awake()
    {
        _source = GetComponent<CinemachineImpulseSource>();
    }

    public void PlayImpulse()
    {
        _source.GenerateImpulse(_impulsePower);
    }

    public void PlayBloodEffect()
    {
        _effect.Play();
    }
}
