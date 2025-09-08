using Code.Entities;
using Code.FSM;
using UnityEngine;

namespace Code.Player.State
{
    public abstract class PlayerState : EntityState
    {
        protected readonly int _rollingHash = Animator.StringToHash("ROLL");
        protected Player _player;
        protected EntityMover _mover;
        protected CameraArm _cameraArm;
        
        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
            _cameraArm = _player.GetCompo<CameraArm>();
        }

        public override void Update()
        {
            base.Update();
            _cameraArm.SetRotation(_player.PlayerInput.LookKey);
        }
    }
}