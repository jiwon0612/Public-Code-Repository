using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        float xInput = _player.PlayerInput.Xinput;

        _player.SetVelocity(xInput * _player.MoveSpeed, _rigidbody.velocity.y);

        if (Mathf.Abs(xInput) < 0.05f || _player.IsWallDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

    }

}
