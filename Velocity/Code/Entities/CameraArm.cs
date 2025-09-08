using Unity.Cinemachine;
using UnityEngine;

namespace Code.Entities
{
    public class CameraArm : MonoBehaviour, IEntityCompo
    {
        [Header("CameraSettings")] 
        public CinemachineCamera targetCamera;
        [SerializeField] private float cameraDistance = 5;
        [SerializeField] private Vector3 cameraOffset;
        [SerializeField] private float maxXAngle;
        [SerializeField] private float minXAngle;
        [SerializeField] private float xRotationSpeed = 1;
        [SerializeField] private float yRotationSpeed = 1;

        [Header("WallCheckSetting")]
        [SerializeField] private LayerMask whatIsPermeability;
        
        private int _mask;

        private Vector3 _cameraRotation;
        private Entity _entity;

        public Vector3 CameraRotation
        {
            get => _cameraRotation;
            set
            {
                _cameraRotation.y = value.y;

                if (value.x < minXAngle)
                    _cameraRotation.x = minXAngle;
                else if (value.x > maxXAngle)
                    _cameraRotation.x = maxXAngle;
                else
                    _cameraRotation.x = value.x;
            }
        }

        public Vector3 TargetDirection
        {
            get => (transform.position - targetCamera.transform.position).normalized;
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;

            InitializeCam();
        }

        private void InitializeCam()
        {
            targetCamera.transform.SetParent(transform);
            targetCamera.transform.localPosition = cameraOffset + transform.forward * (cameraDistance * -1);

            _mask = ~whatIsPermeability.value;
        }

        public void SetRotation(Vector2 delta)
        {
            float yRotation = delta.x * xRotationSpeed * Time.deltaTime;
            float xRotation = -delta.y * yRotationSpeed * Time.deltaTime;

            CameraRotation += new Vector3(xRotation, yRotation, 0f);
            transform.rotation = Quaternion.Euler(CameraRotation);
        }

        private void FixedUpdate()
        {
            SetPosition();
        }

        private void SetPosition()
        {
            Vector3 point = cameraOffset + Vector3.forward * (cameraDistance * -1);

            float distance = cameraDistance;
            Vector3 direction = TargetDirection * -1;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, _mask))
                point = transform.InverseTransformPoint(hit.point);
            
            targetCamera.transform.localPosition = point;
        }
    }
}