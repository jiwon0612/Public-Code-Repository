using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    private float _dashDirection;
    private float _dashStartTime;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        float xInput = _player.PlayerInput.Xinput;
        _dashDirection = Mathf.Abs(xInput) > 0.05f ? Mathf.Sign(xInput) : _player.FacingDirection;
        _dashStartTime = Time.time;
        _player.gameObject.layer = 10;
    }

    public override void Update()
    {
        base.Update();
        _player.SetVelocity(_player.DashSpeed * _dashDirection, 0);
        if(_dashStartTime + _player.DashDuration <= Time.time)
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _player.gameObject.layer = 7;
        _player.StopImmediately(false);
        base.Exit();
    }

}
