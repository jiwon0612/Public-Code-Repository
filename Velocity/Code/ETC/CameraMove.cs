using System;
using Code.Player;
using UnityEngine;

namespace Code.ETC
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO input;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxZPosition;
        [SerializeField] private float minZPosition;
        
        private bool _isClick;
        
        private void Awake()
        {
            input.OnClickEvent += HandleClickEvent;
        }

        private void OnDestroy()
        {
            input.OnClickEvent -= HandleClickEvent;
        }

        private void HandleClickEvent(bool value)
        {
            _isClick = value;
        }

        private void Update()
        {
            if (_isClick)
            {
                Vector2 lookKey = input.LookKey;
                transform.position += Vector3.forward * (-lookKey.y * moveSpeed);
                float zClamp = Mathf.Clamp(transform.position.z, minZPosition, maxZPosition);
                transform.position = new Vector3(transform.position.x, transform.position.y, zClamp);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(transform.position.x,transform.position.y,minZPosition), 0.5f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(transform.position.x,transform.position.y,maxZPosition), 0.5f);
            Gizmos.color = Color.white;
        }
    }
}