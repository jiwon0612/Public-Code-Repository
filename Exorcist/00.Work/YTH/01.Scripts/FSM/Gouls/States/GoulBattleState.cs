using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulBattleState : EnemyState<GoulStateEnum>
{
    protected Player _player;
    private int _moveDirection;
    private float _timer;

    private readonly int _xVelocityHash = Animator.StringToHash("x_velocity");//여기 여기
    public GoulBattleState(Enemy enemyBase, EnemyStateMachine<GoulStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _enemyBase.AnimatorCompo.SetFloat(_xVelocityHash, Mathf.Abs(_rigidbody.velocity.x));

        if (_player.transform.position.x > _enemyBase.transform.position.x)
            _moveDirection = 1;
        else
            _moveDirection = -1;
        _enemyBase.SetVelocity(_enemyBase.moveSpeed * _moveDirection, _rigidbody.velocity.y);

        RaycastHit2D hit = _enemyBase.IsPlayerDetected();
        //레이캐스트힛구조체 안에 operator bool 이 재정의 되어 있음. hit.collider == null 체크함.

        //공격 가능하다면 공격
        if (hit )
        {
            _timer = _enemyBase.battleTime; //타이머 설정

            if (hit.distance < _enemyBase.attackDistance && CanAttack())
            {
                _stateMachine.ChangeState(GoulStateEnum.Attack);
                return;
            }
        }
        float distance = Vector2.Distance(_player.transform.position, _enemyBase.transform.position);

        if (distance <= _enemyBase.attackDistance)
        {
            _enemyBase.StopImmediately(false);  //블렌드 트리로 값에 따라 정지와 이동이 나오게
            return;
        }
    }

    public override void Enter()
    {
        base.Enter();
        //Find Player
        SetDirectionToEnemy();
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void SetDirectionToEnemy()
    {
        _enemyBase.FlipController(_player.transform.position.x - _enemyBase.transform.position.x);
    }

    private bool CanAttack()
    {
        return Time.time >= _enemyBase.lastTimeAttacked + _enemyBase.attackCooldown;
    }

    
}
