using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (_player.IsGroundDetected())
        {
            upAttacked = false;
            downAttacked = false;
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }
}
