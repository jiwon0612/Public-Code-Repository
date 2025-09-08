using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private float _timer;

        private bool _isEnable;

        private void Start()
        {
            _isEnable = true;
            _timer = 0;
        }

        private void Update()
        {
            if (_isEnable)
            {
                _timer += Time.deltaTime;
                text.text = _timer.ToString();
            }
        }

        public void StopTimer()
        {
            _isEnable = false;

            float value = PlayerPrefs.GetFloat($"StageTimer{SceneManager.GetActiveScene().buildIndex - 1}", -1);
            if (value == -1)
            {
                PlayerPrefs.SetFloat($"StageTimer{SceneManager.GetActiveScene().buildIndex - 1}", _timer);
            }
            else
            {
                if (_timer < value)
                    PlayerPrefs.SetFloat($"StageTimer{SceneManager.GetActiveScene().buildIndex - 1}", _timer);
            }

            
        }
    }
}