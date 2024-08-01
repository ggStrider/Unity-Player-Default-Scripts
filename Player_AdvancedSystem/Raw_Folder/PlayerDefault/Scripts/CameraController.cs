using UnityEngine;

namespace PlayerDefault.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        
        [Space]
        [SerializeField] private float _sensitivity = 0.3f;
        
        [Space]
        [SerializeField] private float _verticalMinAngle = -80f;
        [SerializeField] private float _verticalMaxAngle = 80f;
        
        private float _rotationX;

        private void Start()
        {
            if (_camera != null) return;
                
            Debug.LogWarning("Camera is null. Getting MainCamera...");

            if (Camera.main == null)
            {
                Debug.LogWarning("Main camera is null! Camera in component is null.");
                return;
            }
            
            _camera = Camera.main.transform;
        }

        /// <summary>
        /// Method to rotate player and camera
        /// </summary>
        /// <param name="lookRotation">rotate vector</param>
        public void SetLookRotation(Vector2 lookRotation)
        {
            var finalRotationVector = lookRotation * _sensitivity;
            
            var xRotateDelta = finalRotationVector.x;
            transform.Rotate(Vector3.up * xRotateDelta);

            _rotationX -= finalRotationVector.y;
            _rotationX = Mathf.Clamp(_rotationX, _verticalMinAngle, _verticalMaxAngle);

            _camera.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        }
    }
}