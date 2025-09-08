using System;
using Code.Core.EventSystem;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.StageSystem
{
    public class Stage : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO sceneChannel;
        [SerializeField] private Stage beforStage;
        [SerializeField] private TextMeshPro text;
        [SerializeField] private int nextStageIndex;

        [SerializeField] private float mouseEnterTime;
        [SerializeField] private float mouseEnterSizeUpValue;
        
        private SpriteRenderer _renderer;
        private LineRenderer _lineRenderer;
        private bool _isEnable;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _lineRenderer = GetComponentInChildren<LineRenderer>();
            text.gameObject.SetActive(false);

            float timer = PlayerPrefs.GetFloat($"StageTimer{nextStageIndex - 1}",-1);

            if (!Mathf.Approximately(timer, -1))
            {
                text.text = $"{timer}";
            }
            else
            {
                text.text = "00";
            }
            
            if (nextStageIndex < 3)
            {
                _isEnable = true;
            }
            else
            {
                int value = PlayerPrefs.GetInt($"ClearScene{nextStageIndex - 2}", 0);
                
                if (value == 0)
                {
                    _isEnable = false;
                    return;
                }
                else
                {
                    _isEnable = true;
                }
            }
            
            if (beforStage != null)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, beforStage.transform.position);
            }
            else
            {
                _lineRenderer.enabled = false;
            }
        }

        public void OnMouseEnter()
        {
            if (_isEnable)
            {
                text.gameObject.SetActive(true);
                text.transform.rotation = Quaternion.Euler(0, 0, -90);
                text.transform.DOLocalRotate(Vector3.zero, mouseEnterTime);
                transform.DOScale(Vector3.one * mouseEnterSizeUpValue, mouseEnterTime);
            }
        }

        public void OnMouseExit()
        {
            if (_isEnable)
            {
                transform.DOScale(Vector3.one, mouseEnterTime);
                text.gameObject.SetActive(false);
            }
        }

        private void OnMouseUpAsButton()
        {
            if (_isEnable)
                sceneChannel.RaiseEvent(SceneEvents.SceneChange.Init(nextStageIndex));
        }
    }
}