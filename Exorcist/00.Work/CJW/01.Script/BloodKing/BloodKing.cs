using System;
using System.Collections.Generic;
using Cinemachine;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Events;


public enum BloodKingStateEnum
{
    Idle,
    Chase,
    ChargeAttack,
    TeleponrtJumpAttack,
    DlubleSlash,
    ContinuousAttack,
    BloodBoltAttack,
    AttackReady,
    DashAttack,
    Dead
}

public class BloodKing : Enemy
{
    public BloodKingAnimation BloodKingAnimationComp { get; private set; }

    [Header("BloodKingSetting")] public float _detectRange = 1;
    public float _rangeAttakRange = 1;
    [SerializeField] private ContactFilter2D _contactFilter;
    public GameObject _BoltPrefab;
    public List<BloodKingStateEnum> attackStates;
    public List<BloodKingStateEnum> rangeAttackStates;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _rangeAttackDelay;
    [SerializeField] BossHPUI _bossHPUI;

    public bool IsCanAttack { get; private set; }
    public bool IsCanRangeAttack { get; private set; }

    public Transform damageTrm, UpDamageTrm, DownDamageTrm;
    public float attCheckerRadius;

    [HideInInspector] public Transform targetTrm;
    public EnemyStateMachine<BloodKingStateEnum> StateMachine { get; private set; }

    private Collider2D[] _colliders;

    [HideInInspector] public ParticleSystem groundParticles;

    private Collider2D _hitCollider;
    private CinemachineImpulseSource _camImpulse;
    private ParticleSystem _hitParticles;

    public UnityEvent OnDeadEvent;

    public new BloodKingDamageCaster DamageCasterCompo { get; private set; }

    [Header("Sound Setting")] 
    public AudioClip[] sounds;
    public AudioClip _bgm;

    

    protected override void Awake()
    {
        base.Awake();
        BloodKingAnimationComp = transform.Find("Visual").GetComponent<BloodKingAnimation>();
        groundParticles = transform.Find("GroundParticle").GetComponent<ParticleSystem>();
        _hitCollider = GetComponent<Collider2D>();
        _camImpulse = transform.Find("CamImpulse").GetComponent<CinemachineImpulseSource>();
        _hitParticles = transform.Find("HitParticle").GetComponent<ParticleSystem>();
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<BloodKingDamageCaster>();

        StateMachine = new EnemyStateMachine<BloodKingStateEnum>();
        _colliders = new Collider2D[1];
        attackStates = new List<BloodKingStateEnum>();

        IsCanRangeAttack = true;
        IsCanAttack = true;

        Flip();

        foreach (BloodKingStateEnum state in Enum.GetValues(typeof(BloodKingStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"BloodKing{typeName}State");

            if (t != null)
            {
                var enemyState =
                    Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<BloodKingStateEnum>;
                StateMachine.AddState(state, enemyState);
                if (t.IsSubclassOf(typeof(BloodKingAttackState)))
                    attackStates.Add(state);
                else if (t.IsSubclassOf(typeof(BloodKingRangeAttackState)))
                    rangeAttackStates.Add(state);
            }
            else
            {
                Debug.LogError($"Enemy goul: no State[{typeName}]");
            }
        }
    }

    private void Start()
    {
        StateMachine.Initialize(BloodKingStateEnum.Idle, this);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public Collider2D GetPlayerInRange()
    {
        int count = Physics2D.OverlapCircle(transform.position, _detectRange, _contactFilter, _colliders);
        return count > 0 ? _colliders[0] : null;
    }

    public void UseBloodBolt(Vector2 position)
    {
        GameObject obj = Instantiate(_BoltPrefab);
        obj.transform.position = position;
    }

    public BloodKingStateEnum GetRandomAttackState()
    {
        if (attackStates.Count <= 0) return BloodKingStateEnum.Idle;

        int index = UnityEngine.Random.Range(0, attackStates.Count);
        float time = UnityEngine.Random.Range(_attackDelay - 3, _attackDelay + 3);
        float dealTime = UnityEngine.Random.Range(0, attackCooldown);

        IsCanAttack = false;

        BloodKingStateEnum type = attackStates[index];
        attackStates.Remove(type);
        StartDelayCallback(time, () => attackStates.Add(type));
        StartDelayCallback(dealTime, () => IsCanAttack = true);
        return type;
    }

    public BloodKingStateEnum GetRandomLoogRangeAtkState()
    {
        if (rangeAttackStates.Count <= 0) return BloodKingStateEnum.Idle;

        int index = UnityEngine.Random.Range(0, rangeAttackStates.Count);
        float time = UnityEngine.Random.Range(_rangeAttackDelay - 3, _rangeAttackDelay + 3);
        float dealTime = UnityEngine.Random.Range(0, attackCooldown);

        IsCanRangeAttack = false;

        BloodKingStateEnum type = rangeAttackStates[index];
        rangeAttackStates.Remove(type);
        StartDelayCallback(time, () => rangeAttackStates.Add(type));
        StartDelayCallback(dealTime, () => IsCanRangeAttack = true);
        return type;
    }

    public override void Attack()
    {
        bool isHit =DamageCasterCompo.CastSetDamage(0, DamageCasterCompo.atkDamage, damageTrm);
        
        if (isHit)
            EazySoundManager.PlaySound(sounds[3]);
    }

    public void StrongAtk()
    {
        bool isHit = DamageCasterCompo.CastSetDamage(0, DamageCasterCompo.strongAtkDamage, damageTrm);

        if (isHit)
            EazySoundManager.PlaySound(sounds[3]);
    }

    public void LightAtk()
    {
        bool isHit = DamageCasterCompo.CastSetDamage(0, DamageCasterCompo.lightAtkDamage, damageTrm);
        
        if (isHit)
            EazySoundManager.PlaySound(sounds[3]);
    } 

    public void DownAttack()
    {
        DamageCasterCompo.CastSetDamage(0, DamageCasterCompo.strongAtkDamage, DownDamageTrm);
    }

    public override void OnAnimationTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public void SetActiveCollider(bool active)
    {
        _hitCollider.enabled = active;
    }

    public void CamImpulse(Vector3 impulse)
    {
        _camImpulse.m_DefaultVelocity = impulse;
        _camImpulse.GenerateImpulse();
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
        _bossHPUI.SetBoss(this);
    }

    protected override void HandleHitEvent()
    {
        _hitParticles.Play();
        var player = GetPlayerInRange();
        Vector2 dir = -(player.transform.position - transform.position).normalized;
        dir = dir / 2;
        CamImpulse(dir);
    }

    protected override void HandleDead(Vector2 direction)
    {
        StateMachine.ChangeState(BloodKingStateEnum.Dead, true);
    }

    public void PlayAttackSound() => EazySoundManager.PlaySound(sounds[0]);
    public void PlayDashSound() => EazySoundManager.PlaySound(sounds[1]);
    public void PlayDownAttackSound() => EazySoundManager.PlaySound(sounds[2]);

    public void PlayBgm() => EazySoundManager.PlayMusic(_bgm);
    public void StopBgm() => EazySoundManager.StopAllMusic();

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _detectRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _rangeAttakRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.white;
    }

    protected override void OnDrawGizmos()
    {
        if (damageTrm != null || UpDamageTrm != null || DownDamageTrm != null)
        {
            Gizmos.DrawWireSphere(damageTrm.position, attCheckerRadius);
            Gizmos.DrawWireSphere(UpDamageTrm.position, attCheckerRadius);
            Gizmos.DrawWireSphere(DownDamageTrm.position, attCheckerRadius);
        }
    }

#endif
}