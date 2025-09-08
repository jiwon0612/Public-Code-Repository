using Code.Core.EventSystem;
using Code.Entities;
using DG.Tweening;
using UnityEngine;

namespace Code.Player.State
{
    public class PlayerDashChargeState : PlayerState
    {
        private readonly float _dashChargeTime = 1.5f;
        private readonly float _dashSpeed = 60;
        private float _timer;

        public PlayerDashChargeState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _timer = 0;
            _animator.transform.DOShakePosition(_dashChargeTime, new Vector3(0.1f, 0, 0), 50);
        }

        public override void Exit()
        {
            _mover.IsRun = true;
            _player.IsCanHit = true;
            _player.PlayerChannel.RaiseEvent(PlayerEvents.Dash.Init(true));
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            _timer += Time.deltaTime;
            _entity.transform.rotation = Quaternion.Euler(0, _cameraArm.transform.eulerAngles.y, 0);
            if (_timer >= _dashChargeTime)
            {
                _mover.IsAcceleration = true;
                _mover.AddAcceleration(_dashSpeed);
                _player.ChangeState("RUN");
            }
        }
    }
}