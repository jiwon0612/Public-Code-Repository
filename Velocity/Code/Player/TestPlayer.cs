using System;
using Code.Entities;
using UnityEngine;

namespace Code.Player
{
    public class TestPlayer : Entity
    {
        [field: SerializeField] public PlayerInputSO InputSO;

        private EntityMover _mover;
        private CameraArm _arm;
        
        protected override void AddComponent()
        {
            base.AddComponent();
            _mover = GetCompo<EntityMover>();
            _arm = GetCompo<CameraArm>();
        }

        private void Update()
        {
            _arm.SetRotation(InputSO.LookKey);
            _mover.SetInput(new Vector3(InputSO.MovementKey.x, 0, InputSO.MovementKey.y), new Vector3(0,_arm.transform.eulerAngles.y,0));
        }
    }
}