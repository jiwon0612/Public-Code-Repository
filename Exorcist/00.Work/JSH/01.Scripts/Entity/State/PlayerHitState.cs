using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        float deadFadeDelay = .1f;
        _player.StartDelayCallback(deadFadeDelay, () =>
        {
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
        });
    }
}
