using UnityEngine;

namespace PlayerDefault.Scripts
{
    [RequireComponent(typeof(PlayerSystem))]
    public class CameraBobbing : MonoBehaviour
    {
        [SerializeField] private bool _enableBobbing = true;
        
        [SerializeField] private Transform _cameraParent;
        
        [SerializeField, Range(0f, 10f)] private float _bobbingFrequency = 5f;
        [SerializeField, Range(0f, 1f)] private float _bobbingAmplitude = 0.05f;
        private float _bobbingPhase;

        [Space]
        [SerializeField] private float _boostFrequencyOnSprint = 2f;
        
        private float _bobbingValue;
        private PlayerSystem _playerSystem;

        private void Start()
        {
            _playerSystem = GetComponent<PlayerSystem>();
            _playerSystem.OnSprintToggled += HandleBobbingOnSprint;
        }

        private void Update()
        {
            if(!_playerSystem.IsPlayerMoving() || !_enableBobbing) return;

            // Using sinusoid to smoothly change camera position
            _bobbingValue += Time.deltaTime;
            var sinusValue = Mathf.Sin(_bobbingValue * _bobbingFrequency) * _bobbingAmplitude;

            _cameraParent.localPosition = Vector3.up * sinusValue;
        }

        private void HandleBobbingOnSprint(bool isSprinting)
        {
            if (isSprinting)
            {
                _bobbingPhase = _bobbingValue * _bobbingFrequency;
                
                _bobbingFrequency += _boostFrequencyOnSprint;
                _bobbingValue = _bobbingPhase / _bobbingFrequency;
            }
            else
            {
                _bobbingPhase = _bobbingValue * _bobbingFrequency;
            
                _bobbingFrequency -= _boostFrequencyOnSprint;
                _bobbingValue = _bobbingPhase / _bobbingFrequency;
            }
        }

        private void OnDisable()
        {
            _playerSystem.OnSprintToggled -= HandleBobbingOnSprint;
        }
    }
}