using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Setting Values")]
    public float moveSpeed;  
    public float battleTime;

    private float _defalutMoveSpeed;

    [SerializeField] protected LayerMask _whatIsPlayer;

    [Header("Attack Values")]
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    protected int _lastAnimationBoolHash;

    public bool IsDead =false;

    #region stun
    protected bool _isStun = false;
    protected bool _isStunWithoutTimer = false;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _defalutMoveSpeed = moveSpeed;
    }

    public virtual RaycastHit2D IsPlayerDetected()
        => Physics2D.Raycast(_wallChecker.position, Vector2.right * FacingDirection, _whatIsPlayer);

    public abstract void OnAnimationTrigger();
    public void ReturnDefaultSpeed()
    {
        AnimatorCompo.speed = 1f;
        moveSpeed = _defalutMoveSpeed;
    }

    protected override void HandleDead(Vector2 direction)
    {
        isDead = true;
    }

    #region Freeze logic

    public virtual void FreezeTime(bool isFreeze, bool isFreezeWithoutTimer = false)
    {
        if (isFreezeWithoutTimer)
            _isStunWithoutTimer = isFreezeWithoutTimer;

        _isStun = isFreeze;
        if (_isStun)
        {
            moveSpeed = 0;
            AnimatorCompo.speed = 0;
        }
        else
        {
            moveSpeed = _defalutMoveSpeed;
            AnimatorCompo.speed = 1;
            _isStunWithoutTimer = false;
        }
    }

    public virtual void FreezeTimeFor(float freezeTime)
    {
        FreezeTime(true); //freeze enemy
        StartDelayCallback(freezeTime, () =>
        {
            if (!_isStunWithoutTimer)
            {
                FreezeTime(false); //unfreeze when not perminant freezing state
            }
        });
    }

    #endregion
}
