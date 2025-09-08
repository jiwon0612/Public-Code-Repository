using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(menuName = "Input/inputSO")]
public class InputReader : ScriptableObject , Controller.IPlayerActions
{
    public event Action AttackEvent;
    public event Action JumpEvent;
    public event Action DashEvent;
    public event Action SkillEvent;
    public event Action HealEvent;
    public float Xinput { get; private set; }
    public float Yinput { get; private set; }
    public Vector2 AimPosition { get; private set; }


    private Controller controller;

    private void OnEnable()
    {
        if (controller == null)
        {
            controller = new Controller();
            controller.Player.SetCallbacks(this);
        }
        controller.Player.Enable();
    }

    public void SetEnable(bool enable)
    {
        if (controller == null)
        {
            controller = new Controller();
            controller.Player.SetCallbacks(this);
        }
        if (enable)
            controller.Player.Enable();
        else
            controller.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent?.Invoke();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
            DashEvent?.Invoke();
    }

    public void OnGunSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
            SkillEvent?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            JumpEvent?.Invoke();
    }

    public void OnXMove(InputAction.CallbackContext context)
    {
        Xinput = context.ReadValue<float>();
    }

    public void OnYMove(InputAction.CallbackContext context)
    {
        Yinput = context.ReadValue<float>();
    }

    public void OnAimPosition(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    public void OnHealSkilll(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            HealEvent.Invoke();
        }

    }
}
