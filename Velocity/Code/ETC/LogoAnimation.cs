using System;
using Code.Core.EventSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.ETC
{
    [DefaultExecutionOrder(-10)]
    public class LogoAnimation : MonoBehaviour
    {
        [SerializeField] private float animationTime;
        
        private Material _logoMat;
        
        private readonly int _horizontalHash = Shader.PropertyToID("_HorizontalValue");
        
        private void Awake()
        {
            _logoMat = GetComponent<Image>().material;
            
            _logoMat.SetFloat(_horizontalHash, 0);
            ShowLogo();
        }


        private void ShowLogo()
        {
            DOVirtual.DelayedCall(2, () =>
            {
                DOTween.To(() => _logoMat.GetFloat(_horizontalHash), x => _logoMat.SetFloat(_horizontalHash, x), 0.226f,
                        animationTime)
                    .OnComplete(() => DOTween
                        .To(() => _logoMat.GetFloat(_horizontalHash), x => _logoMat.SetFloat(_horizontalHash, x),
                            0.428f, animationTime)
                        .OnComplete(() => DOTween
                            .To(() => _logoMat.GetFloat(_horizontalHash), x => _logoMat.SetFloat(_horizontalHash, x),
                                0.587f, animationTime)
                            .OnComplete(() => DOTween.To(() => _logoMat.GetFloat(_horizontalHash),
                                x => _logoMat.SetFloat(_horizontalHash, x), 1f, animationTime))));


            });
        }
    }
}