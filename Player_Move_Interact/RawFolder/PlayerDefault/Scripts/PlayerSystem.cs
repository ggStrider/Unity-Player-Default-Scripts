using UnityEngine;

namespace PlayerDefault.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(GetObjectInRay))]
    public class PlayerSystem : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _interactionDistance = 3f;
        
        [SerializeField] private Transform _playerCamera;

        private CharacterController _characterController;
        private GetObjectInRay _gameObjectInRay;
        
        private Vector2 _direction;
        
        private void Start()
        {
            _gameObjectInRay = GetComponent<GetObjectInRay>();
            _characterController = GetComponent<CharacterController>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            var cameraForward = _playerCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            var movementDirection = cameraForward * _direction.y + _playerCamera.transform.right * _direction.x;
            movementDirection.Normalize();

            var finalMoveVector = movementDirection * (_speed * Time.fixedDeltaTime);
            _characterController.SimpleMove(finalMoveVector * _speed);
        }

        public void Interact()
        {
            var objectInRay = _gameObjectInRay._Get(_playerCamera.position,
                _playerCamera.forward, _interactionDistance);
            
            if(objectInRay == null) return;
            
            // Do stuff which you want to (e.g. create interface, and inherit it on other
            // Interact components)
            Debug.Log("Write interact feature(s)");
        }
    }
}