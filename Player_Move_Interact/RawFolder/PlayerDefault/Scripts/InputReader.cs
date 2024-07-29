using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerDefault.Scripts
{
    [RequireComponent(typeof(PlayerSystem))]
    [RequireComponent(typeof(CameraController))]
    public class InputReader : MonoBehaviour
    {
        private InputSystem_Actions _inputMap;

        private PlayerSystem _playerSystem;
        private CameraController _cameraController;

        private void Awake()
        {
            _playerSystem = GetComponent<PlayerSystem>();
            _cameraController = GetComponent<CameraController>();

            _inputMap = new InputSystem_Actions();
            
            _inputMap.Player.Move.performed += OnMove;
            _inputMap.Player.Move.canceled += OnMove;

            _inputMap.Player.Look.performed += OnLook;
            _inputMap.Player.Look.canceled += OnLook;

            _inputMap.Player.Interact.started += OnInteract;
            
            _inputMap.Enable();
        }

        private void OnDisable()
        {
            _inputMap.Player.Move.performed -= OnMove;
            _inputMap.Player.Move.canceled -= OnMove;
            
            _inputMap.Player.Look.performed -= OnLook;
            _inputMap.Player.Look.canceled -= OnLook;

            _inputMap.Player.Interact.started -= OnInteract;
            
            _inputMap.Disable();
        }
        
        
        private void OnInteract(InputAction.CallbackContext context)
        {
            _playerSystem.Interact();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            var lookDirection = context.ReadValue<Vector2>();
            _cameraController.SetLookRotation(lookDirection);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _playerSystem.SetDirection(direction);
        }
    }
}
