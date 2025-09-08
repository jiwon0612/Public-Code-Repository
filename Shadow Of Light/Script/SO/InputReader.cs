using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReader : ScriptableObject, Console.IPlayerActions
{
    private Console _console;
    public Console Console => _console;

    public float inputX { get; private set; }
    public float inputY { get; private set; }

    public event Action onInputJump;
    public Action onInteraction;
    public event Action onInteractionEnerge;
    public Action onUseTool;
    public event Action onTransMap;
    public Action<int> onChangeTool;
    public event Action OnPressESC;



    private void OnEnable()
    {
        if (_console == null)
        {
            _console = new Console();
            _console.Player.SetCallbacks(this);

        }
        _console.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().normalized.x;
        inputY = context.ReadValue<Vector2>().normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onInputJump?.Invoke();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onInteraction?.Invoke();
            onInteractionEnerge?.Invoke();
        }
    }

    public void OnTransMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onTransMap?.Invoke();
        }
    }

    public void OnUseTool(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onUseTool?.Invoke();
        }

    }

    public void OnChangeTool1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onChangeTool?.Invoke(0);
        }
    }

    public void OnChangeTool2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onChangeTool?.Invoke(1);
        }
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPressESC?.Invoke();
        }
    }
}
