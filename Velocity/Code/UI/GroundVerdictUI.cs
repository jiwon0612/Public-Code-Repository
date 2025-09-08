using System;
using Code.Core.EventSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class GroundVerdictUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO playerChannel;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float animationTime;
        [SerializeField] private float fadeTime;

        private Tween _textTween;
        private Tween _fadeTween;

        private void Awake()
        {
            playerChannel.AddListener<GroundEvent>(HandleGroundEvent);
            text.enabled = false;
        }

        private void OnDestroy()
        {
            playerChannel.RemoveListener<GroundEvent>(HandleGroundEvent);
        }

        private void HandleGroundEvent(GroundEvent evt)
        {
            string str = evt.verdict.ToString();
            text.enabled = true;
            text.text = str;

            if (_textTween.IsActive())
                _textTween.Complete();

            _fadeTween.Kill();

            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);

            // _textTween = text.transform.DOShakePosition(animationTime, new Vector3(0, 50, 0),10).OnComplete(() 
            //     =>_fadeTween = text.DOFade(0, fadeTime).OnComplete(()=> text.enabled = false));
            _fadeTween = text.DOFade(0, fadeTime);
        }
    }
}