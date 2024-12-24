using UnityEngine;
using System;

using PlayerDefault.Scripts.Installers;
using PlayerDefault.Scripts.Player.Additional;

namespace PlayerDefault.Scripts.Player.Main
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private bool _canMove = true;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _addSpeedDelta = 1f;
        [SerializeField] private float _currentSpeed;
        
        [SerializeField] private Transform _playerCamera;

        private CharacterController _characterController;
        
        private Vector2 _direction;
        
        public Action<bool> IsMoving;
        
        private InputReader _inputReader;
        private SprintSystem _sprintSystem;
        
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            if (PlayerInstaller.Instance == null)
            {
                throw new UnityException("No PlayerInstaller on scene! Add PlayerInstaller to your scene!");
            }
            
            _inputReader = PlayerInstaller.Instance.PlayerInputReader;
            _inputReader.OnMove += SetDirection;
            
            _sprintSystem = PlayerInstaller.Instance.SprintSystem;
            _sprintSystem.OnSprintToggled += HandleSprinting;
        }

        private void OnDestroy()
        {
            if (_inputReader != null)
            {
                _inputReader.OnMove -= SetDirection;
            }

            if (_sprintSystem != null)
            {
                _sprintSystem.OnSprintToggled -= HandleSprinting;
            }
            
            IsMoving = null;
        }
        
        /// <summary>
        /// Sets movement direction for player using input reader
        /// </summary>
        /// <param name="direction">Movement vector</param>
        private void SetDirection(Vector2 direction)
        {
            _direction = direction;
            
            IsMoving?.Invoke(_canMove && _direction != Vector2.zero);
            
            // If player is not moving, set current speed to 0
            if(_direction != Vector2.zero) return;
            _currentSpeed = 0;
        }

        private void FixedUpdate()
        {
            if(_canMove) Move();
        }

        private void Move()
        {
            if (IsPlayerMoving())
            {
                _currentSpeed = Mathf.Clamp(_currentSpeed + _addSpeedDelta, 0, _maxSpeed);
            }
            
            var cameraForward = _playerCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            // direction.x in current context means right/left
            // direction.y in current context means forward/backwards
            var movementDirection = cameraForward * _direction.y + _playerCamera.transform.right * _direction.x;
            movementDirection.Normalize();

            var finalMoveVector = movementDirection * Time.fixedDeltaTime;
            
            _characterController.SimpleMove(finalMoveVector.normalized * _currentSpeed);   
        }

        private bool IsPlayerMoving()
        {
            return _direction != Vector2.zero;
        }

        private void HandleSprinting(bool isSprinting, float boost)
        {
            _maxSpeed += boost;
        }
    }
}