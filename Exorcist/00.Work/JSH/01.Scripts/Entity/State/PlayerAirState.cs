using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{

    public static bool upAttacked = false ;
    public static bool downAttacked = false;

    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
      
    }

    public override void Enter()
    {
        base.Enter();
         
        _player.PlayerInput.AttackEvent += HandleAttackEvent;
    }

    private void HandleAttackEvent()
    {
        if( !upAttacked) 
        {
            upAttacked = true;
            _stateMachine.ChangeState(PlayerStateEnum.UpAttack);
        }
        else if (Input.GetKey(KeyCode.S) && !downAttacked)
        {
            downAttacked = true;
            _stateMachine.ChangeState(PlayerStateEnum.DownAttack);
        }
    }

    public override void Exit()
    {
        _player.PlayerInput.AttackEvent -= HandleAttackEvent;
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        float xInput = _player.PlayerInput.Xinput;

        _player.SetVelocity(_player.MoveSpeed * 0.7f * xInput, _rigidbody.velocity.y);

        if (_player.IsWallDetected())
        {
            upAttacked = false;
            downAttacked = false;
            _stateMachine.ChangeState(PlayerStateEnum.WallSlide);
        }

    }
}
