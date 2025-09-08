using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/UIInputSO")]
public class UIInputReader : ScriptableObject, Controller.IUIActions
{
    private Controller _controller;
    public Action OnUIOpenClose;

    private void OnEnable()
    {
        if(_controller == null)
        {
            _controller = new Controller();
            _controller.UI.SetCallbacks(this);
        }
        _controller.UI.Enable();
        _controller.UI.SetCallbacks(this);
    }

    public void OnUIop(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnUIOpenClose?.Invoke();
        }
    }
}
