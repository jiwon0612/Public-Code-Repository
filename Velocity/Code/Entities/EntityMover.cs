using System;
using UnityEngine;

namespace Code.Entities
{
    public class EntityMover : MonoBehaviour, IEntityCompo
    {
        [Header("MoveSettings")] 
        [SerializeField] private float gravityValue;
        [SerializeField] private float airMoveSpeed;
        [SerializeField] private float airRotationSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float xRotationValue;
        [SerializeField] private float downForce;
        [SerializeField] private float maxDownForce;

        [Header("SpeedSettings")] 
        [SerializeField] private float maxAccelerationSpeed;
        [SerializeField] private float minAccelerationSpeed;
        [SerializeField] private float accelerationTimeSpeed;

        [Header("GroundSetting")] 
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 size;
        [SerializeField] private float maxDistance;

        public event Action<float, float> OnVelocityChange;
        
        public float Velocity
        {
            get => _velocity;
            set
            {
                float before = _velocity;
                if (Mathf.Approximately(before, value))
                    OnVelocityChange?.Invoke(before, value);
                
                _velocity = value;
            }
        }
        private float _velocity;

        public Vector3 MoveDirection => _rigid.linearVelocity.normalized;
        
        public bool IsAcceleration { get; set; }
        
        public bool IsEntityRotation { get; set; }
        
        public bool IsRun { get; set; }

        private Rigidbody _rigid;
        private Entity _entity;

        private Vector3 _projectionMoveDirection;
        private Vector3 _moveValue;
        private Quaternion _cameraDirection;
        private Quaternion _moveRotation;
        private RaycastHit _groundInfo;
        private float _gravity;

        private float _accelerationValue;
        private float _addValue;
        
        private float _currentDownForce;

        private bool _isGround;
        
        public bool IsAddDownForce {get;set;}

        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            _rigid = entity.GetComponent<Rigidbody>();
            _moveRotation = Quaternion.identity;
            IsAcceleration = false;
            IsEntityRotation = false;
            IsAddDownForce = false;
            IsRun = false;
        }

        public void SetInput(Vector3 input, Vector3 cameraDirection)
        {
            _moveValue = input;
            _cameraDirection = Quaternion.Euler(cameraDirection);
        }

        public void SubtractionAcceleration(float value) => _accelerationValue -= value;
        
        public void AddAcceleration(float value) => _accelerationValue += value;

        public bool IsGround()
        {
            _isGround = Physics.BoxCast(_entity.transform.position + new Vector3(0, 0.25f, 0), size, Vector3.down,
                Quaternion.identity, maxDistance, whatIsGround.value);
            return _isGround;
        }

        private void Update()
        {
            GravityCalculate();
            RotationCalculate();
            SpeedCalculate();
            
            if (IsEntityRotation)
                EntityRotation();
            
            Move();
            Velocity = _rigid.linearVelocity.magnitude;
        }

        private void SpeedCalculate()
        {
            if (IsGround() && IsAcceleration)
            {
                _accelerationValue += Time.deltaTime * accelerationTimeSpeed;
            }
            
            if (IsRun)
                _accelerationValue = Mathf.Clamp(_accelerationValue, minAccelerationSpeed, maxAccelerationSpeed);
            else
                _accelerationValue = 0;
        }

        private void RotationCalculate()
        {
            Vector3 velocityVec = new Vector3(_rigid.linearVelocity.x, 0, _rigid.linearVelocity.z);
            if (velocityVec.sqrMagnitude > 0)
            {
                float xDir = 0;

                if (!Mathf.Approximately(_moveValue.x, 0))
                    xDir = Mathf.Sign(_moveValue.x);

                Quaternion rotation = _cameraDirection * Quaternion.Euler(0, xRotationValue * xDir, 0);

                if (IsGround())
                    _moveRotation = Quaternion.Lerp(_moveRotation, rotation, Time.deltaTime * rotationSpeed);
                else
                    _moveRotation = Quaternion.Lerp(_moveRotation, rotation, Time.deltaTime * airRotationSpeed);
            }
            else
                _moveRotation = _cameraDirection;
        }

        private void GravityCalculate()
        {
            if(IsAddDownForce)
                _currentDownForce += downForce * Time.deltaTime;
            else
                _currentDownForce -= downForce * Time.deltaTime;  
            
            _currentDownForce = Mathf.Clamp(_currentDownForce, 0, maxDownForce);
            
            if (!IsGround())
            {
                _gravity += gravityValue * Time.deltaTime;
            }
            else
            {
                _gravity = 0.45f;
                _projectionMoveDirection = Vector3.zero;
            }
        }

        private void Move()
        {
            
            Vector3 addVector = new Vector3(0, -(_gravity + _currentDownForce), 0);
            Vector3 moveDirection = new Vector3(0, 0, _moveValue.z);
            if (IsGround())
            {
                _projectionMoveDirection = Vector3.ProjectOnPlane( _moveRotation * moveDirection, GetGroundInfo().normal);
                _rigid.linearVelocity =_projectionMoveDirection.normalized * _accelerationValue + addVector;
            }
            else
            {
                Vector3 direction = _moveRotation * moveDirection;
                Vector3 velocity = direction.normalized * _accelerationValue;
                float yVelocity = -(_gravity + _currentDownForce);
                velocity.y = yVelocity + (_projectionMoveDirection.y * _accelerationValue);
                
                _rigid.linearVelocity = velocity;
            }
        }

        
        public RaycastHit GetGroundInfo()
        {
            bool isValue = Physics.Raycast(_entity.transform.position + new Vector3(0, 0.25f, 0), Vector3.down, out _groundInfo, 1,
                whatIsGround.value);
            
            if (isValue)
                return _groundInfo;
            else
            {
                Physics.Raycast(_entity.transform.position + new Vector3(0, 0.25f, 0), Vector3.forward, out _groundInfo, 1,
                    whatIsGround.value);   
                return _groundInfo;
            }

        }

        public void EntityRotation()
        {
            // Quaternion lerpValue = Quaternion.LookRotation(new Vector3(_rigid.linearVelocity.x, 0, _rigid.linearVelocity.z));
            // _entity.transform.rotation 
            //     = Quaternion.Lerp(_entity.transform.rotation, lerpValue, Time.deltaTime * rotationSpeed);
            _entity.transform.rotation = Quaternion.LookRotation(new Vector3(_rigid.linearVelocity.x, 0, _rigid.linearVelocity.z));
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_entity != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(_entity.transform.position, _entity.transform.position + _projectionMoveDirection);
                Gizmos.color = Color.white;
            }
        }
#endif
    }
}