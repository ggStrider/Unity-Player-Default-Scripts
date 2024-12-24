using UnityEngine;

using PlayerDefault.Scripts.Interact;
using PlayerDefault.Scripts.Installers;
using PlayerDefault.Scripts.Player.Main;

namespace PlayerDefault.Scripts.Player.Additional
{
    public class InteractSystem : MonoBehaviour
    {
        [Tooltip("Usually I put Player camera here")]
        [SerializeField] private Transform _rayOutPoint;
        [SerializeField] private float _interactDistance = 3f;
        
        private GetGameObjectByCast _getGameObjectByCast;

        private InputReader _inputReader;
        
        private void Start()
        {
            if (PlayerInstaller.Instance == null)
            {
                throw new UnityException("No PlayerInstaller on scene! Add PlayerInstaller to your scene!");
            }

            _inputReader = PlayerInstaller.Instance.PlayerInputReader;
            _inputReader.OnInteracted += Interact;
            
            _getGameObjectByCast = new GetGameObjectByCast();
        }

        private void OnDestroy()
        {
            if (_inputReader != null)
            {
                _inputReader.OnInteracted -= Interact;
            }
        }

        private void Interact()
        {
            var result = _getGameObjectByCast.GetByRay(_rayOutPoint.position, 
                _rayOutPoint.forward, _interactDistance);
            
            result?.GetComponent<IInteract>()?.OnInteract();
        }
    }
}