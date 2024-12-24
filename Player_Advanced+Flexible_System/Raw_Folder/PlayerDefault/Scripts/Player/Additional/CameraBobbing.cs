using PlayerDefault.Scripts.Installers;
using PlayerDefault.Scripts.Player.Main;
using UnityEngine;

namespace PlayerDefault.Scripts.Player.Additional
{
    [RequireComponent(typeof(PlayerMovement))]
    public class CameraBobbing : MonoBehaviour
    {
        [SerializeField] private bool _enableBobbing = true;
        private bool _isPlayerMoving;
        
        [SerializeField] private Transform _cameraParent;
        
        [SerializeField, Range(0f, 10f)] private float _bobbingFrequency = 5f;
        [SerializeField, Range(0f, 1f)] private float _bobbingAmplitude = 0.05f;
        private float _bobbingPhase;

        [Space]
        [SerializeField] private float _boostFrequencyOnSprint = 2f;
        
        private float _bobbingValue;
        
        private SprintSystem _sprintSystem;
        private PlayerMovement _playerMovement;
        
        private void Start()
        {
            if (PlayerInstaller.Instance == null)
            {
                throw new UnityException("No PlayerInstaller on scene! Add PlayerInstaller to your scene!");
            }

            _sprintSystem = PlayerInstaller.Instance.SprintSystem;
            _sprintSystem.OnSprintToggled += HandleBobbingOnSprint;
            
            _playerMovement = PlayerInstaller.Instance.PlayerMovement;
            _playerMovement.IsMoving += state => _isPlayerMoving = state;
        }
        
        private void Update()
        {
            if(!_isPlayerMoving || !_enableBobbing) return;
        
            // Using sinusoid to smoothly change camera position
            _bobbingValue += Time.deltaTime;
            var sinusValue = Mathf.Sin(_bobbingValue * _bobbingFrequency) * _bobbingAmplitude;
        
            _cameraParent.localPosition = Vector3.up * sinusValue;
        }
        
        private void HandleBobbingOnSprint(bool isSprinting, float _)
        {
            _bobbingPhase = _bobbingValue * _bobbingFrequency;
            
            if (isSprinting)
            {
                _bobbingFrequency += _boostFrequencyOnSprint;
            }
            else
            {
                _bobbingFrequency -= _boostFrequencyOnSprint;
            }
            _bobbingValue = _bobbingPhase / _bobbingFrequency;
        }
        
        private void OnDisable()
        {
            if (_sprintSystem != null)
            {
                _sprintSystem.OnSprintToggled -= HandleBobbingOnSprint;
            }
            
            if (_playerMovement != null)
            {
                _playerMovement.IsMoving -= state => _isPlayerMoving = state;
            }
        }
    }
}