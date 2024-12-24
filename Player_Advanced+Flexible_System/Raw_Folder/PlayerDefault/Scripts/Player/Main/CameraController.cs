using PlayerDefault.Scripts.Installers;
using UnityEngine;

namespace PlayerDefault.Scripts.Player.Main
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Tooltip("for rotating")] private Transform _player;
        [SerializeField] private Transform _camera;
        
        [Space]
        [SerializeField] private float _sensitivity = 0.3f;
        
        [Space]
        [SerializeField] private float _verticalMinAngle = -80f;
        [SerializeField] private float _verticalMaxAngle = 80f;
        
        private float _rotationX;

        private InputReader _inputReader;
        
        private void Start()
        {
            if (_camera == null)
            {
                Debug.LogWarning("Camera is null. Getting MainCamera...");

                if (Camera.main == null)
                {
                    throw new MissingComponentException("Main camera is null! Camera in component is null." +
                                                        "Add Camera to CameraController and restart the scene!");
                }
            
                _camera = Camera.main.transform;

                if (PlayerInstaller.Instance.PlayerInputReader == null)
                {
                    throw new UnityException("No PlayerInstaller on scene! Add PlayerInstaller to your scene!");
                }
            }
            
            // Checks if camera still null, after operations upper
            if(_camera == null) return;

            if (_player == null)
            {
                throw new UnityException("Player is null! Add Player to your scene, and restart the game!");
            }
            
            _inputReader = PlayerInstaller.Instance.PlayerInputReader;
            _inputReader.OnLook += SetLookRotation;
        }

        /// <summary>
        /// Method to rotate player and camera
        /// </summary>
        /// <param name="lookRotation">rotate vector</param>
        public void SetLookRotation(Vector2 lookRotation)
        {
            var finalRotationVector = lookRotation * _sensitivity;
            
            var xRotateDelta = finalRotationVector.x;
            _player.Rotate(Vector3.up * xRotateDelta);

            _rotationX -= finalRotationVector.y;
            _rotationX = Mathf.Clamp(_rotationX, _verticalMinAngle, _verticalMaxAngle);

            _camera.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        }
    }
}