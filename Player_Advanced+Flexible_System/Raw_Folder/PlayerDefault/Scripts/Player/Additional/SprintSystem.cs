using System;
using PlayerDefault.Scripts.Installers;
using PlayerDefault.Scripts.Player.Main;
using UnityEngine;

namespace PlayerDefault.Scripts.Player.Additional
{
    public class SprintSystem : MonoBehaviour
    {
        [Header("Sprint")]
        [SerializeField] private bool _isSprinting;
        [SerializeField] private float _speedBoostOnSprint = 5f;
        [SerializeField] private float _cantRunStamina = 3f;
        
        [SerializeField] private bool _canSprint = true;

        [Space, Header("Stamina")]
        [SerializeField] private bool _infiniteStamina;
        [SerializeField] private float _maxStamina = 100f;
        [SerializeField] private float _currentStamina;

        [SerializeField] private float _subtractStaminaDelta = 1;
        [SerializeField] private float _addStaminaDelta = 0.5f;
        
        public event Action<bool, float> OnSprintToggled;

        private bool _wasSprinting;
        
        private InputReader _inputReader;
        
        private void Start()
        {
            if (PlayerInstaller.Instance == null)
            {
                throw new UnityException("No PlayerInstaller on scene! Add PlayerInstaller to your scene!");
            }

            _inputReader = PlayerInstaller.Instance.PlayerInputReader;
            _inputReader.SprintButtonStateChanged += ToggleSprint;
            
            _currentStamina = _maxStamina;
            if (_infiniteStamina)
            {
                _currentStamina = _cantRunStamina + 1;
            }
        }

        private void OnDestroy()
        {
            if (_inputReader != null)
            {
                _inputReader.SprintButtonStateChanged -= ToggleSprint;
            }
            OnSprintToggled = null;
        }

        private void FixedUpdate()
        {
            UpdateStamina();
        }

        public void ToggleSprint(bool isPressed)
        {
            if(!_canSprint || _currentStamina <= _cantRunStamina) return;
            
            if (isPressed)
            {
                EnableSprint();
            }
            else
            {
                DisableSprint();
            }
        }
        
        private void EnableSprint()
        {
            _wasSprinting = _isSprinting;
            _isSprinting = true;
            OnSprintToggled?.Invoke(_isSprinting, _speedBoostOnSprint);
        }
        
        private void DisableSprint()
        {
            _wasSprinting = _isSprinting;
            _isSprinting = false;
            
            if(!_wasSprinting) return;
            OnSprintToggled?.Invoke(_isSprinting, -_speedBoostOnSprint);
        }
        
        private void UpdateStamina()
        {
            if(_infiniteStamina) return;
            
            if (_isSprinting)
            {
                _currentStamina = Mathf.Clamp(_currentStamina - _subtractStaminaDelta, 0, _maxStamina);

                if (_currentStamina <= _cantRunStamina)
                {
                    DisableSprint();
                }
            }
            else
            {
                _currentStamina = Mathf.Clamp(_currentStamina + _addStaminaDelta, 0, _maxStamina);
            }
        }
    }
}
