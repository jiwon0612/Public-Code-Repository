using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpAttackState : PlayerState
{
    public PlayerUpAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocity(x: _rigidbody.velocity.x, y: _player.JumpForce, doNotFlip: true);
        _player.attackOverridePos = _player.UpattackCheckerTrm;

    }

    public override void Update()
    {
        base.Update();

        if (_triggerCall)
            _stateMachine.ChangeState(PlayerStateEnum.Fall);

    }
}
