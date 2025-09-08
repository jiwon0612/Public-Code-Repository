using System;
using DG.Tweening;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI
{
    [RequireComponent(typeof(EventTrigger))]
    public class ButtonUI : MonoBehaviour
    {
        [SerializeField] private Transform visualImage;
        [SerializeField] private Image selectedImage;
        [SerializeField] private float verticalTime;
        [SerializeField] private float noiseTime;
        [SerializeField] private AudioClip btnHover;
        
        public UnityEvent OnMouseClick;
        private EventTrigger _eventTrigger;

        private Material _selectedImageMat;

        private readonly int _verticalValue = Shader.PropertyToID("_VerticalValue");
        private readonly int _noiseValue = Shader.PropertyToID("_NoiseValue");
        
        private Tween _verticalTween;
        private Tween _noiseTween;
        
        private void Awake()
        {
            _eventTrigger = GetComponent<EventTrigger>();
            _selectedImageMat = selectedImage.material;
            
            EventTrigger.Entry enterEntry = new EventTrigger.Entry();
            enterEntry.eventID = EventTriggerType.PointerEnter;
            enterEntry.callback.AddListener((eventData) => MouseEnterEvent());
            
            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((eventData) => MouseExitEvent());
            
            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((eventData) => MouseClickEvent());
            
            _eventTrigger.triggers.Add(enterEntry);
            _eventTrigger.triggers.Add(exitEntry);
            _eventTrigger.triggers.Add(clickEntry);
            
            _selectedImageMat.SetFloat(_verticalValue, 1);
            _selectedImageMat.SetFloat(_noiseValue, -0.27f);
            selectedImage.gameObject.SetActive(false);
        }

        private void MouseEnterEvent()
        {
            //visualImage.transform.DORotate(new Vector3(0,0,rotationValue), rotationTime, RotateMode.Fast);
            EazySoundManager.PlayUISound(btnHover);
            selectedImage.gameObject.SetActive(true);
            _selectedImageMat.SetFloat(_verticalValue, 0);
            _selectedImageMat.SetFloat(_noiseValue, 1);

            _verticalTween = DOTween.To(() => _selectedImageMat.GetFloat(_verticalValue),
                    x => _selectedImageMat.SetFloat(_verticalValue, x), 1, verticalTime)
                .OnComplete(() => _noiseTween = DOTween.To(() => _selectedImageMat.GetFloat(_noiseValue),
                    x => _selectedImageMat.SetFloat(_noiseValue, x), -0.27f, noiseTime).SetUpdate(true)).SetUpdate(true);
        }

        private void MouseExitEvent()
        {
            //visualImage.transform.DORotate(Vector3.zero, rotationTime, RotateMode.Fast);
            
            if (_verticalTween.IsActive())
                _verticalTween.Complete();
            if (_noiseTween.IsActive())
                _noiseTween.Complete();
            
            _selectedImageMat.SetFloat(_verticalValue, 0);
            _selectedImageMat.SetFloat(_noiseValue, -0.27f);
            selectedImage.gameObject.SetActive(false);
        }

        private void MouseClickEvent()
        {
            OnMouseClick?.Invoke();
        }
    }
}