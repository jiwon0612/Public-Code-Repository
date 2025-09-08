using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    private Coroutine _delayCoroutine = null;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
   
    public override void Enter()
    {
        base.Enter();
        Vector2 wallJumpForce = _player.WallJumpForce;
        _player.SetVelocity(-wallJumpForce.x * _player.FacingDirection, wallJumpForce.y);

        _delayCoroutine = _player.StartDelayCallback(0.25f, () =>
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        });
    }

    public override void Exit()
    {
        _player.StopCoroutine(_delayCoroutine);
        base.Exit();
    }
}
