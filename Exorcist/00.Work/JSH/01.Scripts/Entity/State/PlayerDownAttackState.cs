using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownAttackState : PlayerState
{
    public PlayerDownAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocity(x: _rigidbody.velocity.x, y: _player.JumpForce *0.1f, doNotFlip: true);
        _player.attackOverridePos = _player.DownattackCheckerTrm;
    }

    public override void Update()
    {
        base.Update();

        if (_triggerCall)
            _stateMachine.ChangeState(PlayerStateEnum.Fall);

    }
}
