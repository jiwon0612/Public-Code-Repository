using Code.Core.EventSystem;
using Code.Entities;
using UnityEngine;

namespace Code.Player.State
{
    public class PlayerDescentState : PlayerState
    {
        private readonly float _rotationSpeed = 2;
        private Vector3 _afterDirection;

        public PlayerDescentState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnDescentEvent += HandleDescentEvent;
            _mover.IsAddDownForce = true;
        }

        public override void Exit()
        {
            _animator.transform.localRotation = Quaternion.identity;
            _player.PlayerInput.OnDescentEvent -= HandleDescentEvent;
            _mover.IsAddDownForce = false;
            base.Exit();
        }

        private void HandleDescentEvent(bool value)
        {
            if (!value)
                _player.ChangeState("FLY");
        }

        public override void Update()
        {
            base.Update();

            _animator.transform.localRotation =
                Quaternion.Lerp(_animator.transform.localRotation, Quaternion.Euler(45, 0, 0),
                    Time.deltaTime * _rotationSpeed);

            _mover.SetInput(new Vector3(_player.PlayerInput.MovementKey.x, 0, 1),
                new Vector3(0, _cameraArm.transform.eulerAngles.y, 0));

            if (_mover.IsGround())
            {
                float angle = Vector3.Angle(_mover.GetGroundInfo().normal, _afterDirection) - 90;

                GroundVerdict verdict = _player.CalculateGroundVerdict(angle);

                _player.PlayerChannel.RaiseEvent(PlayerEvents.Ground.Init(verdict));
                switch (_player.CalculateGroundVerdict(angle))
                {
                    case GroundVerdict.Perfect:
                        _mover.AddAcceleration(4);
                        break;
                    case GroundVerdict.Good:
                        _mover.SubtractionAcceleration(5);
                        break;
                    case GroundVerdict.Ok:
                        _mover.SubtractionAcceleration(15);
                        break;
                    case GroundVerdict.Bad:
                        _mover.SubtractionAcceleration(30);
                        break;
                }

                _player.ChangeState("RUN");
            }
            
            _afterDirection = _mover.MoveDirection;
        }
    }
}