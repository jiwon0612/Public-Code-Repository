using Code.Core.EventSystem;
using Code.Entities;
using Hellmade.Sound;
using UnityEngine;

namespace Code.Player.State
{
    public class PlayerFlyState : PlayerState
    {
        private readonly int _airRollingHash = Animator.StringToHash("AIRROLL");
        private readonly float _minAirTime = 0.25f;

        private Vector3 _afterDirection;
        private float _timer;

        public PlayerFlyState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnDescentEvent += HandleDescentEvent;

            if (_player.IsAirRolling)
            {
                _animator.SetParam(_airRollingHash);
                _player.IsAirRolling = false;
            }
        }

        public override void Exit()
        {
            _player.PlayerInput.OnDescentEvent -= HandleDescentEvent;
            base.Exit();
        }

        private void HandleDescentEvent(bool value)
        {
            if (value)
                _player.ChangeState("DESCENT");
        }

        public override void Update()
        {
            base.Update();

            _timer += Time.deltaTime;

            if (_mover.IsGround())
            {
                if (_timer > _minAirTime)
                {
                    float angle = Vector3.Angle(_mover.GetGroundInfo().normal, _afterDirection) - 90;

                    GroundVerdict verdict = _player.CalculateGroundVerdict(angle);

                    _player.PlayerChannel.RaiseEvent(PlayerEvents.Ground.Init(verdict));
                    EazySoundManager.PlaySound(_player.groundRandingSound);
                    switch (_player.CalculateGroundVerdict(angle))
                    {
                        case GroundVerdict.Perfect:
                            _mover.AddAcceleration(10);
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
                }

                _timer = 0;
                _player.IsGroundRolling = true;
                _player.ChangeState("RUN");
            }

            _afterDirection = _mover.MoveDirection;

            _mover.SetInput(new Vector3(_player.PlayerInput.MovementKey.x, 0, 1),
                new Vector3(0, _cameraArm.transform.eulerAngles.y, 0));
        }
    }
}