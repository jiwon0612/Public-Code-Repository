using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input", order = 0)]
    public class PlayerInputSO : ScriptableObject, Console.IPlayerActions
    {
        public event Action OnShowSettingEvent;
        public event Action<bool> OnDashChargeEvent;
        public event Action<bool> OnDescentEvent;
        public event Action<bool> OnClickEvent;
        
        public Vector2 MovementKey { get; private set; }
        public Vector2 LookKey { get; private set; }
        
        private Console _console;

        private void OnEnable()
        {
            if (_console == null)
            {
                _console = new Console();
                _console.Player.SetCallbacks(this);
            }
            _console.Player.Enable();
        }

        private void OnDisable()
        {
            OnDescentEvent = null;
            _console.Player.Disable();
        }

        private void OnDestroy()
        {
            OnDescentEvent = null;
            _console.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementKey = context.ReadValue<Vector2>();
        }

        
        public void OnDashCharge(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDashChargeEvent?.Invoke(true);
            else if (context.canceled)
                OnDashChargeEvent?.Invoke(false);
        }

        public void OnDescent(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDescentEvent?.Invoke(true);
            else if (context.canceled)
                OnDescentEvent?.Invoke(false);
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnClickEvent?.Invoke(true);
            else if (context.canceled)
                OnClickEvent?.Invoke(false);
        }

        public void OnShowSetting(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnShowSettingEvent?.Invoke();
        }

        public void OnLookDelta(InputAction.CallbackContext context)
        {
            LookKey = context.ReadValue<Vector2>();
        }

    }
}