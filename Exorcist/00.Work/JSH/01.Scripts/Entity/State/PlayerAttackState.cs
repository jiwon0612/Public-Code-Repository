using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int _comboCounter;
    private float _lastAttackTime;
    private float _comboWindow = 0.8f; //�޺� �̾����� �ð�
    private Coroutine _delayCoroutine;
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");


    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.attackOverridePos = _player.AttackCheckerTrm;

        if (_comboCounter >2 || Time.time >= _lastAttackTime +_comboWindow)
        {
            _comboCounter = 0;
        }

        _player.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter); 
        _player.currentComboCount = _comboCounter;

        float attackDirection = _player.FacingDirection;
        float xInput = _player.PlayerInput.Xinput;
        if (Mathf.Abs(xInput) > 0.05f)
        {
            attackDirection = Mathf.Sign(xInput);
        }

        Vector2 movement = _player.attackMovement[_comboCounter];
      //  EazySoundManager.PlaySound(_player.playerAudio[0]);
        _player.SetVelocity(movement.x * attackDirection, movement.y);


        float delayTime = 0.15f;
        _delayCoroutine = _player.StartDelayCallback(delayTime, () => _player.StopImmediately(false)); 

    }

    public override void Update()
    {
        base.Update();

        if (_triggerCall)
            _stateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void Exit()
    {
        ++_comboCounter;
        _lastAttackTime = Time.time;

        _player.StopCoroutine(_delayCoroutine);
        base.Exit();
    }
}
