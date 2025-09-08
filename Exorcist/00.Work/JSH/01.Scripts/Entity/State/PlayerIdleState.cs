using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.StopImmediately(false);
    }

    public override void Update()
    {
        base.Update();

        float xInput = _player.PlayerInput.Xinput;

        if (Mathf.Abs(_player.FacingDirection + xInput) > 1.5f && _player.IsWallDetected())
            return;

        if(Mathf.Abs(xInput) > 0.5f)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Move);
        }
    }
}
