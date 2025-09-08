using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundState : PlayerState
{
    private readonly int shootHash = Animator.StringToHash("TryShoot");
    private readonly int gunHash = Animator.StringToHash("Gun");

    private GunAniTrigger Gun_triggerCall;
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Gun_triggerCall = _player.gunAni.GetComponent<GunAniTrigger>();
        _player.PlayerInput.JumpEvent += HandleJumpEvent;
        _player.PlayerInput.AttackEvent += HandleAttackEvent;
        //_player.PlayerInput.SkillEvent += HandleSkillEvent;
    }

    private void HandleSkillEvent()
    {
        if (Gun_triggerCall.isActive == true) return;

        _player.gunAni.SetBool(gunHash, true);

    }

    public override void Exit()
    {
        _player.PlayerInput.JumpEvent -= HandleJumpEvent;
        _player.PlayerInput.AttackEvent -= HandleAttackEvent;
        //_player.PlayerInput.SkillEvent -= HandleSkillEvent;

        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }


    //Input event
    private void HandleJumpEvent()
    {
        if (_player.IsGroundDetected())
            _stateMachine.ChangeState(PlayerStateEnum.Jump);
    }

    private void HandleAttackEvent()
    {
        if (_player.IsGroundDetected())
        {
            _stateMachine.ChangeState(PlayerStateEnum.Attack);
        }
    }
}
