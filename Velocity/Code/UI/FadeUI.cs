using System;
using System.Collections;
using Code.Core.EventSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class FadeUI : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private GameEventChannelSO uiChannel;

        [SerializeField] private float fadeTime;

        private Material _fadeMat;

        private readonly int _circleSizeHash = Shader.PropertyToID("_CircleSize");
        private readonly int _sideValue = Shader.PropertyToID("_SideValue");

        private void Awake()
        {
            var image = GetComponent<Image>();
            _fadeMat = Instantiate(image.material);
            image.material = _fadeMat;

            sceneChannel.AddListener<SceneChangeEvent>(HandleSceneChangeEvent);
            sceneChannel.AddListener<SceneStartEvent>(HandleSceneStartEvent);
        }

        private void OnDestroy()
        {
            sceneChannel.RemoveListener<SceneStartEvent>(HandleSceneStartEvent);
            sceneChannel.RemoveListener<SceneChangeEvent>(HandleSceneChangeEvent);
        }

        private void HandleSceneStartEvent(SceneStartEvent obj)
        {
            FadeIn();
        }

        private void HandleSceneChangeEvent(SceneChangeEvent evt)
        {
            FadeOut();
        }

        private void FadeOut()
        {
            _fadeMat.SetFloat(_circleSizeHash, 0f);
            _fadeMat.SetFloat(_sideValue, 0.5f);

            // DOTween.Sequence()
            //     .AppendCallback(() =>
            //     {
            //         Debug.Log("Dd");
            //         uiChannel.RaiseEvent(UIEvents.FadeComplete);
            //     })
            //     .Append(DOTween.To(() => _fadeMat.GetFloat(_circleSizeHash), x => _fadeMat.SetFloat(_circleSizeHash, x),
            //         1.3f, fadeTime))
            //     .Join(DOTween.To(() => _fadeMat.GetFloat(_sideValue), x => _fadeMat.SetFloat(_sideValue, x),
            //         -0.6f, fadeTime));

            // DOTween.To(() => _fadeMat.GetFloat(_circleSizeHash), x => _fadeMat.SetFloat(_circleSizeHash, x),
            //     1.3f, fadeTime).OnComplete(() => Debug.Log("ddd"));
            //
            // DOTween.To(() => _fadeMat.GetFloat(_sideValue), x => _fadeMat.SetFloat(_sideValue, x),
            //     -0.6f, fadeTime);

            StartCoroutine(LerpCoroutine(_circleSizeHash, 0, 1.3f, fadeTime,
                () =>
                {
                    uiChannel.RaiseEvent(UIEvents.FadeComplete);
                }));
            StartCoroutine(LerpCoroutine(_sideValue, 0.5f, -0.6f, fadeTime));
        }

        private void FadeIn()
        {
            // _fadeMat.SetFloat(_circleSizeHash, 1.3f);
            // _fadeMat.SetFloat(_sideValue, -0.6f);
            //
            // Sequence seq = DOTween.Sequence();
            // seq.Append(DOTween.To(() => _fadeMat.GetFloat(_circleSizeHash), x => _fadeMat.SetFloat(_circleSizeHash, x),
            //     0f, fadeTime));
            // seq.Join(DOTween.To(() => _fadeMat.GetFloat(_sideValue), x => _fadeMat.SetFloat(_sideValue, x),
            //     0.5f, fadeTime));

            StartCoroutine(LerpCoroutine(_circleSizeHash, 1.3f, 0, fadeTime));
            StartCoroutine(LerpCoroutine(_sideValue, -0.6f, 0.5f, fadeTime));
        }

        private IEnumerator LerpCoroutine(int hash, float startValue, float endValue, float time,
            Action Callback = null)
        {
            _fadeMat.SetFloat(hash, startValue);
            float currentTime = 0;

            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                float t = currentTime / time;
                _fadeMat.SetFloat(hash, Mathf.Lerp(startValue, endValue, t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t)));
                yield return null;
            }
            Callback?.Invoke();
        }
    }
}