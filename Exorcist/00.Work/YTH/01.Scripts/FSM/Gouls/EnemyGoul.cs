using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoulStateEnum
{
    Transition,
    Attack,
    Battle,
    Hit,
    Dead
}

public class EnemyGoul : Enemy
{
    public EnemyStateMachine<GoulStateEnum> StateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        StateMachine = new EnemyStateMachine<GoulStateEnum>();

        foreach(GoulStateEnum state in Enum.GetValues(typeof(GoulStateEnum)))
        {
            string typeName = state.ToString();
            Type t = Type.GetType($"Goul{typeName}State");

            if(t != null)
            {
                var enemyState = Activator.CreateInstance(t, this, StateMachine, typeName) as EnemyState<GoulStateEnum>;
                StateMachine.AddState(state, enemyState);
            }
            else
            {
                Debug.LogError($"Enemy goul: no State[{typeName}]");
            }
        }
    }

    protected override void HandleHitEvent()
    {
        base.HandleHitEvent();
        StateMachine.ChangeState(GoulStateEnum.Hit);
    }

    protected void Start()
    {
        StateMachine.Initialize(GoulStateEnum.Transition, this);
    }

    protected void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    public override void OnAnimationTrigger()
    {
        StateMachine.CurrentState.AnimationFinishTrigger();
    }

    public override void Attack()
    {
        //데미지 캐스터
    }
    protected override void HandleDead(Vector2 direction)
    {
        StateMachine.ChangeState(GoulStateEnum.Dead, true);
    }
}
