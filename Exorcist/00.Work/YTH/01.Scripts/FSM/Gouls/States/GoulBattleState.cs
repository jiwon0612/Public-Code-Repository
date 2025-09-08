using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulBattleState : EnemyState<GoulStateEnum>
{
    protected Player _player;
    private int _moveDirection;
    private float _timer;

    private readonly int _xVelocityHash = Animator.StringToHash("x_velocity");//���� ����
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
        //����ĳ��Ʈ������ü �ȿ� operator bool �� ������ �Ǿ� ����. hit.collider == null üũ��.

        //���� �����ϴٸ� ����
        if (hit )
        {
            _timer = _enemyBase.battleTime; //Ÿ�̸� ����

            if (hit.distance < _enemyBase.attackDistance && CanAttack())
            {
                _stateMachine.ChangeState(GoulStateEnum.Attack);
                return;
            }
        }
        float distance = Vector2.Distance(_player.transform.position, _enemyBase.transform.position);

        if (distance <= _enemyBase.attackDistance)
        {
            _enemyBase.StopImmediately(false);  //���� Ʈ���� ���� ���� ������ �̵��� ������
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
