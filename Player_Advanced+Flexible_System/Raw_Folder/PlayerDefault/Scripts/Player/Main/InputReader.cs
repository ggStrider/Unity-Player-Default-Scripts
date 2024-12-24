using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerDefault.Scripts.Player.Main
{
    public class InputReader : MonoBehaviour
    {
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnLook;
        public event Action<bool> SprintButtonStateChanged;
        public event Action OnInteracted;
        
        private InputSystem_Actions _inputMap;

        private void Awake()
        {
            _inputMap = new InputSystem_Actions();
            
            _inputMap.Player.Move.performed += context => NotifyVector2(context, OnMove);
            _inputMap.Player.Move.canceled += context => NotifyVector2(context, OnMove);

            _inputMap.Player.Look.performed += context => NotifyVector2(context, OnLook);
            _inputMap.Player.Look.canceled += context => NotifyVector2(context, OnLook);

            _inputMap.Player.Sprint.started += _ => SprintButtonStateChanged?.Invoke(true);
            _inputMap.Player.Sprint.canceled += _ => SprintButtonStateChanged?.Invoke(false);

            _inputMap.Player.Interact.started += _ => OnInteracted?.Invoke();
            
            _inputMap.Player.Enable();
        }

        private void NotifyVector2(InputAction.CallbackContext context, Action<Vector2> action)
        {
            var direction = context.ReadValue<Vector2>();
            action?.Invoke(direction);
        }

        private void OnDisable()
        {
            _inputMap.Dispose();

            OnMove = null;
            OnLook = null;
            SprintButtonStateChanged = null;
            OnInteracted = null;
        }
    }
}
