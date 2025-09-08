using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        float xInput = _player.PlayerInput.Xinput;
        float yInput = _player.PlayerInput.Yinput;

        if(yInput< 0)
        {
            _player.SetVelocity(0, _rigidbody.velocity.y, true);
        }
        else
        {
            _player.SetVelocity(0, _rigidbody.velocity.y * 0.7f, true);
        }

        if (Mathf.Abs(xInput + _player.FacingDirection) < 0.5f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }

        if (_player.IsGroundDetected() || !_player.IsWallDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }
    }

    public override void Enter()
    {
        base.Enter();
        _player.PlayerInput.JumpEvent += HandleJumpEvent;
    }

    private void HandleJumpEvent()
    {
        _stateMachine.ChangeState(PlayerStateEnum.WallJump);
    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvent;
        base.Exit();

    }
}
