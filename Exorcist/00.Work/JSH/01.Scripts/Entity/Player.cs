using Hellmade.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{   

    [Header("Basic Setting")]
    public float MoveSpeed = 10f;
    public float JumpForce = 10f;
    public float DashSpeed = 20f;
    public float DashDuration = 0.4f;
    public Vector2 WallJumpForce;

    private float _defaultmoveSpeed;
    private float _defaultjumpForce;
    private float _defaultdashSpeed;

    [Header("Attack Setting")]
    public float AttackSpeed = 1f;
    public float AttackDuration = 0.2f;
    public Vector2[] attackMovement;
    [HideInInspector] public int currentComboCount = 0;

    [SerializeField] private InputReader inputReader;
    public InputReader PlayerInput => inputReader;
    public PlayerStateMachine StateMachine { get; private set; }

    public Animator gunAni { get; private set; }
    public GunAniTrigger gunAniTrigger { get; private set; }



    public Transform AttackCheckerTrm;
    public Transform UpattackCheckerTrm;
    public Transform DownattackCheckerTrm;
    public Transform attackOverridePos;

    [Range(0, 1)]
    public DeathUI deathUI;
    public GameObject healParticle;

    [Header("SOUNDS")]
    public List<AudioClip> playerAudio;

    protected override void Awake()
    {
        base.Awake();
        gunAniTrigger = FindObjectOfType<GunAniTrigger>();
        gunAni = gunAniTrigger.GetComponent<Animator>();
        StateMachine = new PlayerStateMachine();

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{typeName} is loading error!");
                Debug.LogError(ex);
            }
        }
    }

    protected void Start()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);

        _defaultdashSpeed = DashSpeed;
        _defaultmoveSpeed = MoveSpeed;
        _defaultjumpForce = JumpForce;

        Debug.Log(inputReader);
    }

    protected void Update()
    {
        StateMachine.CurrentState.Update(); //�� �����Ӹ��� ������Ʈ�� �̷������.
    }

    private void OnEnable()
    {
        PlayerInput.DashEvent += HandleDashEvent;
        PlayerInput.SkillEvent += HandleSkillEvent;
        PlayerInput.HealEvent += HandleHealEvent;
    }

    private void OnDisable()
    {
        PlayerInput.SkillEvent -= HandleSkillEvent;
        PlayerInput.DashEvent -= HandleDashEvent;
        PlayerInput.HealEvent -= HandleHealEvent;
    }

    private void HandleSkillEvent()
    {
        //�� �͸����� �ϰ� �Ұ��ΰ�?? 
        //�� �־�ΰ� ���� �����ض� 

        if (PlayerSkillManager.Instance.GetSkill<GunSkill>().AttemptUseSkill() && gunAniTrigger.isActive == false && !(StateMachine.CurrentState is PlayerAttackState))
        {
            EazySoundManager.PlaySound(playerAudio[1]);
            StateMachine.ChangeState(PlayerStateEnum.Gun);
        }
    }

    private void HandleDashEvent()
    {
        //�뽬 ��ų ����
        if (IsWallDetected())
            return;

        if (PlayerSkillManager.Instance.GetSkill<DashSkill>().AttemptUseSkill())
        {
            EazySoundManager.PlaySound(playerAudio[4]);
            StateMachine.ChangeState(PlayerStateEnum.Dash);
        }
    }

    private void HandleHealEvent()
    {
        if (isDead) return;
        HealthCompo.Healing();
    }


    public override void Attack()
    {
        AttackOverride(attackOverridePos);
    }

    private void AttackOverride(Transform attackPos)
    {
        bool isHit = DamageCasterCompo.CastDamage(currentComboCount, attackPos);
        if (isHit)
        {
            EazySoundManager.PlaySound(playerAudio[0]);
        }
        else
        {
            EazySoundManager.PlaySound(playerAudio[3]);
        }
    }

    protected override void HandleHitEvent()
    {
        StateMachine.ChangeState(PlayerStateEnum.Hit);
    }

    protected override void HandleDead(Vector2 direction)
    {
        base.HandleDead(direction);
        StateMachine.ChangeState(PlayerStateEnum.Dead);
    }

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimatorFinishTrigger();


}
