using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class FollowHealth : UIToolkitParents
{
    [SerializeField] private float delay;

    [FormerlySerializedAs("lerpSpeed")] [SerializeField]
    private float tweeningSec;

    private PlayerHPUI _playerHpUi;
    private ProgressBar _hpFollower;
    private ProgressBar _bossHpFollower;

    private float _currentValue;

    protected override void OnEnable()
    {
        base.OnEnable();
        _hpFollower = Root.Q<ProgressBar>("HealthFollower");

        _playerHpUi = GetComponent<PlayerHPUI>();
        _playerHpUi.OnHpUIChange += (float v) => FollowValue(v, _hpFollower);
    }

    void FollowValue(float value, ProgressBar progressBar)
    {
        if (value > _hpFollower.value)
        {
            _hpFollower.value = value;
            return;
        }

        _currentValue = value;

        StartCoroutine(FollowValueWitDelay(value));
    }

    IEnumerator FollowValueWitDelay(float value)
    {
        yield return new WaitForSeconds(delay);

        if (Mathf.Approximately(_currentValue, value))
        {
            float followerValue = _hpFollower.value;

            while (_hpFollower.value - value > 0.5f)
            {
                followerValue = Mathf.Lerp(followerValue, value, tweeningSec / _currentValue);
                _hpFollower.value = followerValue;
                yield return new WaitForSeconds(tweeningSec/_currentValue);
            }

            _hpFollower.value = value;
        }
    }
}