using Code.Entities;
using UnityEngine;

namespace Code.Player.State
{
    public class PlayerRunState : PlayerState
    {
        private readonly int _groundRollingHash = Animator.StringToHash("GROUNDROLL");
        
        public PlayerRunState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.IsEntityRotation = true;
            if (_player.IsGroundRolling)
            {
                _animator.SetParam(_groundRollingHash);
                _player.IsGroundRolling = false;
            }
        }

        public override void Update()
        {
            base.Update();

            if (!_mover.IsGround())
            {
                _player.IsAirRolling = true;
                _player.ChangeState("FLY");
            }
            
            _mover.SetInput(new Vector3(_player.PlayerInput.MovementKey.x, 0, 1), new Vector3(0,_cameraArm.transform.eulerAngles.y,0));
        }
    }
}