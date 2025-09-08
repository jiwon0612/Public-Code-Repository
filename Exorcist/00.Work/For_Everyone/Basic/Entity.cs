using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    #region Component List
    public Animator AnimatorCompo { get; protected set; }
    public Rigidbody2D RigidbodyCompo { get; protected set; }
    public SpriteRenderer SpriteRendererCompo { get; protected set; }
    public Collider2D ColliderCompo { get; protected set; }
    public Health HealthCompo { get; protected set; }   
    public DamageCaster DamageCasterCompo { get; protected set; }

    #endregion


    [Header("Collision info")]
    [SerializeField] protected Transform _groundChecker;
    [SerializeField] protected float _groundCheckDistance;
    [SerializeField] protected LayerMask _whatIsGroundAndWall;
    [SerializeField] protected Transform _wallChecker;
    [SerializeField] protected float _wallCheckDistance;
    [SerializeField] protected float _groundCheckBoxWidth;

    

    [Header("Knockback info")]
    [SerializeField] protected float _knockbackDuration;
    protected Coroutine _knockbackCoroutine = null;
    [HideInInspector] public bool isKnockbacked;

    [Header("Stun info")]
    public float stunDuration;
    public Vector2 stunPower;
    protected bool _canBeStun;

    public bool isDead = false;


    public Action<int> OnFlip;

    public int FacingDirection { get; protected set; } = 1;
    public bool CanStateChangeable { get; set; } = true;

    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        SpriteRendererCompo = visualTrm.GetComponent<SpriteRenderer>();
        RigidbodyCompo = GetComponent<Rigidbody2D>();
        ColliderCompo = GetComponent<Collider2D>();

        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        DamageCasterCompo.SetOwner(this);

        HealthCompo = GetComponent<Health>();
        HealthCompo.SetOwner(this);

        HealthCompo.OnHit += HandleHitEvent;
        HealthCompo.OnKnockback += HandleKnockbackEvent;
        HealthCompo.OnDead += HandleDead;

    }


    private void OnDestroy()
    {
        if(HealthCompo != null)
        {
            HealthCompo.OnHit -= HandleHitEvent;
            HealthCompo.OnKnockback -= HandleKnockbackEvent;
            HealthCompo.OnDead -= HandleDead;
        }
    }



    #region Handling Event region

    protected virtual void HandleDead(Vector2 direction)
    {
        
    }

    protected virtual void HandleHitEvent()
    {

    }

    protected virtual void HandleKnockbackEvent(Vector2 power)
    {
        if (_knockbackCoroutine != null)
            StopCoroutine(_knockbackCoroutine);

        isKnockbacked = true;
        SetVelocity(power.x, power.y, true);
        _knockbackCoroutine = StartDelayCallback(
            _knockbackDuration, () => isKnockbacked = false);
    }

    #endregion

    #region 딜레이 컬백

    public Coroutine StartDelayCallback(float delayTime, Action Callback)
    {
        return StartCoroutine(DelayCoroutine(delayTime, Callback));
    }

    protected IEnumerator DelayCoroutine(float delayTime, Action Callback)
    {
        yield return new WaitForSeconds(delayTime);
        Callback?.Invoke();
    }

    #endregion

    #region 움직임 제어

    public void SetVelocity(float x, float y, bool doNotFlip = false)
    {
        RigidbodyCompo.velocity = new Vector2(x, y);
        if (!doNotFlip)
            FlipController(x);
    }

    public void StopImmediately(bool withYAxis)
    {
        if (withYAxis)
            RigidbodyCompo.velocity = Vector2.zero;
        else
            RigidbodyCompo.velocity = new Vector2(0, RigidbodyCompo.velocity.y);
    }

    #endregion

    #region 회전 관련

    public virtual void Flip()
    {
        FacingDirection = FacingDirection * -1;
        transform.Rotate(0, 180f, 0);
        OnFlip?.Invoke(FacingDirection);
    }

    public virtual void FlipController(float x)
    {
        if (Mathf.Abs(x) < 0.05f) return;
        x = Mathf.Sign(x); //x 의 부호만 가져오거든
        if (Mathf.Abs(FacingDirection + x) < 0.5f)
            Flip();
    }

    #endregion

    #region CheckCollision section

    public virtual bool IsGroundDetected()
    {
        if (_groundChecker == null)
            return false;
        
        return Physics2D.BoxCast(_groundChecker.position,
            new Vector2(_groundCheckBoxWidth, 0.05f), 0,
            Vector2.down, _groundCheckDistance, _whatIsGroundAndWall);
    }

    public virtual bool IsWallDetected() =>
        Physics2D.Raycast(_wallChecker.position,
            Vector2.right * FacingDirection, _wallCheckDistance, _whatIsGroundAndWall);

    #endregion

    public abstract void Attack();
    

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_groundChecker != null)
            Gizmos.DrawWireCube(_groundChecker.position - new Vector3(0, _groundCheckDistance * 0.5f),
                new Vector3(_groundCheckBoxWidth, _groundCheckDistance, 1f));

        if (_wallChecker != null)
            Gizmos.DrawLine(_wallChecker.position, _wallChecker.position + new Vector3(_wallCheckDistance, 0, 0));

        Gizmos.color = Color.white;
    }
#endif


}
